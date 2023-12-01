using BUS;
using DTO;
using System.Collections.ObjectModel;

namespace Movie_Management_Project.ViewModel
{
    public partial class MediaManagerViewModel: BaseViewModel
    {
        BUS_Project1 _bus = new BUS_Project1();

        public ObservableCollection<DTO_Medias> dsMedias { get; } = new();

        public MediaManagerViewModel()
        {
            MediaDataGrid();
        }

        public async void MediaDataGrid()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                List<DTO_Medias> medias = await _bus.BusGetAllMedias();

                foreach(DTO_Medias media in medias)
                {
                    dsMedias.Add(media);
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
