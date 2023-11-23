using DAL;
using DTO;
using System.Data;

namespace BUS
{
    public class BUS_Project1
    {
        DAL_Users dal_users = new DAL_Users();
        DAL_Accounts dal_accounts = new DAL_Accounts();
        DAL_Plans dal_plans = new DAL_Plans();

        // User
        public async Task<bool> BusCreateUser(DTO_Users dTO_Users, DTO_Accounts dTO_Accounts)
        {
            try
            {
                dTO_Accounts.Password = BCrypt.Net.BCrypt.HashPassword(dTO_Accounts.Password); // Crypt Password
                dTO_Users.Account = await dal_accounts.CreateAccount(dTO_Accounts);
                dTO_Users.Plan = await dal_plans.GetPlan(1);
                bool check = await dal_users.CreateUser(dTO_Users);

                if (!check)
                {
                    throw new Exception();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> BusGetLoginUser(string email, string password)
        {
            try
            {
                DTO_Users user = await dal_users.GetUserByEmail(email);

                if (user == null)
                {
                    throw new Exception();
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

        public async Task<List<DTO_Users>> BusGetUser()
        {
            try
            {
                List<DTO_Users> users = await dal_users.GetAllUser();

                if (users == null)
                {
                    throw new Exception();
                }

                return users;
            }
            catch
            {
                throw new Exception("Error BUSGetUser");
            }

        }
    }
}