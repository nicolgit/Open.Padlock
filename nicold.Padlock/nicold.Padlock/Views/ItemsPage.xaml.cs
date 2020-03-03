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

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Item item))
                return;

            viewModel.OnItemSelectedCommand.Execute(item);       

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void SignOut_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Sign out", "Are you sure?", "Yes", "No"))
            {
                viewModel.SignOutCommand.Execute(null);
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