namespace Movie_Management_Project.Content.User;

public partial class Payment : ContentPage
{
	public Payment()
	{
		InitializeComponent();
	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new Home());
    }
}