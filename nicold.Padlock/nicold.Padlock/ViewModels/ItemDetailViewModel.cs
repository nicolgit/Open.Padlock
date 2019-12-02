using System;
using System.Linq;
using nicold.Padlock.ViewModelsArtifacts;
using nicold.Padlock.Models;
using Xamarin.Forms;
using nicold.Padlock.Models.DataFile;
using System.Collections.Generic;

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
                ItemDetailRows.Add(new ItemDetailRow()
                {
                    Name = row.Name,
                    Value = row.Value,
                    Type = row.Type
                });
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
    }
}
