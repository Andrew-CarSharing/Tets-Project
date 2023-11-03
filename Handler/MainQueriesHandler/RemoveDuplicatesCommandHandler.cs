using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using WebApplication5.Command;
using WebApplication5.DBContext;

namespace WebApplication5.Handler;

public class RemoveDuplicatesCommandHandler: IRequestHandler<RemoveDuplicatesCommand, string>
{
    private ShopContext _context;

    public RemoveDuplicatesCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(RemoveDuplicatesCommand command, CancellationToken cancellationToken)
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
}