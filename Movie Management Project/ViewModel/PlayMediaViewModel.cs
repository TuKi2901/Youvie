using BUS;
using DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Movie_Management_Project.ViewModel
{
    public class PlayMediaViewModel: BaseViewModel
    {
        private BUS_Project1 _bus = new BUS_Project1();

        private string _url;
        private string _selectedEpisode;
        private string _decription;

        public ObservableCollection<DTO_Medias> dsMedias { get; } = new();
        public ObservableCollection<string> dsEpisode { get; } = new();
        public ObservableCollection<string> dsCategory { get; } = new();

        public ICommand EpisodeCommand { get; }

        public string Url
        {
            get { return _url; }
            set { SetProperty(ref _url, value); }
        }
        public string SelectedEpisode
        {
            get { return _selectedEpisode; }
            set { SetProperty(ref _selectedEpisode, value); }
        }
        public string Decription
        {
            get { return _decription; }
            set { SetProperty(ref _decription, value); }
        }

        public PlayMediaViewModel()
        {
            //GetTheMovie(Url);
        }
        public PlayMediaViewModel(string idMedia = "")
        {
            GetTheMovie(idMedia);
            EpisodeCommand = new Command(ChooseEpisode);
        }

        private async void GetTheMovie(string idMedia)
        {
            try
            {
                DTO_Medias media = await _bus.BusGetMediaById(idMedia);
                dsMedias.Add(media);

                Decription = media.Decription;

                foreach (string episode in media.ListEpisode)
                {
                    dsEpisode.Add(episode);
                }

                foreach (string category in media.ListCategory)
                {
                    dsCategory.Add(category);
                }

                Url = media.ListEpisode[0];
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was a problem: {ex.Message}", "Ok");
            }
        }

        private async void ChooseEpisode()
        {
            try
            {
                Url = SelectedEpisode;

                if (SelectedEpisode == null)
                {
                    throw new Exception("SelectedEpisode Empty!");
                }

                
                //await Shell.Current.Navigation.PushAsync(new Play(new PlayMediaViewModel()));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was a problem: {ex.Message}", "Ok");
            }
        }
    }
}
