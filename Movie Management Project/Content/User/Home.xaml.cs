using Movie_Management_Project.ViewModel;

namespace Movie_Management_Project.Content.User;

public partial class Home : ContentPage
{
    public Home()
    {
        InitializeComponent();
    }

    public Home(HomeMainViewModel homeMainViewModel)
	{
		InitializeComponent();
		BindingContext = homeMainViewModel;
	}

  //  private async void ImageButton_Clicked(object sender, EventArgs e)
  //  {
		//await Navigation.PushAsync(new Play());
  //  }
}