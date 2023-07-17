using Blast.Model.DataFile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blast.ViewModel.Row
{
    public class MainViewModelItem
    {
        public MainViewModelItem(Card c)
        {
            model = c;
        }
        public string Id => model.Id.ToString();
        public string Title => model.Title;
        public string Notes => model.Notes;
        public string Text01 => string.Format($"{model.Rows.Count()} rows...");
        public Card model { get; set; }
    }

}
