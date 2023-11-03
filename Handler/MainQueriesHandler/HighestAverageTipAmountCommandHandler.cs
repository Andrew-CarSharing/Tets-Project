using MediatR;
using WebApplication5.Command;
using WebApplication5.DBContext;
using WebApplication5.Model;

namespace WebApplication5.Handler;

public class HighestAverageTipAmountCommandHandler: IRequestHandler<HighestAverageTipAmountCommand, AverageTipModel>
{
    private ShopContext _context;

    public HighestAverageTipAmountCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<AverageTipModel> Handle(HighestAverageTipAmountCommand command, CancellationToken cancellationToken)
    {
        var result = _context.datas
            .GroupBy(d => d.PULocationID)
            .Select(g => new AverageTipModel
            {
                PULocationId = g.Key,
                AverageTipAmount = g.Average(d => d.tip_amount)
            })
            .OrderByDescending(x => x.AverageTipAmount)
            .FirstOrDefault();

        if (result != null)
        {
            return result;
        }
        return null; 
    }
}