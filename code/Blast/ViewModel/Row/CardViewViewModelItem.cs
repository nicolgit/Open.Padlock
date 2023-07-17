using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.ViewModel.Row
{
    public class CardViewViewModelItem
    {
        public CardViewViewModelItem(Blast.Model.DataFile.Attribute item)
        {
            Model = item;
        }

        public string UpperText => string.IsNullOrWhiteSpace(Model.Value) ? "" : Model.Name;

        public string MainText
        {
            get 
            {
                if (string.IsNullOrWhiteSpace(Model.Value))
                {
                    return Model.Name;
                }
                else
                {
                    return Model.Type == Blast.Model.DataFile.AttributeType.TYPE_PASSWORD ? "******" : Model.Value;
                }
            }
        }

        public Blast.Model.DataFile.Attribute Model { get; set;}

    }
}
