using BUS;
using DTO;
using Movie_Management_Project.Content.Guest;
using Movie_Management_Project.Content.User;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Movie_Management_Project.ViewModel
{
    public class PlayMediaViewModel: BaseViewModel
    {
        private BUS_Project1 _bus = new BUS_Project1();

        private string _episodeText;
        private string _nameMedia;
        private string _idMedia;
        private string _url;
        private string _selectedEpisode;
        private string _decription;
        private string _nameUser;
        private string _comment;

        public ObservableCollection<DTO_Medias> dsMedias { get; } = new();
        public ObservableCollection<string> dsEpisodeCount { get; } = new();
        public ObservableCollection<string> dsEpisode { get; } = new();
        public ObservableCollection<string> dsCategory { get; } = new();
        public ObservableCollection<DTO_Comments> dsComment { get; } = new();

        public ICommand EpisodeCommand { get; }
        public ICommand CommentCommand { get; }

        #region Properties
        public string EpisodeText
        {
            get { return _episodeText; }
            set { SetProperty(ref _episodeText, value); }
        }
        public string NameMedia
        {
            get { return _nameMedia; }
            set { SetProperty(ref _nameMedia, value);}
        }
        public string Comment
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }
        public string NameUser
        {
            get { return _nameUser; }
            set { SetProperty(ref _nameUser, value); }
        }
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
        #endregion

        public PlayMediaViewModel()
        {
            //GetTheMovie(Url);
        }
        public PlayMediaViewModel(string idMedia)
        {
            GetTheMovie(idMedia);
            EpisodeCommand = new Command<string>(ChooseEpisode);
            CommentCommand = new Command(AddComment);
        }

        private async void GetTheMovie(string idMedia)
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                _idMedia = idMedia;
                DTO_Medias media = await _bus.BusGetMediaById(idMedia);

                NameMedia = media.MediaName;

                foreach (string category in media.ListCategory)
                {
                    dsCategory.Add(category);
                }

                Decription = media.Decription;

                dsMedias.Add(media);

                foreach (string episode in media.ListEpisode)
                {
                    dsEpisode.Add(episode);
                }
                //EpisodeCount();

                //List<string> dsCount = new List<string>();

                for (int i = 0; i < dsEpisode.Count; i++)
                {
                    dsEpisodeCount.Add($"Episode {i+1}");
                }

                //foreach (string d in dsCount)
                //{
                //    dsEpisodeCount.Add(d);
                //}


                foreach (DTO_Comments comment in media.Comments)
                {
                    dsComment.Add(comment);
                }

                Url = media.ListEpisode[0];

                GetTheUser(Login.User);

                await Task.Delay(1000);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was a problem: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ChooseEpisode(string episode)
        {
            try
            {
                for (int i = 0; i < dsEpisodeCount.Count; i++)
                {
                    if (episode == dsEpisodeCount[i])
                    {
                        Url = dsEpisode[i];
                    }
                }
                //await Shell.Current.DisplayAlert("Error!", $"{episode}", "Ok");
                //Url = SelectedEpisode;

                //if (SelectedEpisode == null)
                //{
                //    throw new Exception("SelectedEpisode Empty!");
                //}

                //await Shell.Current.Navigation.PushAsync(new Play(new PlayMediaViewModel()));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was a problem: {ex.Message}", "Ok");
            }
        }

        private async void GetTheUser(DTO_Users user)
        {
            try
            {
                //DTO_Users user = await _bus.BusGetUserById(idUser);

                NameUser = user.UserName;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was a problem: {ex.Message}", "Ok");
            }
        }

        private async void AddComment()
        {
            try
            {
                DTO_Comments comment = new DTO_Comments();
                comment.CommentText = Comment;
                comment.CommentDate = DateTime.Now;
                comment.NameUser = NameUser;
                comment.IdMedia = _idMedia;

                bool check = await _bus.BusAddComment(_idMedia, comment);

                if (!check)
                {
                    throw new Exception("Add Failed!");
                }

                await Shell.Current.DisplayAlert("Notification!", $"Comment success!!!", "Ok");
                PlayMediaViewModel playMediaViewModel = new PlayMediaViewModel(_idMedia);
                await Shell.Current.Navigation.PushAsync(new Play(playMediaViewModel));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was a problem: {ex.Message}", "Ok");
            }
        }
    }
}
