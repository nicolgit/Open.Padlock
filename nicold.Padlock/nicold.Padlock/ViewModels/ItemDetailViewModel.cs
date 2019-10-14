using System;

using nicold.Padlock.Models;
using Xamarin.Forms;

namespace nicold.Padlock.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(INavigation navigation, Item item = null): base (navigation)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
