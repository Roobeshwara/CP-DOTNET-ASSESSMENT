using CPWebApplication.Models;

namespace CPWebApplication.Interfaces
{
    public interface IEmployerApplicationService
    {
        Task AddEmployerApplicationAsync(EmployerApplication application);
    }
}
