using nicold.Padlock.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace nicold.Padlock.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        readonly AboutViewModel viewModel;

        public AboutPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new AboutViewModel(Navigation);
        }
    }
}