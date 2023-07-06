using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.ViewModel
{
    public partial class CardViewViewModel : ViewModelBase
    {
        private Model.Services.Settings settings;
        private Model.Services.Current current;
        private UIServices.INavigationService navigationService;

        public string Title => current.Card.Title;
        public string Notes => current.Card.Notes;
        public List<BlastItem> Attributes {get;set;}

        public CardViewViewModel(Model.Services.Current c, Model.Services.Settings s, UIServices.INavigationService n)
        {
            current = c;    
            settings = s;
            navigationService = n;
        }

        internal void Initialize()
        {
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Notes));
            OnPropertyChanged(nameof(Attributes));
        }


        [RelayCommand]
        async Task Close()
        {
            current.Card = null;
            await navigationService.GoToViewModelAsync(nameof(MainViewModel));
        }
    }
}
