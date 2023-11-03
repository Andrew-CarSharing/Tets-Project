using MediatR;
using WebApplication5.Command;
using WebApplication5.DBContext;

namespace WebApplication5.Handler;

public class ChangeNAndYCommandHandler: IRequestHandler<ChangeNAndYCommand,string>
{
    private ShopContext _context;

    public ChangeNAndYCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(ChangeNAndYCommand command, CancellationToken cancellationToken)
    {
        try
        {
            foreach (var trip in _context.datas)
            {
                if (trip.store_and_fwd_flag == "N")
                {
                    trip.store_and_fwd_flag = "No";
                }
                else if (trip.store_and_fwd_flag == "Y")
                {
                    trip.store_and_fwd_flag = "Yes";
                }
            }

            await _context.SaveChangesAsync();

            return "Successful!";
        }
        catch (Exception ex)
        {
            return ex.Message ;
        }
    }
}