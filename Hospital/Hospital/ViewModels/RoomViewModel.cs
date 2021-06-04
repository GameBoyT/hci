using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.ViewModels
{
    public class RoomViewModel : ViewModel
    {
        private int id;

        private string name;

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

        public RoomViewModel()
        {
            Injector = new Injector();
            _Room = new Room();
        }
    }
}
