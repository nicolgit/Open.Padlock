using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;

namespace Blast.ViewModel
{
    public partial class CardViewViewModel : ViewModelBase
    {
        private UIServices.IDialogService dialogService;
        private Model.Services.Settings settings;
        private Model.Services.Current current;
        private UIServices.INavigationService navigationService;

        public string Title => current.Card.Title;
        public string Notes => current.Card.Notes;
        public ObservableCollection<Row.CardViewViewModelItem> Attributes {get;set;}

        public CardViewViewModel(Model.Services.Current c, Model.Services.Settings s, UIServices.INavigationService n, UIServices.IDialogService ds)
        {
            current = c;    
            settings = s;
            navigationService = n;
            dialogService = ds;
        }

        internal void Initialize()
        {
            Attributes = new ObservableCollection<Row.CardViewViewModelItem>();
            foreach(var a in current.Card.Rows)
            {
                Attributes.Add(new Row.CardViewViewModelItem(a));
            }

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

        [RelayCommand]
        void ToggleShowPassword(Row.CardViewViewModelItem item)
        {
            item.PasswordIsVisible = !item.PasswordIsVisible;
            //Attributes.Add(new Row.CardViewViewModelItem(new Model.DataFile.Attribute() { Name="pippo", Value="pluto", Type=Model.DataFile.AttributeType.TYPE_STRING}))
        }

        [RelayCommand]
        async Task CopyText(Row.CardViewViewModelItem item)
        {
            await Clipboard.Default.SetTextAsync(item.Model.Value);

            await dialogService.DisplayAlertAsync("", $"text:{item.MainText} copied to clipboard", "OK");
        }
    }

}
