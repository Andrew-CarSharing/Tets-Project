using MediatR;
using WebApplication5.Command;
using WebApplication5.DBContext;
using WebApplication5.DTO;

namespace WebApplication5.Handler;

public class Top100TripTimeSpendCommandHandler: IRequestHandler<Top100TripTimeSpendCommand, List<TopTripTimeDTO>>
{
    private ShopContext _context;

    public Top100TripTimeSpendCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<TopTripTimeDTO>> Handle(Top100TripTimeSpendCommand command,CancellationToken cancellationToken)
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
}