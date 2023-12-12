using Movie_Management_Project.Content.Guest;
using Movie_Management_Project.ViewModel;
using UraniumUI.Material.Controls;
using UraniumUI.Pages;

namespace Movie_Management_Project.Content.Admin;

public partial class UserManager : ContentPage
{
    private UsersManagerViewModel _viewModel;
    public UserManager()
	{
		InitializeComponent();
        _viewModel = new UsersManagerViewModel();
        BindingContext = _viewModel;
        NavigatedTo += OnNavigating;
    }
    private void OnNavigating(object sender, NavigatedToEventArgs e)
    {

        if (Login.Admin == null)
            _viewModel.AdminName = "Unknown";
        else
        {
            _viewModel.AdminName = Login.Admin.AdminName;
        }
    }
    private async void btnLogout_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Login());
    }
    private void btnAdmin_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AdminManager());
    }

    private void btnMedia_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MediaManager());
    }

}