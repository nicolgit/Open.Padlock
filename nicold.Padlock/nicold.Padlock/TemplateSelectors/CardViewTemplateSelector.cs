using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace nicold.Padlock.TemplateSelectors
{
    public class CardViewTemplateSelector: DataTemplateSelector
    {
        DataTemplate good;
        DataTemplate bad;

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return good; // ((Person)item).DateOfBirth.Year >= 1980 ? ValidTemplate : InvalidTemplate;
        }
    }
}
