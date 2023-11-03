using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Context;
using WebApplication5.DBContext;
using WebApplication5.DTO;
using WebApplication5.Model;

namespace WebApplication5.Controllers;
[ApiController]
[Route("MainQueries")]
public class MainQueries
{
    private ShopContext _context;
   // private IMapper _mapper;
    public MainQueries(ShopContext context /*IMapper mapper*/)
    {
        _context = context;
       // _mapper = mapper;
    }
    [HttpGet("FindLocationWithHighestAverageTipAmount")]
    public int FindLocationWithHighestAverageTipAmount()
    {
        var result = _context.datas
            .GroupBy(d => d.PULocationID)
            .Select(g => new
            {
                PULocationId = g.Key,
                AverageTipAmount = g.Average(d => d.tip_amount)
            })
            .OrderByDescending(x => x.AverageTipAmount)
            .FirstOrDefault();

        if (result != null)
        {
            return result.PULocationId;
        }
        return -1; 
    }
    
    [HttpGet("Top100TripDistance")]
    public async Task<List<TopTripDistanceDTO>> Top100TripDistance()
    {
        var topTripDistances = await _context.datas
            .OrderByDescending(trip => trip.trip_distance)
            .Take(100)
            .Select(data => new TopTripDistanceDTO
            {
                id = data.id,
                trip_distance = data.trip_distance,
            })
            .ToListAsync();

        return topTripDistances;
    }
    
    [HttpGet("Top100TripTimeSpend")]
    public async Task<List<TopTripTimeDTO>> Top100TripTimeSpend()
    {
            var topLongestFares =  _context.datas
                .AsEnumerable()
                .OrderByDescending(trip => (trip.tpep_dropoff_datetime - trip.tpep_pickup_datetime))
                .Take(100)
                .Select(data => new TopTripTimeDTO
                {
                    id = data.id,
                    trip_time = (data.tpep_dropoff_datetime - data.tpep_pickup_datetime)
                })
                .ToList();

            return topLongestFares;
        }

    [HttpPost("Search")]
    public async Task<List<Data>> Search(short PULocationId)
    {
        try
        {
            var searchResults = await _context.datas
                .Where(data => (PULocationId == 0 || data.PULocationID == PULocationId))
                .ToListAsync();
            
            return searchResults;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    [HttpPost("AddToDataBase")]
    public async Task<string> AddToDataBase(InputDataModel tripModel)
    {
        try
        {
            var trip = new Data
            {
                tpep_pickup_datetime = tripModel.tpep_pickup_datetime,
                tpep_dropoff_datetime = tripModel.tpep_dropoff_datetime,
                passenger_count = tripModel.passenger_count,
                trip_distance = tripModel.trip_distance,
                store_and_fwd_flag = tripModel.store_and_fwd_flag,
                PULocationID = tripModel.PULocationID,
                DOLocationID = tripModel.DOLocationID,
                fare_amount = tripModel.fare_amount,
                tip_amount = tripModel.tip_amount
            };
            await _context.datas.AddAsync(trip);

            await _context.SaveChangesAsync();

            return "Successful!";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    [HttpDelete("RemoveDuplicates")]

    public async Task<string> RemoveDuplicates()
    {
        try
        {
            var duplicates =  _context.datas
                .AsEnumerable() 
                .GroupBy(trip => new { trip.tpep_pickup_datetime, trip.tpep_dropoff_datetime, trip.passenger_count })
                .Where(g => g.Count() > 1)
                .SelectMany(g => g.Skip(1))
                .ToList();

            using (var writer = new StreamWriter("/Users/andrew/work/duplicates.csv"))
            using (var csvWriter = new CsvWriter(writer,
                       new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
            {
                csvWriter.WriteRecords(duplicates);
            }

            return "Successful!";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "Error";
        }
    }

    [HttpGet("ChangeNAndY")]
    public async Task<string> ChangeNAndY()
    {
        try
        {
            foreach (var trip in _context.datas)
            {
                if (trip.store_and_fwd_flag == "N")
                {
                    trip.store_and_fwd_flag = "No";
                }
                else if (trip.store_and_fwd_flag == "Y")
                {
                    trip.store_and_fwd_flag = "Yes";
                }
            }

            await _context.SaveChangesAsync();

            return "Successful!";
        }
        catch (Exception ex)
        {
            return ex.Message ;
        }
    }

    [HttpGet("ConvertESRTToUTC")]

    public async Task<string> Convert_EST_To_UTC()
    {
        try
        {
            var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            
            foreach (var trip in _context.datas)
            {
                trip.tpep_pickup_datetime = TimeZoneInfo.ConvertTimeToUtc(trip.tpep_pickup_datetime, estTimeZone);
                
                trip.tpep_dropoff_datetime = TimeZoneInfo.ConvertTimeToUtc(trip.tpep_dropoff_datetime, estTimeZone);
            }

            await _context.SaveChangesAsync();

            return "Successful!";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}