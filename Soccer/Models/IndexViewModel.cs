using Soccer.BLL.DTO;

namespace Soccer.Models
{
    public class IndexViewModel
    {
        public IEnumerable<PlayerDTO> Players { get; set; }
        public PageViewModel PageViewModel { get; }
        public FilterViewModel FilterViewModel { get; }
        public SortViewModel SortViewModel { get; }

        public IndexViewModel(IEnumerable<PlayerDTO> players, PageViewModel pageViewModel,
            FilterViewModel filterViewModel, SortViewModel sortViewModel)
        {
            Players = players;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
        }
    }
}

