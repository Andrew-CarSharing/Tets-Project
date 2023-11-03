using System.Data.Entity;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using WebApplication5.Command.DataBaseCommand;
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
    private IMediator _mediator;

    public DataBaseAction(ShopContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    [HttpPost("CreateDataBase")]
    public async Task<string> CreateTableBuilder(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreateDataBaseCommand(), cancellationToken);
    }
}