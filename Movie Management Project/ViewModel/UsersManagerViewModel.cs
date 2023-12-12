using BUS;
using DTO;
using Movie_Management_Project.Content.Admin;
using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Windows.Input;
using DAL;
using Movie_Management_Project.Content.Guest;
using Movie_Management_Project.Content.User;

namespace Movie_Management_Project.ViewModel
{
    public partial class UsersManagerViewModel : BaseViewModel
    {
        DTO_Users user = Login.User;
        private BUS_Project1 _bus = new BUS_Project1();
        private string _userName;
        private string _email;
        private string _password;
        private string _country;
        private DateTime _birthday;
        private string _phoneNumber;
        private string _gender;
        private string _search;
        private string _newpassword;
        private string _confirmpassword;
        private string _adminname;

        public ObservableCollection<DTO_Users> SelectedUsers { get; set; } = new ObservableCollection<DTO_Users>();
        public ObservableCollection<DTO_Users> dsUsers { get; } = new();
        public ICommand AddUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand UpdateUserCommand { get; }
        public ICommand SaveUpdateCommand { get; }
        public ICommand FindUserCommand { get; }
        public ICommand ForgotPasswordCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand ChangePasswordCommand { get; }

        //private readonly ICommand newPassword;

        //public ICommand GetNewPassword()
        //{
        //    return newPassword;
        //}

        public UsersManagerViewModel()
        {
            UsersDataGrid();
            #region Guest
            //Login = new Command(Login);
            ForgotPasswordCommand = new Command(ForgotPassword);
            ChangePasswordCommand = new Command(ChangePasswordVM);
            #endregion

            #region User Manager
            AddUserCommand = new Command(AddUser);
            DeleteUserCommand = new Command(DeleteUser);
            UpdateUserCommand = new Command(UpdateUser);
            SaveUpdateCommand = new Command(SaveUpdateUser);
            FindUserCommand = new Command(FindUser);
            RefreshCommand = new Command(RefreshFormUser);
            #endregion
        }

        #region SetProperty

        public string AdminName
        {
            get { return _adminname; }
            set
            {
                if (_adminname != value)
                {
                    _adminname = value;
                    OnPropertyChanged(nameof(AdminName));
                }
            }
        }

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

