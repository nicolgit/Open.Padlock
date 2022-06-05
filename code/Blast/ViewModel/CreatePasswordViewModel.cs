using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.ViewModel
{
    public partial class CreatePasswordViewModel: ViewModelBase, IQueryAttributable
    {
        public CreatePasswordViewModel()
        {

        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {

        }

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Complexity))]
        private string newPassword;

        public string Complexity
        {
            get
            {
                return $"123{newPassword}123 2.3 seconds";
            }
        }
    }
}
