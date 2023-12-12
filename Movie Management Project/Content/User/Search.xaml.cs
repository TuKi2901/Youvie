using Movie_Management_Project.ViewModel;

namespace Movie_Management_Project.Content.User;

public partial class Search : ContentPage
{
	public Search(SearchViewModel searchViewModel)
	{
		InitializeComponent();
		BindingContext = searchViewModel;
	}
}