using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Command;
using WebApplication5.Context;
using WebApplication5.DBContext;

namespace WebApplication5.Handler;

public class SearchCommandHandle: IRequestHandler<SearchCommand, List<Data>>
{
    private ShopContext _context;

    public SearchCommandHandle(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<Data>> Handle(SearchCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var searchResults = await _context.datas
                .Where(data => (command.PULocationId == 0 || data.PULocationID == command.PULocationId))
                .ToListAsync();
            
            return searchResults;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}