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

        public List<Blast.Models.DataFile.Card> Rows => current.File.Cards;
        //List<string> Rows { get; set; }

        private void loadCards()
        {
            current.File.Cards = new List<Models.DataFile.Card>();

            for (int i=0; i<10; i++)
            {
                Rows.Add(new Models.DataFile.Card() { Title="hello " + Random.Shared.Next().ToString()});
            }

            OnPropertyChanged(nameof(Rows));
        }
    }
}
