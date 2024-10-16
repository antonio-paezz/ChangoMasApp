using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangoMasApp.ViewModels
{
    public partial class AcercaDeViewModel: BaseViewModel
    {
        public AcercaDeViewModel()
        {
            
        }

        [RelayCommand]
        private async Task GoToBack()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage(), true);
        }
    }
}
