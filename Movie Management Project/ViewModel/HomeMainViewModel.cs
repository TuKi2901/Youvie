using BUS;
using DTO;
using Movie_Management_Project.Content.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Movie_Management_Project.ViewModel
{
    public partial class HomeMainViewModel: BaseViewModel
    {
        BUS_Project1 _bus = new BUS_Project1();

        private DTO_Medias _selectedMedia;
        private string selectedMediaId;

        public ObservableCollection<DTO_Medias> MediainNominated { get; } = new();

        public ICommand SelectedMediaCommand { get; }

        public DTO_Medias SelectedMedia
        {
            get { return _selectedMedia; }
            set { SetProperty(ref _selectedMedia, value); }
        }

        public HomeMainViewModel()
        {
            NominatedCollection();
            SelectedMediaCommand = new Command(SelectedMediaFuntion);
        }

        public async void NominatedCollection()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                List<DTO_Medias> medias = await _bus.BusGetAllMedias();

                foreach (DTO_Medias media in medias)
                {
                    MediainNominated.Add(media);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Unable to get medias: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void SelectedMediaFuntion()
        {
            await Shell.Current.DisplayAlert("Error!", SelectedMedia.Id, "Ok");
            selectedMediaId = SelectedMedia.Id;
            await Shell.Current.Navigation.PushAsync(new Play());
        }
    }
}
