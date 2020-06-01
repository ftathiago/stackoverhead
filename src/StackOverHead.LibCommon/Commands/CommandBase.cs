using MediatR;

namespace StackOverHead.LibCommon.Commands
{
    public class CommandBase<T> : IRequest<T> where T : class { }
}