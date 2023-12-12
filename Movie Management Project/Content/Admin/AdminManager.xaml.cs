using Movie_Management_Project.Content.Guest;
using Movie_Management_Project.ViewModel;
using UraniumUI.Material.Controls;
using UraniumUI.Pages;

namespace Movie_Management_Project.Content.Admin;

public partial class AdminManager : ContentPage
{
    public static DataGrid data;
    private AdminManagerViewModel _viewModel;

    public AdminManager()
	{
        InitializeComponent();
        _viewModel = new AdminManagerViewModel();
        BindingContext = _viewModel;
        NavigatedTo += OnNavigating;
    }

    private void OnNavigating(object sender, NavigatedToEventArgs e)
    {

        // Kích hoạt animation khi trang xuất hiện
        if (Login.Admin == null)
            _viewModel.NameAdmin = "Unknown";
        else
        {
            _viewModel.NameAdmin = Login.Admin.AdminName;
        }
    }

    private async void btnMedia_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MediaManager());
    }

    private async void btnUser_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserManager());
    }

    private void btnRefreshAdmin_Clicked(object sender, EventArgs e)
    {

    }

    private async void btnLogout_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Login());
    }
}