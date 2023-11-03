using System.Data.Entity;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using WebApplication5.Context;
using WebApplication5.DBContext;
using WebApplication5.Model;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace WebApplication5.Controllers;

[ApiController]
[Route("DataBaseAction")]
public class DataBaseAction : ControllerBase
{
    private ShopContext _context;

    public DataBaseAction(ShopContext context)
    {
        _context = context;
    }

    [HttpPost("CreateDataBase")]
    public async Task<string> CreateTableBuilder()
    {
        try
        {
            using var reader = new StreamReader("/Users/andrew/work/sample-cab-data.csv");
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            var shops = csv.GetRecords<CsvModel>(); // Mapping to a class that represents CSV columns

            foreach (var userModel in shops)
            {
                var data = new Data
                {
                    tpep_pickup_datetime = userModel.tpep_pickup_datetime,
                    tpep_dropoff_datetime = userModel.tpep_dropoff_datetime,
                    passenger_count = userModel.passenger_count,
                    trip_distance = userModel.trip_distance,
                    store_and_fwd_flag = userModel.store_and_fwd_flag,
                    PULocationID = userModel.PULocationID,
                    DOLocationID = userModel.DOLocationID,
                    fare_amount = userModel.fare_amount,
                    tip_amount = userModel.tip_amount
                };

                _context.datas.Add(data);
            }

            _context.SaveChanges();

            return "Okay";
        }
        catch (Exception ex)
        {
            // Handle and log the exception, and return an appropriate error response.
            return ex.Message;
        }
    }
}