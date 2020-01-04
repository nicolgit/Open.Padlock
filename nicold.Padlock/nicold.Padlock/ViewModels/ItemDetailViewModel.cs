using System;
using System.Linq;
using nicold.Padlock.ViewModelsArtifacts;
using nicold.Padlock.Models;
using Xamarin.Forms;
using nicold.Padlock.Models.DataFile;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace nicold.Padlock.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ItemDetailViewModel(INavigation navigation, Item item = null): base (navigation)
        {
            var card = item.Card;

            Title = card.Title;
            Notes = card.Notes;

            ItemDetailRows = new List<ItemDetailRow>();
            foreach (var row in card.Rows)
            {
                var itemRow = new ItemDetailRow()
                {
                    Name = row.Name,
                    Value = row.Value,
                    Type = row.Type,
                };

                switch(itemRow.Type)
                {
                    case AttributeType.TYPE_PASSWORD:
                        itemRow.ShowValue = false;
                        itemRow.PasswordCommand = new Command<ItemDetailRow>(async (ItemDetailRow i) => await PasswordCommandImplementation(i));
                        break;
                    case AttributeType.TYPE_URL:
                        itemRow.UrlCommand = new Command<ItemDetailRow>(async (ItemDetailRow i) => await UrlCommandImplementation(i));
                        break;
                }

                ItemDetailRows.Add(itemRow);
            }
        }

        #region PROPERTIES
        string notes;
        public string Notes
        {
            get { return notes; }
            set { SetProperty(ref notes, value); }
        }

        List<ItemDetailRow> itemDetailRows;
        public List<ItemDetailRow> ItemDetailRows
        {
            get { return itemDetailRows; }
            set { SetProperty(ref itemDetailRows, value); }
        }
        #endregion

        #region COMMAND IMPLEMENTATION
        private async Task PasswordCommandImplementation(ItemDetailRow item)
        {
            const string SHOW = "Show";
            const string HIDE = "Hide";
            const string COPY = "Copy to clipboard";
            
            string rowText = item.ShowValue ? HIDE : SHOW;            
            var action = await Application.Current.MainPage.DisplayActionSheet("How whould you like to use the password?", "Cancel", null, rowText, COPY);

            switch(action)
                {
                case SHOW:
                case HIDE:
                    item.ShowValue = !item.ShowValue;
                    break;
                case COPY:
                    await Clipboard.SetTextAsync(item.Value);
                    break;
            }
        }

        private async Task UrlCommandImplementation(ItemDetailRow item)
        {
            await Browser.OpenAsync(item.Value, BrowserLaunchMode.SystemPreferred);
        }
        #endregion
    }
}
