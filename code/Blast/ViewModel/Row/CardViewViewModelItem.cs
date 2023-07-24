﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.ViewModel.Row
{
    public partial class CardViewViewModelItem: ObservableObject
    {
        public CardViewViewModelItem(Blast.Model.DataFile.Attribute item)
        {
            PasswordIsVisible = false;
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
                    return Model.Type == Blast.Model.DataFile.AttributeType.TYPE_PASSWORD && !PasswordIsVisible ? "******" : Model.Value;
                }
            }
        }

        public bool IsPassword => Model.Type == Blast.Model.DataFile.AttributeType.TYPE_PASSWORD;

        public bool PasswordIsVisible { get; set; }

        public Blast.Model.DataFile.Attribute Model { get; set;}
    }
}
