using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace nicold.Padlock.Models.DataFile
{
    public class Attribute
    {
        public Attribute()
        {
            Type = AttributeType.TYPE_STRING; 
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public AttributeType Type { get; set; }
    }
}
