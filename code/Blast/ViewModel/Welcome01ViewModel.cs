using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.ViewModel
{
    public partial class Welcome01ViewModel: ViewModelBase
    {

        [ICommand]
        async Task New()
        {
            await Shell.Current.GoToAsync($"//{nameof(View.Welcome02Page)}?Action=new");
        }

        [ICommand]
        async Task Existing()
        {
            await Shell.Current.GoToAsync($"//{nameof(View.Welcome02Page)}?Action=open");
        }
    }
}
