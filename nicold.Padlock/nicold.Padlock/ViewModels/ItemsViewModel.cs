using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;

using nicold.Padlock.Models;
using nicold.Padlock.Views;
using nicold.Padlock.Models.Services;
using System.Windows.Input;
using System.Threading;

namespace nicold.Padlock.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ItemsViewModel(INavigation navigation): base (navigation)
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();

            LoadItemsCommand = new Command(async () => await LoadItemsCommandImplementation());
            SignOutCommand = new Command(async () => await SignOutCommandImplementation());
            SearchCommand = new Command(async () => await SearchCommandImplementation());
            
            searchBarText = "";
            
            MessagingCenter.Subscribe<NewItemPage, Item>(this, Messages.ADDITEM, OnAddItem);
            MessagingCenter.Subscribe<SignInViewModel, string>(this, Messages.SIGNIN, OnSignInSuccessfully);
            MessagingCenter.Subscribe<TypePasswordViewModel, string>(this, Messages.FILEOPEN, OnFileOpened);
            MessagingCenter.Subscribe<ItemsViewModel, string>(this, Messages.SEARCH, OnSearchFiltered);
        }

        #region EVENTS
        private async void OnAddItem(NewItemPage arg1, Item item)
        {
            var newItem = item as Item;
            Items.Add(newItem);
            await DataStore.AddItemAsync(newItem);
        }

        private async void OnSignInSuccessfully(SignInViewModel arg1, string arg2)
        {
            if (IsAuthenticated)
            {
                if (Globals.File == null)
                {
                    Globals.FileEncrypted = await Globals.CloudStorage.GetPadlockFile();
                    await Navigation.PushModalAsync(new NavigationPage(new TypePasswordPage()));
                }
                else
                {

                }
            }
            else
            {
                // not authenticated
            }
        }

        private async void OnSearchFiltered(ItemsViewModel arg1, string arg2)
        {
            await RefreshList();
        }

        private async void OnFileOpened(TypePasswordViewModel arg1, string arg2)
        {
            await RefreshList();
        }
        #endregion

        #region PROPERTIES
        public ObservableCollection<Item> Items { get; set; }
        public bool IsAuthenticated => Globals.AccessToken != null;

        private bool searchBarIsVisible;
        public bool SearchBarIsVisible 
        {
            get { return searchBarIsVisible; }
            set { SetProperty(ref searchBarIsVisible, value); }
        }

        private string searchBarText;
        public string SearchBarText {
            get { return searchBarText; }
            set { SetProperty(ref searchBarText, value); }
        }
        #endregion

        #region COMMANDS
        public Command LoadItemsCommand { get; set; }
        public Command SignOutCommand { get; set; }
        public Command SearchCommand { get; set; }
        public Command ToggleSearchBar => new Command(async () => await ToggleSearchBarCommandImplementation());
        #endregion

        #region COMMANDSIMPLEMENTATION
        private async Task LoadItemsCommandImplementation()
        {
            await RefreshList();
            /*
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }*/
        }

        private async Task SignOutCommandImplementation()
        {
            Globals.AccessToken = null;
            Globals.File = null;
            Globals.FileEncrypted = null;
            Globals.FileReadable = null;
            await Globals.CloudStorage.SignOut();
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
                        string FAV = card.IsFavotire ? "FAVORITE" : "";

                        Items.Add(new Item()
                        {
                            Id = card.Id.ToString(),
                            Text = card.Title,
                            Description = $"used {card.UsedCounter} times",
                            Description2 = FAV
                        }) ;

                        await Task.Delay(40);
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