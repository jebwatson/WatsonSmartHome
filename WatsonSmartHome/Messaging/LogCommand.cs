using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace WatsonSmartHome.Messaging
{
    public class LogCommand : IRequest
    {
        
    }
    
    public class LogCommandHandler : IRequestHandler<LogCommand>
    {
        public Task<Unit> Handle(LogCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}