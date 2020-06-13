using System.Windows.Input;

namespace nicold.Padlock.ViewModelsArtifacts
{
    public class ItemDetailViewRow: ItemDetailRow
    {
        public ItemDetailViewRow(Models.DataFile.Attribute row):base(row)
        {
            showValue = true;
        }

        #region PROPERTIES
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
        #endregion

        public ICommand PasswordCommand { get; set; }
        public ICommand UrlCommand { get; set; }
    }
}
