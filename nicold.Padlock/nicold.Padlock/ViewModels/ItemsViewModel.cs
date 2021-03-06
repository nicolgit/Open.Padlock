﻿using nicold.Padlock.Models;
using nicold.Padlock.Models.DataFile;
using nicold.Padlock.ViewModelsArtifacts;
using nicold.Padlock.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace nicold.Padlock.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ItemsViewModel(INavigation navigation): base (navigation)
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            
            searchBarText = "";
            
            MessagingCenter.Subscribe<NewEditItemViewModel, Card>(this, Messages.ADDITEM, OnItemAdded);
            MessagingCenter.Subscribe<ItemsViewModel, string>(this, Messages.SEARCH, OnSearchFiltered);
        }

        #region EVENTS
        private async void OnItemAdded(NewEditItemViewModel arg1, Card item)
        {
            Globals.File.Cards.Add(item);

            await RefreshList();
        }

        private async void OnSearchFiltered(ItemsViewModel arg1, string arg2)
        {
            await RefreshList();
        }
        #endregion

        #region PROPERTIES
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy=value; RaisePropertyChanged(() => IsBusy); }
        }

        private ObservableCollection<Item> items;
        public ObservableCollection<Item> Items
        {
            get { return items; }
            set { items = value; RaisePropertyChanged(() => Items); }
        }

        public bool IsAuthenticated => Globals.AccessToken != null;

        private bool searchBarIsVisible;
        public bool SearchBarIsVisible 
        {
            get { return searchBarIsVisible; }
            set { searchBarIsVisible = value; RaisePropertyChanged(() => SearchBarIsVisible); }
        }

        private string searchBarText;
        public string SearchBarText {
            get { return searchBarText; }
            set { searchBarText = value; RaisePropertyChanged(() => SearchBarText); }
        }
        #endregion

        #region COMMANDS
        public Command SignOutCommand => new Command(async () => await SignOutCommandImplementation());
        public Command SearchCommand => new Command(async () => await SearchCommandImplementation());
        public Command AddCommand => new Command(async () => await AddCommandImplementation());
        public Command OnItemSelectedCommand => new Command<Item>(async (item) => await OnItemSelectedCommandImplementation(item));
        public Command ToggleSearchBar => new Command(async () => await ToggleSearchBarCommandImplementation());
        #endregion

        #region COMMANDSIMPLEMENTATION
        private async Task SignOutCommandImplementation()
        {
            Globals.AccessToken = null;
            Globals.File = null;
            Globals.FileEncrypted = null;
            Globals.FileReadable = null;
            await Globals.CloudStorage.SignOut();

            // reset the main navigation to login page
            //await Application.Current.MainPage.Navigation.PopToRootAsync();
            Application.Current.MainPage = new NavigationPage(new SignIn());
        }

        private async Task SearchCommandImplementation()
        {
            MessagingCenter.Send(this, Messages.SEARCH, "");

            await Task.Delay(100);
        }

        private async Task ToggleSearchBarCommandImplementation()
        {
            SearchBarIsVisible = !SearchBarIsVisible;
            if (SearchBarIsVisible) 
                MessagingCenter.Send(this, Messages.SEARCHOPEN, "");

            await Task.Delay(100);
        }
        
        private async Task AddCommandImplementation()
        {
            await Navigation.PushAsync(new NewItemPage(new NewEditItemViewModel(Navigation)));
        }

        private async Task OnItemSelectedCommandImplementation(Item item)
        {
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(Navigation, item)));
        }

        #endregion

        #region PRIVATE
        private bool stopLoading;
        private SemaphoreSlim semaphoreList = new SemaphoreSlim(1, 1);
        private async Task RefreshList()
        {
            stopLoading = true;

            await semaphoreList.WaitAsync();
            try
            {
                stopLoading = false;
                IsBusy = true;
                Items.Clear();

                string []filter = searchBarText.Trim().Split(' '); 
                filter = filter.Length > 0 && filter[0]!="" ? filter : null;

                foreach (var card in Globals.File.Cards.OrderBy(a => a.Title))
                {
                    // https://stackoverflow.com/questions/500925/check-if-a-string-contains-an-element-from-a-list-of-strings
                    if ( filter == null || filter.All(s => card.AdvancedCompare(s)) ) 
                    {
                        Items.Add(new Item(card)) ;
                        await Task.Delay(10);
                    }

                    if (stopLoading) break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                semaphoreList.Release();
            }
        }
        #endregion
    }
}