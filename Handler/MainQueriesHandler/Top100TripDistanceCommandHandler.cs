using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Command;
using WebApplication5.DBContext;
using WebApplication5.DTO;

namespace WebApplication5.Handler;

public class Top100TripDistanceCommandHandler: IRequestHandler<Top100TripDistanceCommand, List<TopTripDistanceDTO>>
{
    private ShopContext _context;

    public Top100TripDistanceCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<TopTripDistanceDTO>> Handle(Top100TripDistanceCommand command,CancellationToken cancellationToken)
    {
        var topTripDistances = await _context.datas
            .OrderByDescending(trip => trip.trip_distance)
            .Take(100)
            .Select(data => new TopTripDistanceDTO
            {
                id = data.id,
                trip_distance = data.trip_distance,
            }).ToListAsync();

        return topTripDistances;
    }
}