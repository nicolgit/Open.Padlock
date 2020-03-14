using nicold.Padlock.Models.DataFile;
using nicold.Padlock.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace nicold.Padlock.ViewModelsArtifacts
{
    public class ItemDetailRow: ItemDetailEditRow
    {
        public ItemDetailRow(Models.DataFile.Attribute row) : base (row)
        {
            showValue = true;
        }

        public string ValueUI => ShowValue ? Value : new string('*', Value.Length);

        private bool showValue;
        public bool ShowValue {
            get { return showValue; }
            set {
                showValue = value;
                RaisePropertyChanged(() => ShowValue);
                RaisePropertyChanged(() => ValueUI);
            }
        }

        public ICommand PasswordCommand { get; set; }
        public ICommand UrlCommand { get; set; }
    }
}
