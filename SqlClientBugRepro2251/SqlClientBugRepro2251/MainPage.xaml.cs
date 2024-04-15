using SqlClientBugRepro2251.Services;
using System;
using System.IO;
using System.Reflection;
using Workers.Abstractions;
using Xamarin.Forms;

namespace SqlClientBugRepro2251
{

    public partial class MainPage : ContentPage
    {
        private readonly string _path;

        public MainPage()
        {
            InitializeComponent();
            _path = DependencyService.Get<IPathService>().PublicExternalFolder;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var workersPath = Path.Combine(_path, "workers");

            Directory.CreateDirectory(workersPath);

            var workerPath = Path.Combine(workersPath, "sqlclient", "Workers.SqlClient.dll");
            //var workerPath = Path.Combine(workersPath, "sqlclientnewer", "Workers.SqlClientNewer.dll");
            if (!File.Exists(workerPath))
            {
                Result.Text = $"Assembly '{workerPath}' not found";
                return;
            }

            try
            {
                var workerAssembly = Assembly.LoadFrom(workerPath);
                var workerType = workerAssembly.GetType("Workers.SqlClient.Worker");

                IWorker worker = (IWorker)Activator.CreateInstance(workerType);

                Result.Text = await worker.RunAsync();
            }
            catch (Exception ex)
            {
                Result.Text = $"Error ({ex.GetType()}): {ex.Message}";
            }
        }
    }
}
