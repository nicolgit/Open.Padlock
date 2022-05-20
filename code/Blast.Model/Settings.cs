using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.Model
{
    public class Settings
    {
        private IPreferences preferences;

        public enum UIThemeEnum
        {
            UI_AUTO = 1,
            UI_LIGHT = 2,
            UI_DARK = 3

        }

        public Settings(IPreferences preferences)
        {
            UITheme = UIThemeEnum.UI_AUTO;
            this.preferences = preferences;
        }

        public UIThemeEnum UITheme { get; set; }

        public void SaveAll()
        {
            preferences.Set<int>(UITheme.GetType().Name, (int)UITheme);
        }

        public void LoadAll()
        {
           UITheme = (UIThemeEnum)preferences.Get<int>(UITheme.GetType().Name, (int)UIThemeEnum.UI_AUTO);
        }


    }
}
