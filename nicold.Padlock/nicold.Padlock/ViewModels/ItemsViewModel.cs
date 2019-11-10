using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using nicold.Padlock.Models;
using nicold.Padlock.Views;
using nicold.Padlock.Models.Services;
using System.Windows.Input;

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

            MessagingCenter.Subscribe<NewItemPage, Item>(this, Messages.ADDITEM, OnAddItem);
            MessagingCenter.Subscribe<SignInViewModel, string>(this, Messages.SIGNIN, OnSignInSuccessfully);
            MessagingCenter.Subscribe<TypePasswordViewModel, string>(this, Messages.FILEOPEN, OnFileOpened);
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

        private async void OnFileOpened(TypePasswordViewModel arg1, string arg2)
        {
            //await DataStore.ClearAllItemAsync();
            Items.Clear();

            foreach (var card in Globals.File.Cards)
            {
                string FAV = card.IsFavotire ? "FAVORITE" : "";

                //await DataStore.AddItemAsync(new Item()
                //{
                //    Id = card.Id.ToString(),
                //    Text = card.Title,
                //    Description= $"used {card.UsedCounter} - {FAV}"
                //});

                Items.Add(new Item()
                {
                    Id = card.Id.ToString(),
                    Text = card.Title,
                    Description = $"used {card.UsedCounter} - {FAV}"
                });
            }
        }
        #endregion
        #region PROPERTIES
        public ObservableCollection<Item> Items { get; set; }
        public bool IsAuthenticated => Globals.AccessToken != null;
        #endregion

        #region COMMANDS
        public Command LoadItemsCommand { get; set; }
        public Command SignOutCommand { get; set; }
        #endregion

        #region COMMANDSIMPLEMENTATION
        private async Task LoadItemsCommandImplementation()
        {
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
            }
        }

        private async Task SignOutCommandImplementation()
        {
            Globals.AccessToken = null;
            Globals.File = null;
            Globals.FileEncrypted = null;
            Globals.FileReadable = null;
            await Globals.CloudStorage.SignOut();
        }

        #endregion
    }
}