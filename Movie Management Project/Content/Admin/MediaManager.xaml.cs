
using Movie_Management_Project.Content.Guest;
using Movie_Management_Project.Content.User;
using System.Collections.ObjectModel;
using System.Data;
using System.Net.Http.Json;
using UraniumUI.Material;
namespace Movie_Management_Project.Content.Admin;

public partial class MediaManager : ContentPage
{

    public MediaManager()
    {
        InitializeComponent();
    }

    private void listViewCategoryLeftMedia_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }

    private void btnAdmin_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Login());
    }

    private void btnUser_Clicked(object sender, EventArgs e)
    {
        Navigation.PopToRootAsync(false);
        Navigation.PushAsync(new UserManager());
    }
}