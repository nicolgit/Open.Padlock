﻿using nicold.Padlock.ViewModels;
using nicold.Padlock.ViewModelsArtifacts;
using System;
using System.ComponentModel;
using Xamarin.Forms;

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
            // https://github.com/xamarin/Xamarin.Forms/issues/2094
            Device.BeginInvokeOnMainThread(async () =>
            {
                await System.Threading.Tasks.Task.Delay(250);
                searchBar.Focus();
            });
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