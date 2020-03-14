using nicold.Padlock.Validators;
using nicold.Padlock.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace nicold.Padlock.ViewModelsArtifacts
{
    public class ItemDetailEditRow: ExtendedBindableObject
    {
        public ItemDetailEditRow(Models.DataFile.Attribute row)
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
            set { name=value; RaisePropertyChanged(() => Name); }
        }

        private string value_;
        public string Value
        {
            get { return value_; }
            set { value_=value; RaisePropertyChanged(() => Value); }
        }

        private Models.DataFile.AttributeType type;
        public Models.DataFile.AttributeType Type
        {
            get { return type; }
            set { type = value; RaisePropertyChanged(() => Type); }
        }
        #endregion
    }
}
