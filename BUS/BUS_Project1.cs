using DAL;
using DTO;

namespace BUS
{
    public class BUS_Project1
    {
        DAL_Users dal_users = new DAL_Users();
        DAL_Accounts dal_accounts = new DAL_Accounts();
        DAL_Plans dal_plans = new DAL_Plans();
        DAL_Admins dal_admins = new DAL_Admins();
        DAL_Medias dal_medias = new DAL_Medias();
        DAL_Comments DAL_Comments = new DAL_Comments();

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

        //public async Task<bool> BusGetLoginUser(string email, string password)
        //{
        //    try
        //    {
        //        DTO_Users user = await dal_users.GetUserByEmail(email);

        //        if (user == null)
        //        {
        //            return false;
        //        }

        //        bool checkPass = BCrypt.Net.BCrypt.Verify(password, user.Account.Password);

        //        if (!checkPass)
        //        {
        //            throw new Exception("Password is not correct!");
        //        }

        //        return true;
        //    }
        //    catch { return false; }
        //}

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

        public async Task<string> DeleteUsernAccount(List<string> userId, List<string> accountId)
        {
            try
            {
                bool checkDeleteAccount = await dal_accounts.DeleteAccount(accountId);

                if (!checkDeleteAccount)
                {
                    throw new Exception($"Can't delete Accounts");
                }

                bool checkDeleteUser = await dal_users.DeleteUserDAL(userId);

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

        public async Task<DTO_Users> BusGetUserById(string idUser)
        {
            DTO_Users user = await dal_users.GetUserById(idUser);

            return user;
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

        public async Task<bool> BusAddMedia(DTO_Medias dTO_Medias)
        {
            try
            {
                bool check = await dal_medias.AddMedia(dTO_Medias);

                if (!check)
                {
                    throw new Exception("Add Media Failed!");
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> BusDeleteMedia(List<string> mediaID)
        {
            try
            {
                bool check = await dal_medias.DeleteMedia(mediaID);

                if (!check)
                {
                    throw new Exception("Can't delete Media!");
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> BusUpdateMedia(DTO_Medias media)
        {
            try
            {
                await dal_medias.UpdateMedia(media);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<DTO_Medias>> BusFindMedia(string infoMedia)
        {
            try
            {
                List<DTO_Medias> medias = await dal_medias.FindMedia(infoMedia);

                return medias;
            }
            catch
            {
                throw new Exception("Error in BUS_FindMedia");
            }
        }

        public async Task<DTO_Medias> BusGetMediaById(string mediaId)
        {
            try
            {
                DTO_Medias media = await dal_medias.GetMediaById(mediaId);

                return media;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        
        public async Task<bool> BusAddComment(string mediaId, DTO_Comments dTO_Comments)
        {
            try
            {
                DTO_Comments comment = await DAL_Comments.AddComment(dTO_Comments);

                bool check = await dal_medias.AddCommentInMedia(dTO_Comments, mediaId);

                if (!check)
                {
                    return false;
                }

                return true;
            }
            catch 
            {
                return false;
            }
        }
        #endregion 


        public async Task<dynamic> CheckUserOrAdmin(string email, string pass)
        {
            try
            {
                DTO_Users user = await dal_users.GetUserByEmail(email);

                if (user != null)
                {
                    bool checkPass = BCrypt.Net.BCrypt.Verify(pass, user.Account.Password);

                    if (!checkPass)
                    {
                        throw new Exception("Password is not correct!");
                    }

                    return user;
                }

                DTO_Admins admin = await dal_admins.GetAdminByEmail(email);

                if (admin != null)
                {
                    bool checkPass = BCrypt.Net.BCrypt.Verify(pass, admin.Account.Password);

                    if (!checkPass)
                    {
                        throw new Exception("Password is not correct!");
                    }

                    return admin;
                }

                throw new Exception("Email is not correct!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}