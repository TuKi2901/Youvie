using BUS;
using DTO;
using Movie_Management_Project.Content.Admin;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace Movie_Management_Project.ViewModel
{
    public partial class UsersManagerViewModel : BasicViewModel
    {
        private BUS_Project1 _bus = new BUS_Project1();

        private string _userName;
        private string _email;
        private string _password;
        private string _country;
        private DateTime _birthday;
        private string _phoneNumber;
        private string _gender;

        public ObservableCollection<DTO_Users> dsUsers { get; } = new();
        public ICommand AddUserCommand { get; }

        public UsersManagerViewModel()
        {
            UsersDataGrid();
            AddUserCommand = new Command(AddUser);
        }

        #region SetProperty
        public string UserName
        {
            get { return _userName; }
            set
            { SetProperty(ref _userName, value); }
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
        public string Country
        {
            get { return _country; }
            set
            { SetProperty(ref _country, value); }
        }
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            { SetProperty(ref _birthday, value); }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            { SetProperty(ref _phoneNumber, value); }
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

        public IReadOnlyList<Page> ModalStack => throw new NotImplementedException();

        public IReadOnlyList<Page> NavigationStack => throw new NotImplementedException();
        #endregion

        public async void AddUser()
        {
            try
            {
                DTO_Users dTO_Users = new DTO_Users();
                dTO_Users.UserName = UserName;
                dTO_Users.PhoneNumber = PhoneNumber;
                dTO_Users.Birthday = Birthday;
                dTO_Users.Country = Country;
                dTO_Users.Gender = _gender;

                DTO_Accounts dTO_Accounts = new DTO_Accounts();
                dTO_Accounts.Email = Email;
                dTO_Accounts.Password = Password;

                bool check = await _bus.BusCreateUser(dTO_Users, dTO_Accounts);

                if (!check)
                {
                    throw new Exception();

                }

                UserName = string.Empty;
                PhoneNumber = string.Empty;
                Birthday = DateTime.MinValue;
                Email = string.Empty;
                Password = string.Empty;
                Country = string.Empty;
                _gender = string.Empty;

                await Shell.Current.DisplayAlert("Notification!", $"Add user success!!!", "Ok");
                await Shell.Current.Navigation.PushAsync(new UserManager());
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Add User failed!: {ex.Message}", "Ok");
            }
        }

        public async void UsersDataGrid()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                List<DTO_Users> users = await _bus.BusGetUser();

                foreach (DTO_Users user in users)
                {
                    dsUsers.Add(user);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Unable to get users: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
