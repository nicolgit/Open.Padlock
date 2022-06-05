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
        private Model.Current current;
        private Model.Settings settings;

        public Welcome02ViewModel(Model.Current c, Model.Settings s)
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

        [ICommand]
        async Task LocalFile(string fileName)
        {
            settings.FileName = fileName;
            settings.StorageType = Model.Settings.StorageEnum.STORAGE_LOCAL;

            current.CloudStorage = new Blast.Model.Services.Storage.LocalStorage();
            current.CloudStorage.FileName = fileName;
            current.RawFile = await current.CloudStorage.GetFileAsync();
            current.File = new Models.DataFile.BlastDocument();

            if (current.RawFile == null)
            {
                // creating a new file
                await Shell.Current.GoToAsync($"//{nameof(View.CreatePasswordPage)}?NextPage={nameof(MainPage)}");
            }
            else
            {
                // file exists
                //await Shell.Current.GoToAsync($"//{nameof(View.TypeMainPasswordPage)}?NextPage={nameof(MainPage)}");
            }

        }

        [ICommand]
        async Task OneDrive(string fileName)
        { 
        }
    }
}
