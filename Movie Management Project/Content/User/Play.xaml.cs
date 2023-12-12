
using Movie_Management_Project.ViewModel;

namespace Movie_Management_Project.Content.User;

public partial class Play : ContentPage
{
	public Play(PlayMediaViewModel playMediaViewModel)
	{
        InitializeComponent();
        BindingContext = playMediaViewModel;
    }
    public Play()
	{
		InitializeComponent();
    }
}