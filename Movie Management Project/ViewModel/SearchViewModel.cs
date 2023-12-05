using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Management_Project.ViewModel
{
    public class SearchViewModel: BaseViewModel
    {
        public ObservableCollection<DTO_Medias> dsMediaSeach { get; } = new(); 

        public SearchViewModel()
        {

        }

        public SearchViewModel(List<DTO_Medias> medias)
        {

        }

        private void SearchedMedia(List<DTO_Medias> medias)
        {
            foreach (DTO_Medias m in medias)
            {
                dsMediaSeach.Add(m);
            }
        }
    }
}
