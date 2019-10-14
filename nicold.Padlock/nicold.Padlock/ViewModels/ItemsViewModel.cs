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

            MessagingCenter.Subscribe<NewItemPage, Item>(this, Messages.ADDITEM, async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            MessagingCenter.Subscribe<SignInViewModel, string>(this, Messages.SIGNIN, async (obj, item) =>
            {
                if (IsAuthenticated)
                {
                    Globals.FileContent = await Globals.CloudStorage.GetPadlockFile();
                    await Navigation.PushModalAsync(new NavigationPage(new TypePasswordPage()));
                }
                else
                {

                }

            });
        }

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
            await Globals.CloudStorage.SignOut();
        }

        #endregion
    }
}