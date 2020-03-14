using nicold.Padlock.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace nicold.Padlock.ViewModelsArtifacts
{
    public class ItemDetailEditRow: BaseViewModel
    {
        public ItemDetailEditRow(Models.DataFile.Attribute row) : base(null)
        {
            name = row.Name;
            value_ = row.Value;
            type = row.Type;
        }

        public Models.DataFile.Attribute GetRowModel()
        {
            Models.DataFile.Attribute row = new Models.DataFile.Attribute(Type);
            row.Name = Name;
            row.Value = Value;

            return row;
        }

        #region PROPERTIES
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string value_;
        public string Value
        {
            get { return value_; }
            set { SetProperty(ref value_, value); }
        }

        private Models.DataFile.AttributeType type;
        public Models.DataFile.AttributeType Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }
        #endregion
    }
}
