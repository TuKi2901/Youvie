﻿using BUS;
using DTO;
using Movie_Management_Project.Content.Admin;
using System.Collections.ObjectModel;
using System.Net.Mail;
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

        public ObservableCollection<DTO_Users> SelectedUsers { get; set; } = new ObservableCollection<DTO_Users>();
        public ObservableCollection<DTO_Users> dsUsers { get; } = new();
        public ICommand AddUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand UpdateUserCommnad {  get; }

        public UsersManagerViewModel()
        {
            UsersDataGrid();
            AddUserCommand = new Command(AddUser);
            DeleteUserCommand = new Command(DeleteUser);
            RefreshCommand = new Command(RefreshFormUser);
            UpdateUserCommnad = new Command(UpdateUser);
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
        #endregion

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

        public async void AddUser()
        {
            try
            {
                if (UserName == string.Empty || UserName == null) throw new Exception("UserName doesn't must empty");

                if (Email == string.Empty || Email == null || !IsValidEmail(Email)) throw new Exception("Email is invalid, bro");

                if (Password == string.Empty || Password == null) throw new Exception("Password doesn't must empty");

                if (PhoneNumber == string.Empty || PhoneNumber == null || !long.TryParse(PhoneNumber, out _)) throw new Exception("PhoneNumber is invalid, bro");

                if (Country == string.Empty || Country == null) throw new Exception("Country doesn't must empty");

                if (_gender == string.Empty || _gender == null) throw new Exception("Choose your gender, bro !!!");

                DTO_Users dTO_Users = new DTO_Users();
                dTO_Users.UserName = UserName;
                dTO_Users.PhoneNumber = PhoneNumber;
                dTO_Users.Birthday = Birthday;
                dTO_Users.Country = Country;
                dTO_Users.Gender = _gender;

                DTO_Accounts dTO_Accounts = new DTO_Accounts();
                dTO_Accounts.Email = Email;
                dTO_Accounts.Password = Password;

                string check = await _bus.BusCreateUser(dTO_Users, dTO_Accounts);

                if (check != string.Empty)
                {
                    throw new Exception(check);
                }

                await Shell.Current.DisplayAlert("Notification!", $"Add user success!!!", "Ok");
                RefreshFormUser();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Add User failed!: {ex.Message}", "Ok");
            }
        }

        public async void DeleteUser()
        {
            try
            {
                if (SelectedUsers.Count == 0) { throw new Exception("Must choose Users you want to delete!!"); }    

                List<string> emails = new List<string>();

                foreach (var user in SelectedUsers)
                {
                    emails.Add(user.Account.Email);
                }

                string check = await _bus.DeleteUsernAccount(emails);

                if (check != string.Empty)
                {
                    throw new Exception(check);
                }

                await Shell.Current.DisplayAlert("Notification!", $"Delete user success!!!", "Ok");
                RefreshFormUser();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was some problem: {ex.Message}", "Ok");
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

        public async void RefreshFormUser()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                dsUsers.Clear();

                List<DTO_Users> users = await _bus.BusGetUser();

                foreach (DTO_Users user in users)
                {
                    dsUsers.Add(user);
                }

                UserName = string.Empty;
                PhoneNumber = string.Empty;
                Birthday = DateTime.MinValue;
                Email = string.Empty;
                Password = string.Empty;
                Country = string.Empty;
                _gender = string.Empty;
                IsMale = false;
                IsFemale = false;

            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void UpdateUser()
        {
            try
            {
                //if (SelectedUsers.Count == 0) { throw new Exception("Must choose Users you want to update!!"); }

                //if (UserName == string.Empty || UserName == null) throw new Exception("UserName doesn't must empty");

                //if (Email == string.Empty || Email == null || !IsValidEmail(Email)) throw new Exception("Email is invalid, bro");

                //if (Password == string.Empty || Password == null) throw new Exception("Password doesn't must empty");

                //if (PhoneNumber == string.Empty || PhoneNumber == null || !long.TryParse(PhoneNumber, out _)) throw new Exception("PhoneNumber is invalid, bro");

                //if (Country == string.Empty || Country == null) throw new Exception("Country doesn't must empty");

                //if (_gender == string.Empty || _gender == null) throw new Exception("Choose your gender, bro !!!");

                List<DTO_Users> users = new List<DTO_Users>();

                foreach (DTO_Users u in SelectedUsers)
                {
                    users.Add(u);
                }

                bool check = await _bus.BusUpdateUser(users);

                if (!check)
                {
                    throw new Exception("BusUpdateUser have a error!!!!");
                }

                await Shell.Current.DisplayAlert("Notification!", $"Update user success!!!", "Ok");
                await Shell.Current.Navigation.PushAsync(new UserManager());
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Update User failed!: {ex.Message}", "Ok");
            }
        }
    }
}
