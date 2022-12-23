using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.ViewModel
{
    public partial class Welcome02ViewModel : ViewModelBase, IQueryAttributable
    {

/* Unmerged change from project 'Blast (net6.0-windows10.0.19041.0)'
Before:
        private Model.Current current;
After:
        private Current current;
*/
        private Model.Services.Current current;

/* Unmerged change from project 'Blast (net6.0-windows10.0.19041.0)'
Before:
        private Model.Settings settings;
After:
        private Settings settings;
*/
        private Model.Services.Settings settings;


/* Unmerged change from project 'Blast (net6.0-windows10.0.19041.0)'
Before:
        public Welcome02ViewModel(Model.Current c, Model.Settings s)
After:
        public Welcome02ViewModel(Model.Current c, Settings s)
*/

/* Unmerged change from project 'Blast (net6.0-windows10.0.19041.0)'
Before:
        public Welcome02ViewModel(Model.Current c, Model.Services.Settings s)
After:
        public Welcome02ViewModel(Current c, Model.Services.Settings s)
*/
        public Welcome02ViewModel(Model.Services.Current c, Model.Services.Settings s)
        {
            current = c;
            settings = s;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Action = query["Action"] as String;
        }

        [ObservableProperty]
        private string action;

        [RelayCommand]
        async Task LocalFile(string fileName)
        {
            settings.FileName = fileName;
            settings.StorageType = Model.Services.Settings.StorageEnum.STORAGE_LOCAL;

            current.CloudStorage = new Blast.Model.Services.Storage.LocalStorage();
            current.CloudStorage.FileName = fileName;
            current.RawFile = await current.CloudStorage.GetFileAsync();
            current.File = new Models.DataFile.BlastDocument();

            if (current.RawFile == null)
            {
                // creating a new file
                await Shell.Current.GoToAsync($"//{nameof(View.CreatePasswordPage)}?NextPage={nameof(View.MainPage)}");
            }
            else
            {
                // file exists
                //await Shell.Current.GoToAsync($"//{nameof(View.TypeMainPasswordPage)}?NextPage={nameof(MainPage)}");
            }

        }

        [RelayCommand]
        async Task OneDrive(string fileName)
        { 
        }
    }
}
