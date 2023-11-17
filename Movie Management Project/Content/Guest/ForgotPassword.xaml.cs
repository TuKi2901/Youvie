namespace Movie_Management_Project.Content.Guest;

public partial class ForgotPassword : ContentPage
{
	public ForgotPassword()
	{
		InitializeComponent();
	}

    private async void btnSignUp_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
    }
}