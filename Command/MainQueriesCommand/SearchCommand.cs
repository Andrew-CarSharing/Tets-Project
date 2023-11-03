using MediatR;
using WebApplication5.Context;

namespace WebApplication5.Command;

public class SearchCommand: IRequest<List<Data>>
{
    public short PULocationId { get; set; }

    public SearchCommand(short id)
    {
        PULocationId = id;
    }
}