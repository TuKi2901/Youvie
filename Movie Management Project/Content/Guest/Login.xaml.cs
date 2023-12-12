using BUS;
using DTO;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Movie_Management_Project.Content.Admin;
using Movie_Management_Project.Content.User;
using Movie_Management_Project.ViewModel;
using System.Net.Mail;
using System.Security.Principal;

namespace Movie_Management_Project.Content.Guest;

public partial class Login : ContentPage
{
    public static DTO_Users User;
    public static DTO_Admins Admin;
    public static int IsLogin;

    BUS_Project1 bus_project1 = new BUS_Project1();
    public Login()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Xóa dữ liệu khi trang Login sắp hiển thị
        txtEmail.Text = null;
        txtPassword.Text = null;
    }

    private async void btnSignUp_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUp());
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
                throw new Exception("Bro, you forgot input password !!!");
            }
            dynamic check = await bus_project1.CheckUserOrAdmin(txtEmail.Text, txtPassword.Text);

            await DisplayAlert("Notification", "Login sucessfully !!!", "OK");

            if (check is DTO_Users)
            {
                DTO_Users user = check;
                IsLogin = 1;

                User = user;
                await Navigation.PushAsync(new Home());
            }

            if (check is DTO_Admins)
            {
                DTO_Admins admin = check;
                Admin = check;
                IsLogin = 1;
                await Navigation.PushAsync(new AdminManager());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}