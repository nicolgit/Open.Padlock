using System;
using System.Collections.Generic;
using System.Text;

namespace nicold.Padlock.Models.DataFile
{
    public class PadlockFile
    {
        public PadlockFile()
        {
            Id = Guid.NewGuid();
            Version = 10;
            Cards = new List<Card>();
        }

        public Guid Id { get; set; }
        public int Version { get; set; }

        List<Card> Cards { get; set; }
    }
}
