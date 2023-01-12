using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;

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

            loadCards();
        }

        public List<Blast.Model.DataFile.Card> Rows => current.Document.Cards;
        
        private void loadCards()
        {
            //TODO implemets CardViewModel

            OnPropertyChanged(nameof(Rows));
        }
    }
}
