using Microsoft.Extensions.DependencyInjection;

namespace LinnWorks.Task.Core.DependencyInjection
{
    public interface IServiceRegistration
    {
        void Register(IServiceCollection services);
    }
}