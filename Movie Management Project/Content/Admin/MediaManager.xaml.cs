using Movie_Management_Project.Content.Guest;
using Movie_Management_Project.ViewModel;
using UraniumUI.Material.Controls;
using UraniumUI.Pages;
namespace Movie_Management_Project.Content.Admin;

public partial class MediaManager : ContentPage
{
    private MediaManagerViewModel _viewModel;
    public MediaManager()
    {
        InitializeComponent();
        _viewModel = new MediaManagerViewModel();
        BindingContext = _viewModel;
        NavigatedTo += OnNavigating;
    }

    private void OnNavigating(object sender, NavigatedToEventArgs e)
    {

        // Kích hoạt animation khi trang xuất hiện
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
    private void listViewCategoryLeftMedia_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }

    private void btnAdmin_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AdminManager());
    }

    private void btnUser_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new UserManager());
    }
}