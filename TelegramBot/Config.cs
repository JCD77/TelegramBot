using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
   public static  class Config
    {
        static ConfigurationBuilder builder = (ConfigurationBuilder)new ConfigurationBuilder()
   .SetBasePath(Directory.GetCurrentDirectory())
   .AddJsonFile("appsettings.json", optional: false);
        public static IConfiguration config = builder.Build();
    }
}
