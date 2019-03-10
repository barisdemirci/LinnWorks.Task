using LinnWorks.Task.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace LinnWorks.Task.Repositories
{
    public sealed class RepositoriesRegistration : IServiceRegistration
    {
        private readonly IServiceRegistration[] serviceRegistrations =
        {
        };

        public void Register(IServiceCollection services)
        {
            foreach (IServiceRegistration registration in serviceRegistrations)
            {
                registration.Register(services);
            }
        }
    }
}