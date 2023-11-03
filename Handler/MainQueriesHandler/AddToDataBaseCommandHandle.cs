using AutoMapper;
using MediatR;
using WebApplication5.Command;
using WebApplication5.Context;
using WebApplication5.DBContext;

namespace WebApplication5.Handler;

public class AddToDataBaseCommandHandle: IRequestHandler<AddToDataBaseCommand, string>
{
    private ShopContext _context;
    private IMapper _mapper;

    public AddToDataBaseCommandHandle(ShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> Handle(AddToDataBaseCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var trip = _mapper.Map<Data>(command._model);
            
            await _context.datas.AddAsync(trip);

            await _context.SaveChangesAsync();

            return "Successful!";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}