using nicold.Padlock.Validators;
using nicold.Padlock.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace nicold.Padlock.ViewModelsArtifacts
{
    public class ItemDetailEditRow: ItemDetailRow
    {
        public ItemDetailEditRow(Models.DataFile.Attribute row): base(row)
        {
        }

        public Models.DataFile.Attribute GetRowModel()
        {
            Models.DataFile.Attribute row = new Models.DataFile.Attribute(Type);
            row.Name = Name;
            row.Value = Value;

            return row;
        }

        #region PROPERTIES
        
        #endregion

        public ICommand DeleteCommand { get; set; }
    }
}
