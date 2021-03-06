﻿using System;
using System.Linq;
using nicold.Padlock.ViewModelsArtifacts;
using Xamarin.Forms;
using nicold.Padlock.Models.DataFile;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Collections.ObjectModel;

namespace nicold.Padlock.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ItemDetailViewModel(INavigation navigation, Item item = null): base (navigation)
        {
            var card = item.Card;

            Title = card.Title;
            Notes = card.Notes;

            ItemDetailRows = new ObservableCollection<ItemDetailViewRow>();

            foreach (var row in card.Rows)
            {
                var itemRow = new ItemDetailViewRow(row);

                switch(itemRow.Type)
                {
                    case AttributeType.TYPE_PASSWORD:
                        itemRow.ShowValue = false;
                        itemRow.PasswordCommand = new Command<ItemDetailViewRow>(async (ItemDetailViewRow i) => await PasswordCommandImplementation(i));
                        break;
                    case AttributeType.TYPE_URL:
                        itemRow.UrlCommand = new Command<ItemDetailViewRow>(async (ItemDetailViewRow i) => await UrlCommandImplementation(i));
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
            set { notes=value; RaisePropertyChanged(() => Notes); }
        }

        ObservableCollection<ItemDetailViewRow> itemDetailRows;
        public ObservableCollection<ItemDetailViewRow> ItemDetailRows
        {
            get { return itemDetailRows; }
            set { itemDetailRows=value; RaisePropertyChanged(() => ItemDetailRows); }
        }
        #endregion

        #region COMMAND IMPLEMENTATION
        private async Task PasswordCommandImplementation(ItemDetailViewRow item)
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

        private async Task UrlCommandImplementation(ItemDetailViewRow item)
        {
            var uristring = item.Value;

            if ( !uristring.StartsWith("http://",StringComparison.OrdinalIgnoreCase) &&
                 !uristring.StartsWith("https://",StringComparison.OrdinalIgnoreCase))
            {
                uristring = "http://" + uristring;
            }

            if (Uri.IsWellFormedUriString(uristring, UriKind.Absolute) == false)
            {
                await Application.Current.MainPage.DisplayAlert("", "URL not well formed, please check","mycancel");
            }
            else
            {
                await Browser.OpenAsync(uristring, BrowserLaunchMode.SystemPreferred);
            }
        }
        #endregion
    }
}
