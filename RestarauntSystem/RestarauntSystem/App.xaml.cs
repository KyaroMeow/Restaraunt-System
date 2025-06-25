using RestarauntSystem.Infrastructure.Data;
using RestarauntSystem.WPF;
using RestarauntSystem.WPF.ViewModel;
using RestarauntSystem.WPF.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace RestarauntSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainView = new MainView();
            mainView.DataContext = Locator.GetService<MainViewModel>();
            mainView.Show();
        }

    }

}
