using System;
using System.Linq;
using nicold.Padlock.Models;
using Xamarin.Forms;

namespace nicold.Padlock.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public string Description { get; set; }
        public ItemDetailViewModel(INavigation navigation, Item item = null): base (navigation)
        {
            Title = item?.Text;
            var card = Globals.File.Cards.Where(a => a.Id.ToString() == item.Id).FirstOrDefault();
            Description = card.Notes + "\r\n";

            foreach (var row in card.Rows)
            {
                Description += row.Name + ": " + row.Value + "\r\n";
            }
        }
    }
}
