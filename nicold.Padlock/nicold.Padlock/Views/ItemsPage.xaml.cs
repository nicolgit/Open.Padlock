using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using nicold.Padlock.Models;
using nicold.Padlock.Views;
using nicold.Padlock.ViewModels;
using nicold.Padlock.ViewModelsArtifacts;

namespace nicold.Padlock.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        readonly ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel(Navigation);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Item item))
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(Navigation, item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        async void SignOut_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Sign out", "Are you sure?", "Yes", "No"))
            {
                viewModel.SignOutCommand.Execute(null);
                //TODO https://github.com/xamarin/Xamarin.Forms/issues/6697  BUG su Navigation Page in uscita da AppShell
                App.Current.MainPage = new NavigationPage(new SignIn());
            }
        }

        void ShowSearchBar_Clicked(object sender, EventArgs e)
        {
            viewModel.ToggleSearchBar.Execute(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<ItemsViewModel, string>(viewModel, Messages.SEARCHOPEN, OnSearchOpen);
        }

        private void OnSearchOpen(ItemsViewModel arg1, string arg2)
        {
            searchBar.Focus();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<ItemsViewModel>(viewModel, Messages.SEARCHOPEN);
            base.OnDisappearing();
        }
        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.SearchCommand.Execute(this);
        }

        private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            viewModel.SearchCommand.Execute(this);
        }
    }
}