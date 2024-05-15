using Microsoft.Azure.Cosmos;

namespace CPWebApplication.Services
{
    public class CosmosDBConnectionService
    {
        //Cosmos Credentials
        static private string? cosmosUri;
        static private string? primaryKey;

        // The Cosmos client instance
        static private CosmosClient? cosmosClient;

        // The database
        static private Database? database;

        // The container
        static public Container? EmployerApplicationContainer;
        static public Container? CandidateApplicationContainer;

        // The name of the database
        static private string databaseId = "ApplicationManagementDB";

        // The name of required containers
        static private string employerApplicationContainerId = "EmployerApplication";
        static private string candidateApllicationContainerId = "CandidateApplication";
        public static async Task GetStartedCosmosDBAsync(IConfiguration configuration)
        {

            try
            {
                // Initialize Cosmos client
                cosmosUri = configuration["CosmosCredentials:EndpointUri"];
                primaryKey = configuration["CosmosCredentials:PrimaryKey"];
                cosmosClient = new CosmosClient(cosmosUri, primaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });

                // Create database and containers
                await CreateDatabaseAsync();
                await CreateContainersAsync();
                await ScaleContainersAsync();
                Console.WriteLine("DB and Containers are created!");
            }
            catch (CosmosException ex)
            {
                Console.WriteLine($"Cosmos Exception: {ex.StatusCode} - {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Exception: {ex.Message}");
                throw;
            }
        }

        private static async Task CreateDatabaseAsync()
        {
            // Create a new database if dosn't exist
            database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        }
        private static async Task CreateContainersAsync()
        {
            // Create a new containers
            EmployerApplicationContainer = await database.CreateContainerIfNotExistsAsync(employerApplicationContainerId, "/id", 400);
            CandidateApplicationContainer = await database.CreateContainerIfNotExistsAsync(candidateApllicationContainerId, "/id", 400);
        }
        private static async Task ScaleContainersAsync()
        {
            // Read the current throughput
            int? throughput = await EmployerApplicationContainer.ReadThroughputAsync();
            if (throughput.HasValue)
            {
                int newThroughput = throughput.Value + 100;
                // Update throughput
                await EmployerApplicationContainer.ReplaceThroughputAsync(newThroughput);
            }
            int? throughput2 = await CandidateApplicationContainer.ReadThroughputAsync();
            if (throughput2.HasValue)
            {
                int newThroughput = throughput2.Value + 100;
                // Update throughput
                await CandidateApplicationContainer.ReplaceThroughputAsync(newThroughput);
            }
        }
    }
}
