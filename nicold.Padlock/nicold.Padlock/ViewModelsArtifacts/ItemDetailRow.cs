using nicold.Padlock.Models.DataFile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace nicold.Padlock.ViewModelsArtifacts
{
    public class ItemDetailRow
    {
        public string Name { get; set; }
        public string Value{ get; set; }
        public AttributeType Type { get; set; }

        public ICommand PasswordCommand { get; set; }
    }
}
