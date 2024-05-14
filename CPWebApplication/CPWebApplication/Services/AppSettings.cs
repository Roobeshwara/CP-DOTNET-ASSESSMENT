namespace CPWebApplication.Services
{
    public static class AppSettings
    {
        private static string _cosmosUri;
        private static string _primaryKey;
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
            _cosmosUri = _configuration["CosmosCredentials:EndpointUri"];
            _primaryKey = _configuration["CosmosCredentials:PrimaryKey"];
        }

        public static string CosmosUri => _cosmosUri;
        public static string PrimaryKey => _primaryKey;
    }
}
