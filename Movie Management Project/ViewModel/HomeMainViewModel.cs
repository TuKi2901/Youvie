using BUS;
using DTO;
using Movie_Management_Project.Content.User;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Movie_Management_Project.ViewModel
{
    public partial class HomeMainViewModel: BaseViewModel
    {
        BUS_Project1 _bus = new BUS_Project1();

        private string _idUser;
        private DTO_Medias _selectedMedia;

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

        public HomeMainViewModel(string userId)
        {
            _idUser = userId;
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
            try
            {
                if (SelectedMedia == null)
                {
                    throw new Exception("You must choose the media you want!");
                }

                await Shell.Current.DisplayAlert("Error!", SelectedMedia.MediaName, "Ok");

                PlayMediaViewModel playMediaViewModel = new PlayMediaViewModel(SelectedMedia.Id, _idUser);
                await Shell.Current.Navigation.PushAsync(new Play(playMediaViewModel));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was a problem {ex.Message}", "Ok");
            }
        }
    }
}
