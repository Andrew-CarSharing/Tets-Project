using MediatR;
using WebApplication5.Model;

namespace WebApplication5.Command;

public class AddToDataBaseCommand: IRequest<string>
{
    public InputDataModel _model { get; set; }

    public AddToDataBaseCommand(InputDataModel model)
    {
        _model = model;
    }
}