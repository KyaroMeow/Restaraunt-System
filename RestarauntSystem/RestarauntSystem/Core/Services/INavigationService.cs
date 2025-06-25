using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Services
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel>() where TViewModel : ObservableObject;
    }
}
