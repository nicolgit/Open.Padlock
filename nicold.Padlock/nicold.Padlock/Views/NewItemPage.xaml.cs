using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using nicold.Padlock.Models;
using nicold.Padlock.ViewModels;
using nicold.Padlock.ViewModelsArtifacts;
using nicold.Padlock.Models.Services;

namespace nicold.Padlock.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        readonly NewEditItemViewModel viewModel;

        public NewItemPage(NewEditItemViewModel viewModel)
        {
            BindingContext = this.viewModel = viewModel;
            
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // https://github.com/xamarin/Xamarin.Forms/issues/2094
            Device.BeginInvokeOnMainThread(async () =>
            {
                await System.Threading.Tasks.Task.Delay(250);
                entryTitle.Focus();
            });
        }
        void Save_Clicked(object sender, EventArgs e)
        {
            viewModel.SaveCommand.Execute(null);
        }

        void Cancel_Clicked(object sender, EventArgs e)
        {
            viewModel.CancelCommand.Execute(null);
        }
    }
}