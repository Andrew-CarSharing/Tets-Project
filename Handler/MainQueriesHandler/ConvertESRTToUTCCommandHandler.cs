using MediatR;
using WebApplication5.Command;
using WebApplication5.DBContext;

namespace WebApplication5.Handler;

public class ConvertESRTToUTCCommandHandler: IRequestHandler<ConvertESRTToUTCCommand, string>
{
    private ShopContext _context;

    public ConvertESRTToUTCCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(ConvertESRTToUTCCommand command, CancellationToken cancellationToken)
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