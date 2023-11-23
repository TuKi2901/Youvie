using Movie_Management_Project.Content.Admin;
using Movie_Management_Project.Content.User;
using Movie_Management_Project.Content.Guest;

namespace Movie_Management_Project
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
        }
        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Play());
        }
        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminManager());
        }
        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MediaManager());
        }
        private async void Button_Clicked_5(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserManager());
        }
    }
}