        public string NewPassword
        {
            get { return _newpassword; }
            set
            { SetProperty(ref _newpassword, value); }
        }
        public string ConfirmPassword
        {
            get { return _confirmpassword; }
            set
            { SetProperty(ref _confirmpassword, value); }
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

        #region Forgot Password
        public string RandomPassword()
        {
            //giới hạn độ dài password
            byte[] bytes = new byte[8];
            //dùng thư viện để random
            RandomNumberGenerator.Create().GetBytes(bytes);
            //chuyển đổi byte thành string
            string result = Convert.ToBase64String(bytes);
            return result;
        }
        //gửi mail khi quên mật khẩu
        public string SendMail(string recipientEmail, string randomPassword)
        {
            try
            {
                //tạo biến format nội dung(body) trong email
                string bodyHtml = $@"
                <p style=""font-size: 18px;"">
                    Bạn đã sử dụng chức năng Quên Mật Khẩu
                    <br>
                    Đây là mật khẩu mới của bạn
                    <br>
                </p>
    
                <p style=""border: 2px ;background-color: rgb(0, 101, 209);font-size: 40px; color: white; letter-spacing: 5px; text-align: center; width: auto;"">
                        {randomPassword}
                </p>";

                //tạo biến "mail" để tùy chỉnh các thuộc tính trong thư (email)
                MailMessage mail = new MailMessage();

                //tạo biến smtp: giao thức truyền tải email, kết nối với "gmail"
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                //mail người gửi
                mail.From = new MailAddress("kiet43012@gmail.com");

                //mail người nhận (truyền tham số)
                mail.To.Add(recipientEmail);

                //tiêu đề mail
                mail.Subject = "Forgot Password";

                //xác định mail được viết bằng html
                mail.IsBodyHtml = true;

                //gọi lại biến được khai báo ở trên
                mail.Body = bodyHtml;

                //chọn cổng của google
                smtp.Port = 587;

                //không dùng thông tin đăng nhập mặc định
                smtp.UseDefaultCredentials = false;

                //đăng nhập tài khoản cần gửi mail, mã đăng nhập vào mail
                smtp.Credentials = new NetworkCredential("kiet43012@gmail.com", "cywt jdam bmuw fqwq");

                //sử dụng giao thức ssl để kết nối máy chủ
                smtp.EnableSsl = true;

                //gửi mail
                smtp.Send(mail);

                //thông báo khi gửi mail thành công
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async void UpdatePassword(string randomPassword)
        {
            try
            {
                if (await _bus.BusForgotPassword(Email, randomPassword) != string.Empty)
                    throw new Exception("Error BusForgotPassword");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async void ForgotPassword()
        {
            try
            {
                string randomPassword = RandomPassword();
                if (IsValidEmail(Email) == false)
                    throw new Exception("Email is invalid");
                if (await _bus.BusForgotPassword(Email, randomPassword) != string.Empty)
                    throw new Exception("Mail is not exists");
                if (SendMail(Email, randomPassword) != string.Empty)
                    throw new Exception("Send Mail is error");
                await Shell.Current.DisplayAlert("Notification!", $"New password sent your mail, Please check your mail!!!", "Ok");

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Notification!", $"Forgot password feature is error!!!\n{ex.Message}", "Ok");
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

        //public async void Login(string email, string password) 
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

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

        private async void DeleteUser()
        {
            try
            {
                if (SelectedUsers.Count == 0) { throw new Exception("Must choose Users you want to delete!!"); }    

                List<string> userIds = new List<string>();
                List<string> accountIds = new List<string>();

                foreach (var user in SelectedUsers)
                {
                    userIds.Add(user.Id);
                    accountIds.Add(user.Account.Id);
                }

                string check = await _bus.DeleteUsernAccount(userIds, accountIds);

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

        private async void UsersDataGrid()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                List<DTO_Users> users = await _bus.BusGetAllUser();

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

        private async void RefreshFormUser()
        {
            await Shell.Current.Navigation.PushAsync(new UserManager());
        }

        private async void UpdateUser()
        {
            try
            {
                if (SelectedUsers.Count == 0) { throw new Exception("Must choose User you want to update!!"); }

                if (SelectedUsers.Count > 1)
                {
                    throw new Exception("Choose only 1 user to Update !!!");
                }

                UserName = SelectedUsers[0].UserName;
                PhoneNumber = SelectedUsers[0].PhoneNumber;
                _gender = SelectedUsers[0].Gender;
                if (_gender == "Male") { IsMale = true; IsFemale = false; }  else if (_gender == "Female") { IsMale = false; IsFemale = true; }
                Country = SelectedUsers[0].Country;
                Birthday = SelectedUsers[0].Birthday;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was a problem!: {ex.Message}", "Ok");
            }
        }

        private async void SaveUpdateUser()
        {
            try
            {
                if (SelectedUsers.Count < 1)
                {
                    throw new Exception("Must update before save!!");
                }

                DTO_Users user = SelectedUsers[0];
                user.UserName = UserName;
                user.PhoneNumber = PhoneNumber;
                user.Birthday = Birthday;
                user.Country = Country;
                user.Gender = _gender;

                bool check = await _bus.BusUpdateUser(user);

                if (!check)
                {
                    throw new Exception("Update user failed!!");
                }

                await Shell.Current.DisplayAlert("Notification!", $"Update user success!!!", "Ok");
                RefreshFormUser();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"{ex.Message}", "Ok");
            }
        }

        private async void FindUser()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                var users = await _bus.BusFindUser(Search);

                dsUsers.Clear();

                foreach (var user in users)
                {
                    dsUsers.Add(user);
                }

                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Find User failed!: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ChangePasswordVM()
        {
            try
            {
                if (Password == null)
                {
                    await Shell.Current.DisplayAlert("Notification!", "Please Input Your Password", "Ok");
                }
                else if (NewPassword == null)
                {
                    await Shell.Current.DisplayAlert("Notification!", "Please Input New Password", "Ok");
                }
                else if (ConfirmPassword == null)
                {
                    await Shell.Current.DisplayAlert("Notification!", "Please Input Confirm Password", "Ok");
                }
                else if (NewPassword != ConfirmPassword)
                {
                    await Shell.Current.DisplayAlert("Notification!", "Confirm Password was wrong", "Ok");
                }
                else
                {
                    bool checkPassword = BCrypt.Net.BCrypt.Verify(Password, user.Account.Password);
                    if (checkPassword)
                    {
                        if (await _bus.BusChangePassword(user.Account.Email, NewPassword)==string.Empty)
                        {
                            await Shell.Current.DisplayAlert("Notification!", $"Change Password Success", "Ok");
                            Password = null;
                            NewPassword = null;
                            ConfirmPassword = null;
                        }
                    }
                    else
                    await Shell.Current.DisplayAlert("Notification!", "Your Password is not correct", "Ok");
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Notification!", $"Change Password Unsuccess {ex.Message}", "Ok");
            }
        }
    }
}
