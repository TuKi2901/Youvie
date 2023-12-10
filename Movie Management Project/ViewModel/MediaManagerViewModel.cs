using BUS;
using DTO;
using Microsoft.Maui.Storage;
using Movie_Management_Project.Content.Admin;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Movie_Management_Project.ViewModel
{
    public partial class MediaManagerViewModel: BaseViewModel
    {
        BUS_Project1 _bus = new BUS_Project1();

        private string _chooseCategory;
        private string _backCategory;
        private string _urlEpisode;
        private string _mediaName;
        private string _country;
        private DateTime _releaseDate;
        private List<string> _listCategory = new List<string>();
        private List<string> _listEpisode = new List<string>();
        private int _role = 0;
        private string _description;
        private string _image;
        private string _search;

        public ObservableCollection<DTO_Medias> SelectedMedias { get; set; } = new ObservableCollection<DTO_Medias>();
        public ObservableCollection<DTO_Medias> dsMedias { get; } = new();
        public ObservableCollection<string> dsCategorys { get; } = new();
        public ObservableCollection<string> dsCategorys2 { get; } = new();
        public ObservableCollection<string> dsEpisodes { get; } = new();

        public ICommand ChooseCategoryCommand { get; }
        public ICommand BackCategoryCommand { get; }
        public ICommand AddUrlEpisodeCommand { get; }
        public ICommand OpenFileCommand { get; }
        public ICommand AddMediaCommand { get; }
        public ICommand DeleteMediaCommand { get; }
        public ICommand RefreshMediaCommand { get; }
        public ICommand UpdateButtonCommand { get; }
        public ICommand SaveUpdateCommand { get; }
        public ICommand FindMediaCommand { get; }

        #region Set Properties
        public string ChooseCategory
        {
            get { return _chooseCategory; }
            set { SetProperty(ref _chooseCategory, value); }
        }

        public string BackCategory
        {
            get { return _backCategory; }
            set { SetProperty(ref _backCategory, value); }
        }

        public string UrlEpisode
        {
            get { return _urlEpisode; }
            set { SetProperty(ref _urlEpisode, value); }
        }

        public string MediaName
        {
            get { return _mediaName; }
            set { SetProperty(ref _mediaName, value); }
        }

        public string Country
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
        }
        public DateTime ReleaseDate
        {
            get { return _releaseDate; }
            set { SetProperty(ref _releaseDate, value); }
        }
        //public List<string> ListCategory
        //{
        //    get { return _listCategory; }
        //    set { SetProperty(ref _listCategory, value); }
        //}
        //public List<string> ListEpisode
        //{
        //    get { return _listEpisodes; }
        //    set { SetProperty(ref _listEpisodes, value); }
        //}
        public int Role
        {
            get { return _role; }
            set { SetProperty(ref _role, value); }
        }
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        public string Imagee
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }
        public string Search
        {
            get { return _search; }
            set { SetProperty(ref _search, value); }
        }
        #endregion

        public MediaManagerViewModel()
        {
            MediaDataGrid();
            CategoryDatagrid();
            ChooseCategoryCommand = new Command(ChooseCategoryButton);
            BackCategoryCommand = new Command(BackCategoryButton);
            AddUrlEpisodeCommand = new Command(AddEpisodeIntoList);
            OpenFileCommand = new Command(async () => await PickFile());
            AddMediaCommand = new Command(AddMedia);
            DeleteMediaCommand = new Command(DeleteMedia);
            RefreshMediaCommand = new Command(RefreshFormMedia);
            UpdateButtonCommand = new Command(UpdateButton);
            SaveUpdateCommand = new Command(SaveUpdate);
            FindMediaCommand = new Command(FindMedia);
        }

        private async void MediaDataGrid()
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
                await Shell.Current.DisplayAlert("Error!", $"Unable to get medias: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void CategoryDatagrid()
        {
            List<string> categorys = new List<string>()
            {
                "Action",
                "Honor",
                "Romantic",
                "Anime",
                "Cartoon"
            };

            foreach (string category in categorys)
            {
                dsCategorys.Add(category);
            }
        }

        private async void ChooseCategoryButton()
        {
            try
            {
                foreach (string category in dsCategorys2)
                {
                    if (ChooseCategory == category)
                    {
                        ChooseCategory = string.Empty;
                    }
                }

                if (ChooseCategory == string.Empty || ChooseCategory == null)
                {
                    return;
                }

                dsCategorys2.Add(ChooseCategory);
                _listCategory.Add(ChooseCategory);
                dsCategorys.Remove(ChooseCategory);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Error when ChooseCategory: {ex.Message}", "Ok");
            }
        }

        private async void BackCategoryButton()
        {
            try
            {
                foreach (string category in dsCategorys)
                {
                    if (BackCategory == category)
                    {
                        BackCategory = string.Empty;
                    }
                }

                if (BackCategory == string.Empty || BackCategory == null)
                {
                    return;
                }

                dsCategorys.Add(BackCategory);
                dsCategorys2.Remove(BackCategory);
                _listCategory.Remove(BackCategory);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Error when BackCategory: {ex.Message}", "Ok");
            }
        }

        private async void AddEpisodeIntoList()
        {
            try
            {
                if (UrlEpisode == null || UrlEpisode == string.Empty) { throw new Exception("UrlEpisode mustn't empty!!"); }

                dsEpisodes.Add(UrlEpisode);
                _listEpisode.Add(UrlEpisode);

                UrlEpisode = string.Empty;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "Ok");
            }
        }

        private async Task PickFile()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Pick an image"
                });

                Imagee = result.FullPath;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!","Error when PickFile: " + ex.Message, "Ok");
            }
        }

        private async void RefreshFormMedia()
        {
            await Shell.Current.Navigation.PushAsync(new MediaManager());
        }

        private async void AddMedia()
        {
            try
            {
                if (MediaName == string.Empty || MediaName == null) throw new Exception("MediaName mustn't empty!");
                if (Country == string.Empty || Country == null) throw new Exception("Choose the media's country!");
                if (_listCategory.Count == 0) throw new Exception("Choose category for the media!");
                if (_listEpisode.Count == 0) throw new Exception("Add episode for the media!");
                if (Role == 0) throw new Exception("Choose role for the media!");
                if (Description == string.Empty || Description == null) throw new Exception("Write description for the media!");
                if (Imagee == string.Empty || Imagee == null) throw new Exception("Add Imgae for the media!");

                DTO_Medias media = new DTO_Medias();
                media.MediaName = MediaName;
                media.Country = Country;
                media.ReleaseDate = ReleaseDate;
                media.ListCategory = _listCategory;
                media.ListEpisode = _listEpisode;
                media.RoleMedia = Role;
                media.Decription = Description;
                media.Image = Imagee;
                media.Comments = new List<DTO_Comments>();

                bool check = await _bus.BusAddMedia(media);

                if (!check)
                {
                    throw new Exception();
                }

                await Shell.Current.DisplayAlert("Notification!", $"Add media success!!!", "Ok");
                RefreshFormMedia();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was a problem when add media: {ex.Message}", "Ok");
            }
        }

        private async void DeleteMedia()
        {
            try
            {
                if (SelectedMedias.Count == 0)
                {
                    throw new Exception("Choose media that you want to Delete!");
                }

                List<string> mediaId = new List<string>();

                foreach (DTO_Medias m in SelectedMedias)
                {
                    mediaId.Add(m.Id);
                }

                bool check = await _bus.BusDeleteMedia(mediaId);

                if (!check)
                {
                    throw new Exception("Delete Failed !!");
                }

                await Shell.Current.DisplayAlert("Notification!", $"Delete media success!!!", "Ok");
                RefreshFormMedia();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Error when delete: {ex.Message}", "Ok");
            }
        }

        private async void UpdateButton()
        {
            try
            {
                if (SelectedMedias.Count == 0)
                {
                    throw new Exception("Must choose Media you want to update!!");
                }

                if (SelectedMedias.Count > 1)
                {
                    throw new Exception("Choose only 1 media to Update !!!");
                }

                foreach (DTO_Medias m in SelectedMedias)
                {
                    MediaName = m.MediaName;
                    Country = m.Country;
                    ReleaseDate = m.ReleaseDate;
                    _listCategory = m.ListCategory;
                    _listEpisode = m.ListEpisode;
                    Role = m.RoleMedia;
                    Description = m.Decription;
                    Imagee = m.Image;
                }

                foreach (string category in _listCategory)
                {
                    dsCategorys2.Add(category);
                    dsCategorys.Remove(category);
                }

                foreach (string episode in _listEpisode)
                {
                    dsEpisodes.Add(episode);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was a problem: {ex.Message}", "Ok");
            }
        }

        private async void SaveUpdate()
        {
            try
            {
                DTO_Medias media = SelectedMedias[0];
                media.MediaName = MediaName;
                media.Country = Country;
                media.ReleaseDate = ReleaseDate;
                media.ListCategory = _listCategory;
                media.ListEpisode = _listEpisode;
                media.RoleMedia = Role;
                media.Decription = Description;
                media.Image = Imagee;

                bool check = await _bus.BusUpdateMedia(media);

                if (!check)
                {
                    throw new Exception("Update Media failed!!");
                }

                await Shell.Current.DisplayAlert("Notification!", $"Update media success!!!", "Ok");
                RefreshFormMedia();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"There was a problem: {ex.Message}", "Ok");
            }
        }

        private async void FindMedia()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                var medias = await _bus.BusFindMedia(Search);

                dsMedias.Clear();

                foreach (var meida in medias)
                {
                    dsMedias.Add(meida);
                }

                await Shell.Current.DisplayAlert("Notification!", $"Find media success!!!", "Ok");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Find media failed!: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
