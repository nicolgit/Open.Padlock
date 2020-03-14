using System;
using nicold.Padlock.ViewModels;

namespace nicold.Padlock.ViewModelsArtifacts
{
    public class ItemDetailRow : ExtendedBindableObject
    {
        public ItemDetailRow(Models.DataFile.Attribute row)
        {
            name = row.Name;

            value_ = row.Value;
            type = row.Type;
        }

        #region PROPERTIES
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(() => Name); }
        }

        private string value_;
        public string Value
        {
            get { return value_; }
            set { value_ = value; RaisePropertyChanged(() => Value); }
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
