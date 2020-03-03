using nicold.Padlock.Models.DataFile;
using nicold.Padlock.ViewModelsArtifacts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace nicold.Padlock.ViewModels
{
    public class NewEditItemViewModel : BaseViewModel
    {
        private Card data;

        public NewEditItemViewModel(INavigation navigation, Card item = null) : base(navigation)
        {
            if (item == null)
                item = new Card();

            data = item;
            data.Notes = "ciao";
        }

        #region PROPERTIES
        public string Notes
        {
            get { return data.Notes; }
            set { data.Notes = value; }
        }
        #endregion

        #region COMMANDS
        public Command SaveCommand => new Command(async () => await SaveCommandImplementation());
        public Command CancelCommand => new Command(async () => await CancelCommandImplementation());
        #endregion

        #region COMMANDS_IMPLEMENTATION
        private async Task SaveCommandImplementation()
        {
            MessagingCenter.Send(this, Messages.ADDITEM, this.data);
            await Navigation.PopAsync();
        }

        private async Task CancelCommandImplementation()
        {
            await Navigation.PopAsync();
        }
        #endregion
    }
}
