using Blast.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.ViewModel
{
    public partial class SettingsViewModel: ViewModelBase
    {
        private Model.Settings settings;

        public SettingsViewModel(Model.Settings s)
        {
            settings = s;
            s.LoadAll();

            var t = new List<BlastItem>();
            t.Clear();
            t.Add(new BlastItem() { Id = (int)Settings.UIThemeEnum.UI_AUTO, Title = "auto" });
            t.Add(new BlastItem() { Id = (int)Settings.UIThemeEnum.UI_DARK, Title = "dark" });
            t.Add(new BlastItem() { Id = (int)Settings.UIThemeEnum.UI_LIGHT, Title = "light" });
            themes = t;
            selectedTheme = themes.Where(a => a.Id == (int)settings.UITheme).FirstOrDefault();
        }

        [ObservableProperty]
        private BlastItem selectedTheme;

        [ObservableProperty]
        private List<BlastItem> themes;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(pluto))]
        private string pippoPippo;

        public string pluto => $"aaaa {pippoPippo} bbb";

        [ICommand]
        void SaveAll()
        {
            settings.UITheme = (Settings.UIThemeEnum)selectedTheme.Id;
            settings.SaveAll();
        }

        [ICommand]
        void ThemeChanged()
        {
            switch ((int)selectedTheme.Id)
            {
                case (int)Settings.UIThemeEnum.UI_AUTO:
                    Application.Current.UserAppTheme = AppTheme.Unspecified;
                    break;
                case (int)Settings.UIThemeEnum.UI_LIGHT:
                    Application.Current.UserAppTheme = AppTheme.Light;
                    break;
                case (int)Settings.UIThemeEnum.UI_DARK:
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    break;
            }
        }
    }
}
