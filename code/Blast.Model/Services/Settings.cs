using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.Model.Services
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

        public enum StorageEnum
        {
            NONE = 0,
            STORAGE_LOCAL = 1,
            STORAGE_ONEDRIVE = 2
        }

        public Settings(IPreferences preferences)
        {
            UITheme = UIThemeEnum.UI_AUTO;
            StorageType = StorageEnum.NONE;
            FileName = "";

            this.preferences = preferences;
        }

        public UIThemeEnum UITheme { get; set; }
        public StorageEnum StorageType { get; set; }
        public string FileName { get; set; }

        public void SaveAll()
        {
            preferences.Set(UITheme.GetType().Name, (int)UITheme);
            preferences.Set(StorageType.GetType().Name, (int)StorageType);
            preferences.Set(FileName.GetType().Name, FileName);

        }

        public void LoadAll()
        {
            UITheme = (UIThemeEnum)preferences.Get(UITheme.GetType().Name, (int)UIThemeEnum.UI_AUTO);
            StorageType = (StorageEnum)preferences.Get(StorageType.GetType().Name, (int)StorageEnum.NONE);
            FileName = preferences.Get(FileName.GetType().Name, "");
        }

    }
}
