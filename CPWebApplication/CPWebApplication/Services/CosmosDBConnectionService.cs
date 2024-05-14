using Microsoft.Azure.Cosmos;

namespace CPWebApplication.Services
{
    public class CosmosDBConnectionService
    {
        //Cosmos Credentials
        static readonly string cosmosUri = AppSettings.CosmosUri;
        static readonly string primaryKey = AppSettings.PrimaryKey;

        // The Cosmos client instance
        static private CosmosClient cosmosClient;

        // The database
        static private Database database;

        // The container
        static public Container EmployerApplicationContainer;
        static public Container CandidateApplicationContainer;

        // The name of the database
        static private string databaseId = "ApplicationManagementDB";

        // The name of required containers
        static private string employerApplicationContainerId = "EmployerApplication";
        static private string candidateApllicationContainerId = "CandidateApplication";
        public static async Task GetStartedCosmosDBAsync()
        {
            // Create a new instance of the Cosmos Client
            cosmosClient = new CosmosClient(cosmosUri, primaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
            await CreateDatabaseAsync();
            await CreateContainersAsync();
            await ScaleContainersAsync();
            Console.WriteLine("DB and Containers are created!");
        }

        private static async Task CreateDatabaseAsync()
        {
            // Create a new database if dosn't exist
            database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        }
        private static async Task CreateContainersAsync()
        {
            // Create a new containers
            EmployerApplicationContainer = await database.CreateContainerIfNotExistsAsync(employerApplicationContainerId, "/ProgramTitle", 400);
            CandidateApplicationContainer = await database.CreateContainerIfNotExistsAsync(candidateApllicationContainerId, "/FirstName", 400);
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
