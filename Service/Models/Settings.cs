using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Models
{
    public class Settings
    {
        private readonly IConfiguration configuration;

        public Settings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public int WorkersCount => configuration.GetValue<int>("WorkersCount");
        public int RunInterval => configuration.GetValue<int>("RunInterval");
        public string InstanceName => configuration.GetValue<string>("name");
        public string ResultPath => configuration.GetValue<string>("ResultPath");
    }
}
