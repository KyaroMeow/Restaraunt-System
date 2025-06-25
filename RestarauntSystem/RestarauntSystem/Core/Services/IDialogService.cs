using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Services
{
    public interface IDialogService
    {
        void ShowMessage(string message, string title = null);
        void ShowWarning(string message, string title = null);
        void ShowError(string message, string title = null);
        bool ShowConfirmation(string message, string title = null);
        string ShowInputDialog(string message, string title = null);
        string ShowFileOpenDialog(string filter = null);
        string ShowFileSaveDialog(string defaultFileName = null, string filter = null);
    }
}
