using ChildBehaviour.UI.Models;
using ChildBehaviour.UI.Tabs;
using ChildBehaviour.UI.Tabs.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildBehaviour.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var service = new ServiceCollection();
            ConfigureServices(service);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (ServiceProvider serviceProvider = service.BuildServiceProvider())
            {
                var frmLogin = serviceProvider.GetRequiredService<FrmLogin>();
                Application.Run(frmLogin);
            }
        }


        private static void ConfigureServices(IServiceCollection serviceCollection)
        {

            serviceCollection.AddOptions();
            serviceCollection.AddScoped<FrmLogin>();
            serviceCollection.AddScoped<CurrentUser>();
            serviceCollection.AddTransient<IConfigTab, ConfigTab>();
            serviceCollection.AddTransient<ITabCommons, TabCommons>();
            serviceCollection.AddTransient<IDecisionTableTab, DecisionTableTab>();
            serviceCollection.AddTransient<IRecommendationsTab, RecommendationsTab>();
            serviceCollection.AddTransient<IPupilsTab, PupilsTab>();
            serviceCollection.AddTransient<ICheckListTab, CheckListTab>();
        }
    }
}
