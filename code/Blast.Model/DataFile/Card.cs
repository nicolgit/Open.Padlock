using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Blast.Models.DataFile
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
                        sb.Append(row.Value);
                        break;
                    case AttributeType.TYPE_STRING:
                        sb.Append($"{row.Name} {row.Value}");
                        break;
                }
                sb.Append(" ");
            }

            return sb.ToString();
        }

        public bool AdvancedCompare(string s)
        {
            var me = RemoveDiacritics(this.ToString()).ToLower();
            s = RemoveDiacritics(s).ToLower();

            return me.Contains(s);
        }

        // https://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net
        string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
