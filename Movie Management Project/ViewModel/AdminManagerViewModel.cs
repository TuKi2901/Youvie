using BUS;
using DTO;
using System.Collections.ObjectModel;

namespace Movie_Management_Project.ViewModel
{
    public partial class AdminManagerViewModel : BasicViewModel
    {
        private BUS_Project1 _bus = new BUS_Project1();

        public ObservableCollection<DTO_Admins> dsAdmins { get; } = new();

        public AdminManagerViewModel()
        {
            AdminsDataGrid();
        }

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

                foreach(DTO_Admins admin in admins)
                {
                    dsAdmins.Add(admin);
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
    }
}
