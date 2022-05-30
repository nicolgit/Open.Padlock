using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.ViewModel
{
    public partial class Welcome02ViewModel : ViewModelBase, IQueryAttributable
    {
        [ObservableProperty]
        private string action;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Action = query["Action"] as String;
        }
    }
}
