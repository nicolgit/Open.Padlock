using nicold.Padlock.Models.DataFile;
using nicold.Padlock.Validators;
using nicold.Padlock.ViewModelsArtifacts;
using System;
using System.Linq;
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

            title.Value = data.Title;
            title.Validations.Add(new IsNotNullOrEmptyRule<string>()
            {
                ValidationMessage = "Card name can not be empty"
            });

            ItemDetailEditRows = new ObservableCollection<ItemDetailEditRow>();
            foreach (var rowModel in data.Rows)
            {
                ItemDetailEditRows.Add(new ItemDetailEditRow(rowModel));
            }
        }

        #region PROPERTIES
        private ValidatableObject<string> title = new ValidatableObject<string>();
        new public ValidatableObject<string> Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(() => Title); }
        }

        private string titleError;
        public string TitleError
        {
            get { return titleError; }
            set { titleError = value; RaisePropertyChanged(() => TitleError); }
        }

        public string Notes
        {
            get { return data.Notes; }
            set { data.Notes = value; RaisePropertyChanged(() => Notes); }
        }
        ObservableCollection<ItemDetailEditRow> itemDetailEditRows;
        public ObservableCollection<ItemDetailEditRow> ItemDetailEditRows
        {
            get { return itemDetailEditRows; }
            set { itemDetailEditRows=value; RaisePropertyChanged(() => ItemDetailEditRows); }
        }
        #endregion

        #region COMMANDS
        public Command SaveCommand => new Command(async () => await SaveCommandImplementation());
        public Command CancelCommand => new Command(async () => await CancelCommandImplementation());
        public Command AddHeaderRowCommand => new Command(async () => await AddHeaderRowCommandImplementation());
        public Command AddAttributeRowCommand => new Command(async () => await AddAttributeRowCommandImplementation());
        public Command AddURLRowCommand => new Command(async () => await AddURLRowCommandImplementation());
        public Command AddPasswordRowCommand => new Command(async () => await AddPasswordRowCommandImplementation());
        #endregion

        #region COMMANDS_IMPLEMENTATION
        private async Task SaveCommandImplementation()
        {
            if (!title.Validate())
            {
                TitleError = title.Errors.FirstOrDefault();
                return;
            }

            data.Title = title.Value;
            data.Rows = new List<Models.DataFile.Attribute>();
            foreach(var row in ItemDetailEditRows)
            {
                data.Rows.Add(row.GetRowModel());
            }

            data.LastUpdateDateTime = DateTime.Now;

            MessagingCenter.Send(this, Messages.ADDITEM, this.data);
            await Navigation.PopAsync();
        }

        private async Task CancelCommandImplementation()
        {
            await Navigation.PopAsync();
        }

        private async Task AddHeaderRowCommandImplementation()
        {
            await Task.Delay(1);
            Models.DataFile.Attribute row = new Models.DataFile.Attribute(AttributeType.TYPE_HEADER);
            ItemDetailEditRows.Add(new ItemDetailEditRow(row));
        }

        private async Task AddAttributeRowCommandImplementation()
        {
            await Task.Delay(1);
            Models.DataFile.Attribute row = new Models.DataFile.Attribute(AttributeType.TYPE_STRING);
            ItemDetailEditRows.Add(new ItemDetailEditRow(row));
        }
        private async Task AddURLRowCommandImplementation()
        {
            await Task.Delay(1);
            Models.DataFile.Attribute row = new Models.DataFile.Attribute(AttributeType.TYPE_URL);
            row.Name = "URL";
            row.Value = "https://";
            ItemDetailEditRows.Add(new ItemDetailEditRow(row));
        }
        private async Task AddPasswordRowCommandImplementation()
        {
            await Task.Delay(1);
            Models.DataFile.Attribute row = new Models.DataFile.Attribute(AttributeType.TYPE_PASSWORD);
            row.Name = "Password";
            ItemDetailEditRows.Add(new ItemDetailEditRow(row));
        }
        #endregion
    }
}
