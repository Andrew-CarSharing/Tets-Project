using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Command;
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

    private readonly IMediator _mediator;
   
    public MainQueries(ShopContext context, IMediator mediator)
    {
        _context = context;
       // _mapper = mapper;
       _mediator = mediator;
    }
    [HttpGet("HighestAverageTipAmount")]
    public async Task<AverageTipModel> HighestAverageTipAmount(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new HighestAverageTipAmountCommand(), cancellationToken);
    }
    
    [HttpGet("Top100TripDistance")]
    public async Task<List<TopTripDistanceDTO>> Top100TripDistance(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new Top100TripDistanceCommand(), cancellationToken);
    }
    
    [HttpGet("Top100TripTimeSpend")]
    public async Task<List<TopTripTimeDTO>> Top100TripTimeSpend(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new Top100TripTimeSpendCommand(), cancellationToken);
    }

    [HttpPost("Search")]
    public async Task<List<Data>> Search(short PULocationId,CancellationToken cancellationToken)
    {
        return await _mediator.Send(new SearchCommand(PULocationId), cancellationToken);
    }

    [HttpPost("AddToDataBase")]
    public async Task<string> AddToDataBase(InputDataModel tripModel,CancellationToken cancellationToken)
    {
        return await _mediator.Send(new AddToDataBaseCommand(tripModel), cancellationToken);
    }

    [HttpDelete("RemoveDuplicates")]

    public async Task<string> RemoveDuplicates(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new RemoveDuplicatesCommand(), cancellationToken);
    }

    [HttpGet("ChangeNAndY")]
    public async Task<string> ChangeNAndY(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new ChangeNAndYCommand(), cancellationToken);
    }

    [HttpGet("ConvertESRTToUTC")]
    public async Task<string> Convert_EST_To_UTC(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new ConvertESRTToUTCCommand(), cancellationToken);
    }
}