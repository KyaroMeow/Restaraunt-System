using Microsoft.Win32;
using RestarauntSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RestarauntSystem.Infrastructure.Services
{
    public class DialogService : IDialogService
    {
        public void ShowMessage(string message, string title = null)
        {
            MessageBox.Show(message, title ?? "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowWarning(string message, string title = null)
        {
            MessageBox.Show(message, title ?? "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ShowError(string message, string title = null)
        {
            MessageBox.Show(message, title ?? "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool ShowConfirmation(string message, string title = null)
        {
            return MessageBox.Show(message, title ?? "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        public string ShowInputDialog(string message, string title = null)
        {
            // Можно использовать кастомное диалоговое окно
            return Microsoft.VisualBasic.Interaction.InputBox(message, title ?? "Ввод данных");
        }

        public string ShowFileOpenDialog(string filter = null)
        {
            var dialog = new OpenFileDialog
            {
                Filter = filter ?? "Все файлы (*.*)|*.*"
            };
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        public string ShowFileSaveDialog(string defaultFileName = null, string filter = null)
        {
            var dialog = new SaveFileDialog
            {
                FileName = defaultFileName,
                Filter = filter ?? "Все файлы (*.*)|*.*"
            };
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}
