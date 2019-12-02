using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using nicold.Padlock.Models;
using nicold.Padlock.ViewModels;
using nicold.Padlock.ViewModelsArtifacts;

namespace nicold.Padlock.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        readonly ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        //public ItemDetailPage()
        //{
        //    InitializeComponent();

        //    viewModel = new ItemDetailViewModel(Navigation, item);
        //    BindingContext = viewModel;
        //}
    }
}