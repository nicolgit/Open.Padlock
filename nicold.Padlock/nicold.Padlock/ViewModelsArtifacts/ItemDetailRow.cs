using nicold.Padlock.Models.DataFile;
using nicold.Padlock.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace nicold.Padlock.ViewModelsArtifacts
{
    public class ItemDetailRow: BaseViewModel
    {
        public ItemDetailRow(): base (null)
        {
            ShowValue = true;
        }

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

        public string ValueUI => ShowValue ? Value : new string('*', Value.Length);

        private AttributeType type;
        public AttributeType Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }


        private bool showValue;
        public bool ShowValue {
            get { return showValue; }
            set { 
                SetProperty(ref showValue, value);
                OnPropertyChanged("ValueUI");
            }
        }

        public ICommand PasswordCommand { get; set; }
    }
}
