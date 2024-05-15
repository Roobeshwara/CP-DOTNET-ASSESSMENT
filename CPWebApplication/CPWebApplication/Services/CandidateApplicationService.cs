using CPWebApplication.Interfaces;
using CPWebApplication.Models;
using Microsoft.Azure.Cosmos;

namespace CPWebApplication.Services
{
    public class CandidateApplicationService : ICandidateApplicationService
    {
        private static Container _container;

        public CandidateApplicationService(IConfiguration configuration)
        {
            InitializeContainerAsync(configuration).Wait(); // Call an async method synchronously
        }

        private async Task InitializeContainerAsync(IConfiguration configuration)
        {
            await CosmosDBConnectionService.GetStartedCosmosDBAsync(configuration);
            _container = CosmosDBConnectionService.CandidateApplicationContainer;
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
