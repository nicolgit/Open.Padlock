using nicold.Padlock.Models.DataFile;
using nicold.Padlock.ViewModelsArtifacts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            ItemDetailEditRows = new ObservableCollection<ItemDetailEditRow>();
            foreach (var rowModel in data.Rows)
            {
                ItemDetailEditRows.Add(new ItemDetailEditRow(rowModel));
            }
        }

        #region PROPERTIES
        new public string Title
        {
            get { return data.Title; }
            set { data.Title = value; }
        }
        public string Notes
        {
            get { return data.Notes; }
            set { data.Notes = value; }
        }
        ObservableCollection<ItemDetailEditRow> itemDetailEditRows;
        public ObservableCollection<ItemDetailEditRow> ItemDetailEditRows
        {
            get { return itemDetailEditRows; }
            set { SetProperty(ref itemDetailEditRows, value); }
        }
        #endregion

        #region COMMANDS
        public Command SaveCommand => new Command(async () => await SaveCommandImplementation());
        public Command CancelCommand => new Command(async () => await CancelCommandImplementation());
        public Command<string> AddRowCommand => new Command<string>(async (type) => await AddRowCommandImplementation(type));
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
        private async Task AddRowCommandImplementation(string type)
        {
            await Task.Delay(10);

            Models.DataFile.Attribute row = new Models.DataFile.Attribute(AttributeType.TYPE_STRING);

            ItemDetailEditRows.Add(new ItemDetailEditRow(row));
        }
        #endregion
    }
}
