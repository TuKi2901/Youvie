using DAL;
using DTO;
using MongoDB.Driver;
using System.Collections;
using System.Data;

namespace BUS
{
    public class BUS_Project1
    {
        DAL_Users dal_users = new DAL_Users();
        DAL_Accounts dal_accounts = new DAL_Accounts();
        DAL_Plans dal_plans = new DAL_Plans();
        DAL_Admins dal_admins = new DAL_Admins();
        DAL_Medias dal_medias = new DAL_Medias();

        // User
        #region User
        public async Task<string> BusCreateUser(DTO_Users dTO_Users, DTO_Accounts dTO_Accounts)
        {
            try
            {
                dTO_Accounts.Password = BCrypt.Net.BCrypt.HashPassword(dTO_Accounts.Password); // Crypt Password
                dTO_Users.Account = await dal_accounts.CreateAccount(dTO_Accounts);
                dTO_Users.Plan = await dal_plans.GetPlan(1);
                bool check = await dal_users.CreateUser(dTO_Users);

                if (!check)
                {
                    throw new Exception("CreateUser failed!");
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<bool> BusGetLoginUser(string email, string password)
        {
            try
            {
                DTO_Users user = await dal_users.GetUserByEmail(email);

                if (user.Account == null)
                {
                    throw new Exception("user.Account mustn't null");
                }

                bool checkPass = BCrypt.Net.BCrypt.Verify(password, user.Account.Password);

                if (!checkPass)
                {
                    throw new Exception();
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<List<DTO_Users>> BusGetAllUser()
        {
            try
            {
                List<DTO_Users> users = await dal_users.GetAllUser();

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<string> BusDeleteUser(List<string> email)
        {
            try
            {
                bool checkDeleteAccount = await dal_accounts.DeleteAccount(email);

                if (!checkDeleteAccount)
                {
                    throw new Exception($"Can't delete Accounts");
                }

                bool checkDeleteUser = await dal_users.DeleteUser(email);

                if (!checkDeleteUser)
                {
                    throw new Exception($"Can't delete User");
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<bool> BusUpdateUser(DTO_Users _user)
        {
            try
            {
                await dal_users.UpdateUser(_user);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<DTO_Users>> BusFindUser(string infoUser)
        {
            try
            {
                List<DTO_Users> users = await dal_users.FindUserWith(infoUser);

                return users;
            }
            catch
            {
                throw new Exception("Error in BUS_FindUserWith");
            }
        }

        public async Task<string> BusForgotPassword(string email, string password)
        {
            try
            {
                if (await dal_users.GetUserByEmail(email) == null)
                    throw new Exception("Email is not exists");
                DTO_Accounts accounts = new DTO_Accounts();
                accounts.Email = email;
                accounts.Password = BCrypt.Net.BCrypt.HashPassword(password);
                await dal_accounts.UpdateAccount(accounts);
                await dal_users.UpdatePasswordUser(email, accounts.Password);
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception($"BusForgotPassword is error\n{ex.Message}");
            }
        }
        #endregion

        // Admin
        #region Admin
        public async Task<List<DTO_Admins>> BusGetAllAdmins()
        {
            try
            {
                List<DTO_Admins> admins = await dal_admins.GetAllAdmin();

                return admins;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> BusCreateAdmin(DTO_Admins dTO_Admins, DTO_Accounts dTO_Accounts)
        {
            try
            {
                dTO_Accounts.Password = BCrypt.Net.BCrypt.HashPassword(dTO_Accounts.Password); // Crypt Password
                dTO_Admins.Account = await dal_accounts.CreateAccount(dTO_Accounts);
                bool check = await dal_admins.CreateAdmin(dTO_Admins);
                if (!check)
                {
                    throw new Exception("CreateAdmin failed!");
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> BusDeleteAdmin(List<string> email)
        {
            try
            {
                bool checkDeleteAccount = await dal_accounts.DeleteAccount(email);

                if (!checkDeleteAccount)
                {
                    throw new Exception($"Can't delete Accounts");
                }

                bool checkDeleteUser = await dal_admins.DeleteAdmin(email);

                if (!checkDeleteUser)
                {
                    throw new Exception($"Can't delete Admin");
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<bool> BusUpdateAdmin(DTO_Admins admin)
        {
            try
            {
                await dal_admins.UpdateAdmin(admin);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<DTO_Admins>> BusFindAdmin(string infoAdmins)
        {
            try
            {
                List<DTO_Admins> admins = await dal_admins.FindAdminWith(infoAdmins);

                return admins;
            }
            catch
            {
                throw new Exception("Error in BUS_FindUserWith");
            }
        }
        #endregion

        // Media
        #region Media
        public async Task<List<DTO_Medias>> BusGetAllMedias()
        {
            try
            {
                List<DTO_Medias> medias = await dal_medias.GetAllMedia();

                return medias;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion 
    }
}