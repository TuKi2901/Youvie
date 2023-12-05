using Movie_Management_Project.ViewModel;
using System.Globalization;
using System.Net.Mail;

namespace Movie_Management_Project.Content.Admin;

public partial class UserManager : ContentPage
{
    public UserManager()
	{
		InitializeComponent();
    }

    private void btnAdmin_Clicked(object sender, EventArgs e)
    {
        Navigation.PopToRootAsync();
    }

    private void btnMedia_Clicked(object sender, EventArgs e)
    {
        Navigation.PopToRootAsync(false);
        Navigation.PushAsync(new MediaManager());
    }
}