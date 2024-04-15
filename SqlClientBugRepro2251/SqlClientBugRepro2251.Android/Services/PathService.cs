using Android.Content;
using SqlClientBugRepro2251.Droid.Services;
using SqlClientBugRepro2251.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(PathService))]
namespace SqlClientBugRepro2251.Droid.Services
{
    public class PathService : IPathService
    {
        private readonly Context context = Android.App.Application.Context;

        public string PublicExternalFolder
        {
            get
            {
                var filePath = context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
                return filePath.Path;
            }
        }
    }
}