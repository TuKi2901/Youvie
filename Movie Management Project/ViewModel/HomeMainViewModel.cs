using BUS;
using DTO;
using Movie_Management_Project.Content.Guest;
using Movie_Management_Project.Content.User;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Movie_Management_Project.ViewModel
{
    public partial class HomeMainViewModel : BaseViewModel
    {
        BUS_Project1 _bus = new BUS_Project1();

        private DTO_Users _idUser;
        private DTO_Medias _selectedMedia;
        private string _search;

        public ObservableCollection<DTO_Medias> MediainNominated { get; } = new();
        public ObservableCollection<DTO_Medias> MediainPopular { get; } = new();

        public ICommand SelectedMediaCommand { get; }
        public ICommand SearchMediaCommand { get; }

        public string Search
        {
            get { return _search; }
            set { SetProperty(ref _search, value); }
        }
        public DTO_Medias SelectedMedia
        {
            get { return _selectedMedia; }
            set { SetProperty(ref _selectedMedia, value); }
        }

        public HomeMainViewModel()
        {
            _idUser = Login.User;
            NominatedCollection();
            SelectedMediaCommand = new Command(SelectedMediaFuntion);
        }

        //public HomeMainViewModel(string userId)
        //{
        //    NominatedCollection();
        //    SelectedMediaCommand = new Command(SelectedMediaFuntion);
        //}

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

                for (int i = medias.Count - 1; i >= 0; i--)
                {
                    MediainPopular.Add(medias[i]);
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

                PlayMediaViewModel playMediaViewModel = new PlayMediaViewModel(SelectedMedia.Id);
                await Shell.Current.Navigation.PushAsync(new Play(playMediaViewModel));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was a problem {ex.Message}", "Ok");
            }
        }

        public async void SearchMedia()
        {
            try
            {
                if (Search == null)
                {
                    throw new Exception("Must input the Name Media that you need to search!");
                }

                List<DTO_Medias> medias = await _bus.BusFindMedia(Search);

                SearchViewModel searchViewModel = new SearchViewModel(medias);
                await Shell.Current.Navigation.PushAsync(new Search(searchViewModel));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was a problem {ex.Message}", "Ok");
            }
        }
    }
}
