using System.Windows;
using Service;
using System;
using Model;
using System.Collections.Generic;

namespace Hospital
{
    public partial class MainWindow : Window
    {
        private readonly RenovationService _renovationService = new RenovationService();
        private readonly RoomService _roomService = new RoomService();
        private readonly MovingStaticService _movingStaticService = new MovingStaticService();
        public MainWindow()
        {
            InitializeComponent();
            RenovationTime();
            //premestanje opreme
            MoviStaticEquipment();
                
        }

        private void RenovationTime()
        {
            List<RenovationAppointment> renovations = _renovationService.GetAll();
            foreach (RenovationAppointment renovation in renovations)
            {
                if (renovation.StartTime.Ticks <= DateTime.Now.Ticks)
                {
                    if (renovation.Type == 0)
                    {
                        _roomService.AttachRooms(renovation.RoomId, renovation.RoomBId);
                        _renovationService.Delete(renovation.Id);
                    }
                    else
                    {
                        _roomService.DettachRooms(renovation.RoomId);
                        _renovationService.Delete(renovation.Id);
                    }
                }

            }
        }

        private void MoviStaticEquipment()
        {
            List<MovingStatic> listOfStatic = _movingStaticService.GetAll();
            foreach (MovingStatic staticToMove in listOfStatic )
            {
                if (staticToMove.DateTime.Ticks <= DateTime.Now.Ticks)
                {
                    _roomService.MoveStaticEquipment(staticToMove.StaticId, staticToMove.RoomId);
                    _movingStaticService.Delete(staticToMove.Id);
                }

            }

        }
        private void Button_Click_Patient(object sender, RoutedEventArgs e)
        {
            var new_window = new PatientWindow();
            new_window.Show();
            //this.Close();
        }

        private void Button_Click_Doctor(object sender, RoutedEventArgs e)
        {
            View.Doctor.DoctorWindow doctorWindow = new View.Doctor.DoctorWindow();
            doctorWindow.Show();
            //this.Close();
        }

        private void Button_Click_Director(object sender, RoutedEventArgs e)
        {
            View.Director.DirectorWindow directorWindow = new View.Director.DirectorWindow();
            directorWindow.Show();
            //this.Close();
        }

        private void Button_Click_Secretary(object sender, RoutedEventArgs e)
        {
            SecretaryFun secretaryFun = new SecretaryFun();
            secretaryFun.Show();
            //this.Close();
        }
    }
}
