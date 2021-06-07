using Model;

namespace Hospital.ViewModels
{
    public class RoomViewModel : ViewModel
    {
        private int id;

        private string name;

        private int floor;

        private string detail;

        public Injector Injector { get; set; }

        public Room _Room { get; set; }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public int Floor
        {
            get { return floor; }
            set
            {
                floor = value;
                OnPropertyChanged();
            }
        }

        public string Detail
        {
            get { return detail; }
            set
            {
                detail = value;
                OnPropertyChanged();
            }
        }

        public RoomViewModel()
        {
            Injector = new Injector();
            _Room = new Room();
        }
    }
}
