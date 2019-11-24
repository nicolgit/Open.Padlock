using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace nicold.Padlock.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel(INavigation navigation): base (navigation)
        {
            Title = "About";
            OpenWebCommand = new Command(() => Launcher.OpenAsync("https://xamarin.com/platform"));
        }

        public string Version => AppInfo.VersionString;
        public string Build => AppInfo.BuildString;

        public ICommand OpenWebCommand { get; }
    }
}