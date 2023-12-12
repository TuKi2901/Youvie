using BUS;
using DTO;
using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Windows.Input;
using Movie_Management_Project.Content.Admin;
using System.Diagnostics.Metrics;
using Movie_Management_Project.Content.Guest;

namespace Movie_Management_Project.ViewModel
{
    public partial class AdminManagerViewModel : BaseViewModel
    {
        private BUS_Project1 _bus = new BUS_Project1();
        private string _email;
        private string _password;
        private string _adminname;
        private string _gender;
        private string _search;
        public ObservableCollection<DTO_Admins> SelectedAdmins { get; set; } = new ObservableCollection<DTO_Admins>();
        public static ObservableCollection<DTO_Admins> dsAdmins = new();

        public ICommand AddAdminCommand { get; }
        public ICommand DeleteAdminCommand { get; }
        public ICommand SaveUpdateAdminCommand { get; }
        public ICommand UpdateAdminCommand { get; }
        public ICommand FindAdminCommand { get; }
        public ICommand RefreshCommand { get; }
        public AdminManagerViewModel()
        {
            AdminsDataGrid();
            AddAdminCommand = new Command(AddAdmin);
            DeleteAdminCommand = new Command(DeleteAdmin);
            UpdateAdminCommand = new Command(UpdateAdmin);
            SaveUpdateAdminCommand = new Command(SaveUpdateAdmin);
            FindAdminCommand = new Command(FindAdmin);
            RefreshCommand = new Command(RefreshFormAdmin);
        }

        #region SetProperties
        public ObservableCollection<DTO_Admins> DsAdmins
        {
            get { return dsAdmins; }
            set { SetProperty(ref dsAdmins, value, "DsAdmins"); }
        }
        public string AdminName
        {
            get { return _adminname; }
            set
            { SetProperty(ref _adminname, value); }
        }
        public string Email
        {
            get { return _email; }
            set
            { SetProperty(ref _email, value); }
        }
        public string Password
        {
            get { return _password; }
            set
            { SetProperty(ref _password, value); }
        }

        private bool isMale;

        public bool IsMale
        {
            get => isMale;
            set
            {
                if (SetProperty(ref isMale, value))
                {
                    _gender = "Male";
                }
            }
        }

        // Similarly, you can have a property for Female
        private bool isFemale;

        public bool IsFemale
        {
            get => isFemale;
            set
            {
                if (SetProperty(ref isFemale, value))
                {
                    _gender = "Female";
                }
            }
        }

        public string Search
        {
            get { return _search; }
            set
            { SetProperty(ref _search, value); }
        }
        #endregion

        private async void AdminsDataGrid()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                List<DTO_Admins> admins = await _bus.BusGetAllAdmins();

                foreach (DTO_Admins admin in admins)
                {
                    DsAdmins.Add(admin);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Unable to get admins: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void RefreshFormAdmin()
        {
            if (IsBusy)
            {
                return;
            }

            //await Shell.Current.Navigation.PushAsync(new AdminManager());
            try
            {

                IsBusy = false;
                Email = string.Empty;
                AdminName = string.Empty;
                Password = string.Empty;
                IsMale = false;
                IsFemale = false;
                Search = string.Empty;
                _gender = string.Empty;

                SelectedAdmins.Clear();
                DsAdmins.Clear();
                //AdminManager.data.ItemsSource = null;
                //AdminsDataGrid();
                //AdminManager.data.ItemsSource = DsAdmins;
                
                //var admins = await _bus.BusGetAllAdmins();

                ////foreach (var admin in admins)
                ////{
                ////    DsAdmins.Add(admin);
                ////    //await Shell.Current.DisplayAlert("Error!", $"{DsAdmins.AdminName}", "Ok");k
                ////}

                ////ObservableCollection<DTO_Admins> admins1 = new();

                //for (int i = 0; i < admins.Count; i++)
                //{
                //    //admins1.Add(admins[i]);
                //    DsAdmins.Add(admins[i]);
                //    await Shell.Current.DisplayAlert("Error!", $"{DsAdmins[i].AdminName}", "Ok");
                //}

                ////DsAdmins.Clear();

                ////DsAdmins = admins1;
                ///

                //await Shell.Current.Navigation.PopToRootAsync(true);






                await Shell.Current.Navigation.PushAsync(new AdminManager());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { IsBusy = false; }
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

        public async void AddAdmin()
        {
            try
            {
                if (AdminName == string.Empty || AdminName == null) throw new Exception("AdminName doesn't must empty");

                if (Email == string.Empty || Email == null || !IsValidEmail(Email)) throw new Exception("Email is invalid, bro");

                if (Password == string.Empty || Password == null) throw new Exception("Password doesn't must empty");

                if (_gender == string.Empty || _gender == null) throw new Exception("Choose your gender, bro !!!");

                DTO_Admins dTO_Admins = new DTO_Admins();
                dTO_Admins.AdminName = AdminName;
                dTO_Admins.Gender = _gender;

                DTO_Accounts dTO_Accounts = new DTO_Accounts();
                dTO_Accounts.Email = Email;
                dTO_Accounts.Password = Password;

                string check = await _bus.BusCreateAdmin(dTO_Admins, dTO_Accounts);

                if (check != string.Empty)
                {
                    throw new Exception(check);
                }
                await Shell.Current.DisplayAlert("Notification!", $"Add admin success!!!", "Ok");
                RefreshFormAdmin();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Add admin failed!: {ex.Message}", "Ok");
            }
        }

        public async void DeleteAdmin()
        {
            try
            {
                if (SelectedAdmins.Count == 0) { throw new Exception("Must choose Admins you want to delete!!"); }

                List<string> emails = new List<string>();

                foreach (var admin in SelectedAdmins)
                {
                    emails.Add(admin.Account.Email);
                }

                string check = await _bus.BusDeleteAdmin(emails);

                if (check != string.Empty)
                {
                    throw new Exception(check);
                }

                await Shell.Current.DisplayAlert("Notification!", $"Delete admin success!!!", "Ok");
                RefreshFormAdmin();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was some problem: {ex.Message}", "Ok");
            }
        }

        public async void UpdateAdmin()
        {
            try
            {
                if (SelectedAdmins.Count == 0) { throw new Exception("Must choose Admin you want to update!!"); }

                if (SelectedAdmins.Count > 1)
                {
                    throw new Exception("Choose only 1 admin to Update !!!");
                }

                AdminName = SelectedAdmins[0].AdminName;
                _gender = SelectedAdmins[0].Gender;
                if (_gender == "Male") { IsMale = true; IsFemale = false; } else if (_gender == "Female") { IsMale = false; IsFemale = true; }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Update Admin failed!: {ex.Message}", "Ok");
            }
        }

        public async void SaveUpdateAdmin()
        {
            try
            {
                DTO_Admins admin = SelectedAdmins[0];
                admin.AdminName = AdminName;
                admin.Gender = _gender;


                bool check = await _bus.BusUpdateAdmin(admin);

                if (!check)
                {
                    throw new Exception("BusUpdateAdmin have a error!!!!");
                }

                await Shell.Current.DisplayAlert("Notification!", $"Update Admin success!!!", "Ok");
                RefreshFormAdmin();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Update Admin failed!: {ex.Message}", "Ok");
            }
        }

        public async void FindAdmin()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                var admins = await _bus.BusFindAdmin(Search);

                DsAdmins.Clear();

                foreach (var admin in admins)
                {
                    DsAdmins.Add(admin);
                }

                //await Shell.Current.DisplayAlert("Notification!", $"Find admin success!!!", "Ok");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Find Admin failed!: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
