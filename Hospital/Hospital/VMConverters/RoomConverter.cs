using Hospital.ViewModels;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital.VMConverters
{
    public class RoomConverter
    {
        public RoomViewModel ConvertModelToViewModel(Room room)
        {
            RoomViewModel roomViewModel = new RoomViewModel
            {
                Id = room.Id,
                Name = room.Name,
                _Room = room
            };

            return roomViewModel;
        }

        public ObservableCollection<RoomViewModel> ConvertCollectionToViewModel(List<Room> rooms)
        {
            ObservableCollection<RoomViewModel> vmRooms = new ObservableCollection<RoomViewModel>();
            RoomViewModel roomViewModel;
            foreach (Room room in rooms)
            {
                roomViewModel = ConvertModelToViewModel(room);
                vmRooms.Add(roomViewModel);
            }
            return vmRooms;
        }
    }
}
