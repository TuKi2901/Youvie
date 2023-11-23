using BUS;
using DTO;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Movie_Management_Project.Content.User;
using System.Net.Mail;
using System.Security.Principal;

namespace Movie_Management_Project.Content.Guest;

public partial class Login : ContentPage
{
    BUS_Project1 bus_project1 = new BUS_Project1();

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

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (txtEmail.Text == string.Empty)
            {
                throw new Exception("Email mustn't be left blank, bro !!!");
            }

            if (txtPassword.Text == string.Empty)
            {
                throw new Exception("Bro, you fogot input password !!!");
            }

            bool check = await bus_project1.BusGetLoginUser(txtEmail.Text, txtPassword.Text);

            if (!check)
            {
                throw new Exception("Email or password isn't right !!!");
            }

            await DisplayAlert("Notification", "Login sucessfully !!!", "OK");
            await Navigation.PushAsync(new Home());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}