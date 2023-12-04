﻿using BUS;
using DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Movie_Management_Project.ViewModel
{
    public class PlayMediaViewModel: BaseViewModel
    {
        private BUS_Project1 _bus = new BUS_Project1();

        private string _idMedia;
        private string _url;
        private string _selectedEpisode;
        private string _decription;
        private string _nameUser;
        private string _comment;

        public ObservableCollection<DTO_Medias> dsMedias { get; } = new();
        public ObservableCollection<string> dsEpisode { get; } = new();
        public ObservableCollection<string> dsCategory { get; } = new();

        public ICommand EpisodeCommand { get; }
        public ICommand CommentCommand { get; }

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

        public PlayMediaViewModel()
        {
            //GetTheMovie(Url);
        }
        public PlayMediaViewModel(string idMedia, string idUser)
        {
            GetTheMovie(idMedia);
            GetTheUser(idUser);
            EpisodeCommand = new Command(ChooseEpisode);
            CommentCommand = new Command(AddComment);
        }

        private async void GetTheMovie(string idMedia)
        {
            try
            {
                _idMedia = idMedia;
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

        private async void GetTheUser(string idUser)
        {
            try
            {
                DTO_Users user = await _bus.BusGetUserById(idUser);

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
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was a problem: {ex.Message}", "Ok");
            }
        }
    }
}
