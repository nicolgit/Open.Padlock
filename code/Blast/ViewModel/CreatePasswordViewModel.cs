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
        private Model.Services.PasswordsHelper passwordsHelper;
        public CreatePasswordViewModel(Model.Services.PasswordsHelper ph)
        {
            passwordsHelper = ph;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {

        }

        public string AllowedSymbols => Model.Services.PasswordsHelper.AllowedSymbols;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Complexity))]
        private string newPassword;

        public string Complexity
        {
            get
            {
                return $"{passwordsHelper.BruteForceItearions(newPassword)} iterations";
            }
        }
    }
}
