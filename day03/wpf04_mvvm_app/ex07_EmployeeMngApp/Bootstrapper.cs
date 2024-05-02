using Caliburn.Micro;
using ex07_EmployeeMngApp.ViewModels;
using System.Windows;

namespace ex07_EmployeeMngApp
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize(); // Caliburn.Micro MVVM 시작하도록 초기화
        }

        // MVVM 애플리케이션이 처음 시작될 때 이벤트 핸들러

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
           DisplayRootViewForAsync<MainViewModel>();
        }
    }
}
