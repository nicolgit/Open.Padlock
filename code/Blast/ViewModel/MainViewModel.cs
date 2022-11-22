using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private Model.Services.Current current;
        private Model.Services.Settings settings;

        public MainViewModel(Model.Services.Current c, Model.Services.Settings s)
        {
            current = c;
            settings = s;
        }


        List<Blast.Models.DataFile.Card> Rows => current.File.Cards;
    }
}
