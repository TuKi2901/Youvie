using DTO;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Movie_Management_Project.Content.Guest;
using System.Xml.Linq;

namespace Movie_Management_Project.Content.User;

public partial class InformationUser : ContentPage
{
    DTO_Users user = Login.User;
    bool isClickChangePassword = false;
    public InformationUser()
    {
        InitializeComponent();
        LoadDataUser();
        lbPassword.Text = new string('\u25CF', 12);
    }

    private void LoadDataUser()
    {
        lbName.Text = user.UserName.ToString();
        lbPhoneNumber.Text = user.PhoneNumber.ToString();
        lbEmail.Text = user.Account.Email.ToString();
        lbGender.Text = user.Gender.ToString();
        lbCountry.Text = user.Country.ToString();
        lbBirthDay.Text = user.Birthday.ToString();
        lbPlan.Text = 1.ToString();

    }

    private async void btnUpgradePlan_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Payment());
    }

    private void btnChangePassword_Clicked(object sender, EventArgs e)
    {
        if (isClickChangePassword == true)
        {
            isClickChangePassword = false;
            btnChangePasswordVisible.Text = "Change Password";
            gridChangePassword.IsVisible = false;
        }
        else if (isClickChangePassword == false)
        {
            isClickChangePassword = true;
            btnChangePasswordVisible.Text = "Cancel";
            gridChangePassword.IsVisible = true;
        }
    }

    private async void btnLogout_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Login());
    }
}