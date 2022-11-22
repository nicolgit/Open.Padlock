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

        [RelayCommand]
        async Task New()
        {
            await Shell.Current.GoToAsync($"//{nameof(View.Welcome02Page)}?Action=new");
        }

        [RelayCommand]
        async Task Existing()
        {
            await Shell.Current.GoToAsync($"//{nameof(View.Welcome02Page)}?Action=open");
        }
    }
}
