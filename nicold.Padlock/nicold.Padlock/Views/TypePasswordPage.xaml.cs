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
    public partial class TypePasswordPage : ContentPage
    {
        private readonly TypePasswordViewModel viewModel;

        public TypePasswordPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new TypePasswordViewModel(Navigation);
        }
    }
}