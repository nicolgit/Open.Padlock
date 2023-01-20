using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.UIServices
{
    public interface INavigationService
    {
        Task GoToViewModelAsync(string viewmodelName);

        Task GoToAsync (ShellNavigationState navigationState);
    }

    internal class NavigationService: INavigationService
    {
        public Task GoToAsync (ShellNavigationState navigationState)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Code to run on the main thread
                Shell.Current.GoToAsync(navigationState);
            });
            return Task.CompletedTask;
        }

        public Task GoToViewModelAsync(string viewmodelName)
        {
            return GoToAsync($"//{viewmodelName.Replace("ViewModel","Page")}");
        }

    }
}
