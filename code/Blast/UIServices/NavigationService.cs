using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.UIServices
{
    public interface INavigationService
    {
        Task GoToAsync (ShellNavigationState navigationState);
    }

    internal class NavigationService: INavigationService
    {
        public Task GoToAsync (ShellNavigationState navigationState)
        {
            return Shell.Current.GoToAsync(navigationState);
        }
    }
}
