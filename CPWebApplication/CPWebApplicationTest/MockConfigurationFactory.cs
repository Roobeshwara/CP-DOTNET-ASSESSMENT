using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPWebApplicationTest
{
    public class MockConfigurationFactory
    {
        public static IConfiguration CreateMockConfiguration(Dictionary<string, string> settings)
        {
            var configuration = new Mock<IConfiguration>();

            // Setup configuration values from dictionary
            configuration.Setup(x => x[It.IsAny<string>()]).Returns<string>(key => settings.ContainsKey(key) ? settings[key] : null);

            return configuration.Object;
        }
    }
}
