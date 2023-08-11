using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blast.Model.DataFile;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;

namespace Blast.ViewModel
{
    public partial class MainViewModel:ViewModelBase
    {
        private Model.Services.Current current;
        private Model.Services.Settings settings;
        private UIServices.INavigationService navigationService;

        private List<Row.MainViewModelItem> rows;

        public class OpenSearchBarMessage: ValueChangedMessage<bool>
        {
            public OpenSearchBarMessage(bool isVisible): base (isVisible)
            {
            }
        }

        public MainViewModel(Model.Services.Current c, Model.Services.Settings s, UIServices.INavigationService n)
        {
            current = c;
            settings = s;
            navigationService = n;
        }

        internal void Initialize()
        {
            LoadCards();
        }

        public List<Row.MainViewModelItem> Rows => rows;

        [ObservableProperty]
        private bool searchBarIsVisible;

        [ObservableProperty]
        private string searchBarText;

        [RelayCommand]
        void LoadCards()
        {
            rows = new List<Row.MainViewModelItem>();

            foreach (var card in current.Document.Cards)
            {
                if (!SearchBarIsVisible || string.IsNullOrWhiteSpace(SearchBarText))
                {
                    rows.Add(new Row.MainViewModelItem(card));
                }
                else
                {
                    switch (card.AdvancedSearch(SearchBarText))
                    {
                        case Card.AdvancedSearchResult.InBody:
                            rows.Add(new Row.MainViewModelItem(card));
                            break;
                        case Card.AdvancedSearchResult.InTitle:
                            rows.Insert(0, new Row.MainViewModelItem(card));
                            break;
                        default:
                            break;
                    }
                }
            }

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

        [RelayCommand]
        async Task Open(Row.MainViewModelItem selectedRow)
        {
            current.Card = selectedRow.model;
            await navigationService.GoToViewModelAsync(nameof(CardViewViewModel));
        }

        [RelayCommand]
        void ToggleSearchBar()
        {
            SearchBarIsVisible = !SearchBarIsVisible;

            WeakReferenceMessenger.Default.Send(new OpenSearchBarMessage(SearchBarIsVisible));
        }
    }
}
