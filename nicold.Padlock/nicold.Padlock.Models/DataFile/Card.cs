using System;
using System.Collections.Generic;
using System.Text;

namespace nicold.Padlock.Models.DataFile
{
    public class Card
    {
        public Card()
        {
            Id = Guid.NewGuid();
            Tags = new List<string>();
            Rows = new List<Attribute>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public bool IsFavotire { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public DateTime LastOpenedDateTime { get; set; }
        public int UsedCounter { get; set; }
        public List<string> Tags { get; set; }
        public List<Attribute> Rows { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Title);
            sb.Append(" ");
            sb.Append(Notes);
            
            foreach (var tag in Tags)
            {
                sb.Append(tag);
                sb.Append(" ");
            }

            foreach (var row in Rows)
            {
                switch (row.Type)
                {
                    case AttributeType.TYPE_PASSWORD:
                        // do not search by password value
                        break;
                    case AttributeType.TYPE_HEADER:
                        sb.Append(row.Name);
                        break;
                    default:
                    case AttributeType.TYPE_URL:
                    case AttributeType.TYPE_STRING:
                        sb.Append(row.Value);
                        break;
                }
                sb.Append(" ");
            }

            return sb.ToString().ToLower();
        }
    }
}
