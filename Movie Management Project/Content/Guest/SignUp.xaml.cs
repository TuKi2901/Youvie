using BUS;
using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.GTKSpecific;
using Microsoft.Maui.Platform;
using System.Net.Mail;
using static System.Net.WebRequestMethods;

namespace Movie_Management_Project.Content.Guest
{
    public partial class SignUp : ContentPage
    {
        BUS_Project1 bus_project1 = new BUS_Project1();

        public SignUp()
        {
            
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private async void btnSignUp_Clicked(object sender, EventArgs e)
        {
            try
            {
                string gender;
                if (rbMale.IsChecked) gender = rbMale.Value.ToString();
                else if (rbFemale.IsChecked) gender = rbFemale.Value.ToString();
                else { throw new Exception("Choose your gender, bro !!!"); }

                if (txtUserName.Text == null || txtUserName.Text.Length < 2)
                {
                    txtUserName.Focus();
                    throw new Exception("UserName is invalid !!!");
                }

                if (txtEmail.Text == null)
                {
                    throw new Exception("Email is invalid !!!");
                } 
                else
                {
                    if (!IsValidEmail(txtEmail.Text))
                    {
                        txtEmail.Focus();
                        throw new Exception("Email is invalid !!!");
                    }
                }

                if (txtPassword.Text == null || txtPassword.Text.Length < 8)
                {
                    txtPassword.Focus();
                    throw new Exception("Password must larger 8 character !!!");
                }
                else
                {
                    if (txtConfirmPassword.Text == null || !txtConfirmPassword.Text.Equals(txtPassword.Text))
                    {
                        throw new Exception("Password doesn't match !!!");
                    }
                }

                if (txtPhoneNumber.Text == null || txtPhoneNumber.Text.Length < 1)
                {
                    throw new Exception("PhoneNumber is invalid !!!");
                }

                if (pkCountry.SelectedItem == null)
                {
                    throw new Exception("Bro, You need must choose your country !!!");
                }

                DTO_Users dTO_Users = new DTO_Users();
                dTO_Users.UserName = txtUserName.Text;
                dTO_Users.Gender = gender;
                dTO_Users.PhoneNumber = txtPhoneNumber.Text;
                dTO_Users.Birthday = dpkBirthday.Date;
                dTO_Users.Country = pkCountry.SelectedItem.ToString();

                await DisplayAlert("Error", dTO_Users.Birthday.ToString() + " " + dTO_Users.Country, "OK");

                DTO_Accounts dTO_Accounts = new DTO_Accounts();
                dTO_Accounts.Email = txtEmail.Text;
                dTO_Accounts.Password = txtPassword.Text;

                bool check = await bus_project1.BusCreateUser(dTO_Users, dTO_Accounts);

                if (!check)
                {
                    throw new Exception("Create an account fail");
                }

                await DisplayAlert("Notification", "Created an account successful !!!", "OK");
                await Navigation.PushAsync(new Login());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}