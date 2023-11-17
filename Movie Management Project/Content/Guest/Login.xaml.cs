using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Security.Principal;

namespace Movie_Management_Project.Content.Guest;

public partial class Login : ContentPage
{
    public Login()
    {
        Content = new StackLayout
        {
            Children = { btnSignUp }
        };



        InitializeComponent();

    }
    private async void btnSignUp_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void btnForgotPassword_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ForgotPassword());
    }
}