using CPWebApplication.Interfaces;
using CPWebApplication.Models;
using Microsoft.Azure.Cosmos;

namespace CPWebApplication.Services
{
    public class EmployerApplicationSerivce : IEmployerApplicationService
    {
        private static Container _container;

        public static void Initialize(Container container)
        {
            _container = container;
        }
        public async Task AddEmployerApplicationAsync(EmployerApplication application)
        {
            try
            {
                await _container.CreateItemAsync<EmployerApplication>(application, new PartitionKey(application.ProgramTitle));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
