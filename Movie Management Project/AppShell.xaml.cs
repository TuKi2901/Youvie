using Movie_Management_Project.Content.Admin;
using Movie_Management_Project.Content.User;
using Movie_Management_Project.Content.Guest;
using Movie_Management_Project.ViewModel;
namespace Movie_Management_Project
{
    public partial class AppShell : Shell
    {
        private string _idUser;

        public AppShell()
        {
            InitializeComponent();
        }
        
        //Chuyển Page, fix lỗi ấn nhanh button không bắt được sự kiện
        private async Task PushPage<T>(T page) where T : Page
        {
            if (busyIndicator != null)
            {
                busyIndicator.IsRunning = true;
                busyIndicator.IsVisible = true;

                try
                {
                    await Navigation.PushAsync(page);
                }
                finally
                {
                    busyIndicator.IsRunning = false;
                    busyIndicator.IsVisible = false;
                }
            }
        }

        private void btnUser_Loaded(object sender, EventArgs e)
        {

        }

        private async void btnHome_Clicked(object sender, EventArgs e)
        {
            _idUser = Login.User;
            HomeMainViewModel homeMainViewModel = new HomeMainViewModel(_idUser);
            await PushPage(new Home(homeMainViewModel));
        }

        private async void btnTVShows_Clicked(object sender, EventArgs e)
        {
            await PushPage(new AdminManager());
        }

        private async void btnMovies_Clicked(object sender, EventArgs e)
        {
            await PushPage(new MediaManager());
        }

        private async void btnLastest_Clicked(object sender, EventArgs e)
        {
            await PushPage(new UserManager());
        }

        #region Shortcut Form Test
        //private async void Button_Clicked_2(object sender, EventArgs e)
        //{
        //    await PushPage(new Home());
        //}
        //private async void Button_Clicked_3(object sender, EventArgs e)
        //{
        //    await PushPage(new AdminManager());
        //}
        //private async void Button_Clicked_4(object sender, EventArgs e)
        //{
        //    await PushPage(new MediaManager());
        //}
        //private async void Button_Clicked_5(object sender, EventArgs e)
        //{
        //    await PushPage(new UserManager());
        //}
        //private async void Button_Clicked_6(object sender, EventArgs e)
        //{
        //    await PushPage(new Search());
        //}
        //private async void Button_Clicked_7(object sender, EventArgs e)
        //{
        //    await PushPage(new Payment());
        //}
        //private async void Button_Clicked_8(object sender, EventArgs e)
        //{
        //    await PushPage(new SignUp());
        //}
        #endregion

        private async void btnUser_Clicked(object sender, EventArgs e)
        {
            await PushPage(new InformationUser());
        }
    }
}