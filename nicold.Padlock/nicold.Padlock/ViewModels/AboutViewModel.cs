using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace nicold.Padlock.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel(INavigation navigation): base (navigation)
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }
    }
}