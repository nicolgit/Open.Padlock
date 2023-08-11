

namespace Blast.UIServices
{
    public interface IDialogService
    {
        public Task DisplayAlertAsync(string title, string message, string cancel);
        public Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel);
        public Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = default, int maxLength = -1, Microsoft.Maui.Keyboard keyboard = default, string initialValue = "");

        //public Task<string> ShowActionsAsync(string title, string message, string destruction, params string[] buttons);
        //public Task ToastAsync(string message, int duration);
        //public Task<bool> SnackBarAsync(string message, string actionText, Func<task> action);
    }
    internal class DialogService : IDialogService
    {
        public Task DisplayAlertAsync(string title, string message, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = default, int maxLength = -1, Microsoft.Maui.Keyboard keyboard = default, string initialValue = "")
        {
            return Application.Current.MainPage.DisplayPromptAsync(title, message, accept, cancel, placeholder,maxLength, keyboard, initialValue);
        }

        //public Task<string> ShowActionsAsync(string title, string message, string destruction, params string[] buttons)
        //{
        //    return Application.Current.MainPage.DisplayActionSheet(title, Resources.CloseText, destruction, buttons);
        //}

        //public Task ToastAsync(string message, int duration)
        //{
        //    return Application.Current.MainPage.DisplayToastAsync(message, duration);
        //}

        //public Task<bool> SnackBarAsync(string message, string actionText, Func<task> action)
        //{
        //    return Application.Current.MainPage.DisplaySnackBarAsync(message, actionText, action);
        //}
    }
}
