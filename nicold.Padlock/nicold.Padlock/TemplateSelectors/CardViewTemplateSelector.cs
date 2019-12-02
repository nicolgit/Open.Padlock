using Microsoft.Graph;
using nicold.Padlock.ViewModelsArtifacts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace nicold.Padlock.TemplateSelectors
{
    public class CardViewTemplateSelector: DataTemplateSelector
    {
        public DataTemplate StringTemplate { get; set; }
        public DataTemplate HeaderTemplate { get; set; }
        public DataTemplate PasswordTemplate { get; set; }
        public DataTemplate URLTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch ( ((ItemDetailRow) item).Type)
            {
                case Models.DataFile.AttributeType.TYPE_HEADER:
                    return HeaderTemplate;
                case Models.DataFile.AttributeType.TYPE_URL:
                    return URLTemplate;
                case Models.DataFile.AttributeType.TYPE_PASSWORD:
                    return PasswordTemplate;
                default:
                case Models.DataFile.AttributeType.TYPE_STRING:
                    return StringTemplate;
            }
        }
    }
}
