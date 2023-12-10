using Movie_Management_Project.ViewModel;
using UraniumUI.Material.Controls;
using UraniumUI.Pages;

namespace Movie_Management_Project.Content.Admin;

public partial class AdminManager : ContentPage
{
    public static DataGrid data;

    public AdminManager()
	{
        InitializeComponent();
	}

    private async void btnMedia_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MediaManager());
    }

    private async void btnUser_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserManager());
    }
}