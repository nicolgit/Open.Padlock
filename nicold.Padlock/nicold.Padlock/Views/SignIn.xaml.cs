using nicold.Padlock.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace nicold.Padlock.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignIn : ContentPage
    {
        private readonly SignInViewModel viewModel;

        public SignIn()
        {
            InitializeComponent();
            BindingContext = viewModel = new SignInViewModel();
            viewModel.Navigation = Navigation;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            
        }

    }
}