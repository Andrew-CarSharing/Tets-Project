using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using WebApplication5.Command.DataBaseCommand;
using WebApplication5.Context;
using WebApplication5.DBContext;
using WebApplication5.Model;

namespace WebApplication5.Handler.DataBaseHandler;

public class CreateDataBaseCommandHandler: IRequestHandler<CreateDataBaseCommand, string>
{
    private readonly ShopContext _context;

    public CreateDataBaseCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateDataBaseCommand command, CancellationToken cancellationToken)
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
            return ex.Message;
        }
    }
}