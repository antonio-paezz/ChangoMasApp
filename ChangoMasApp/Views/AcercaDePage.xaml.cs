using ChangoMasApp.ViewModels;

namespace ChangoMasApp.Views;

public partial class AcercaDePage : ContentPage
{
	public AcercaDePage()
	{
		InitializeComponent();
        
        AcercaDeViewModel viewModel = new AcercaDeViewModel();
        this.BindingContext = viewModel;
    }
}