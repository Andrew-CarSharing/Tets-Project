using MediatR;
using WebApplication5.DTO;

namespace WebApplication5.Command;

public class Top100TripDistanceCommand: IRequest<List<TopTripDistanceDTO>>
{
    
}