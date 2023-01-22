using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Graph;

namespace Blast.ViewModel
{
    public partial class MainViewModel:ViewModelBase
    {
        private Model.Services.Current current;
        private Model.Services.Settings settings;
        private UIServices.INavigationService navigationService;

        public MainViewModel(Model.Services.Current c, Model.Services.Settings s, UIServices.INavigationService n)
        {
            current = c;
            settings = s;
            navigationService = n;
        }

        internal void Initialize()
        {
            loadCards();
        }

        public List<Blast.Model.DataFile.Card> Rows => current.Document.Cards;


        private void loadCards()
        {
            //TODO implemets CardViewModel

            OnPropertyChanged(nameof(Rows));
        }

        [RelayCommand]
        async Task SignOut()
        {
            settings.FileName = "";
            settings.StorageType = Model.Services.Settings.StorageEnum.NONE;
            settings.SaveAll();

            current.CloudStorage = null;
            current.Document = new Model.DataFile.BlastDocument();
            current.File = new Model.DataFile.BlastFile();
            
            await navigationService.GoToViewModelAsync(nameof(WelcomeSelectStorageViewModel));
        }
    }
}
