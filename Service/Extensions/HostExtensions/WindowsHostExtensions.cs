using Microsoft.Extensions.Hosting;
using Service.WindowsService;
using System;
using System.Threading.Tasks;

namespace Service.Extensions.HostExtensions
{
    public static class WindowsHostExtensions
    {
        public static async Task RunService(this IHostBuilder hostBuilder)
        {
            if (!Environment.UserInteractive)
            {
                await hostBuilder.RunAsServiceAsync();
            }
            else
                await hostBuilder.RunConsoleAsync();
        }
    }
}
