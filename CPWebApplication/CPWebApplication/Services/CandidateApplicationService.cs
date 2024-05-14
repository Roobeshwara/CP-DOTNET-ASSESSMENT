using CPWebApplication.Interfaces;
using CPWebApplication.Models;
using Microsoft.Azure.Cosmos;

namespace CPWebApplication.Services
{
    public class CandidateApplicationService : ICandidateApplicationService
    {
        private static Container _container;

        public static void Initialize(Container container)
        {
            _container = container;
        }
        public async Task AddCandidateApplicationAsync(CandidateApplication application)
        {
            try
            {
                await _container.CreateItemAsync<CandidateApplication>(application, new PartitionKey(application.FirstName));
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
