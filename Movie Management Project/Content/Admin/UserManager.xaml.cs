using Movie_Management_Project.ViewModel;
using System.Globalization;
using System.Net.Mail;

namespace Movie_Management_Project.Content.Admin;

public partial class UserManager : ContentPage
{
    private readonly UsersManagerViewModel _usersManagerViewModel = new UsersManagerViewModel();

    public UserManager()
	{
		InitializeComponent();
        BindingContext = _usersManagerViewModel;
    }
}