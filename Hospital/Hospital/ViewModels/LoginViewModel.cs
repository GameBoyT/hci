using Hospital.Commands;
using Hospital.View.Doctor;
using Hospital.ViewModels.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Hospital.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        public UserViewModel User { get; set; }

        public RelayCommand LoginCommand { get; set; }

        public void Executed_LoginCommand(object obj)
        {
            User.Validate();
            if (User.IsValid)
            {
                DoctorMainWindow doctorMainWindow = new DoctorMainWindow();
                doctorMainWindow.Show();
                foreach (Window item in Application.Current.Windows)
                {
                    if (item.DataContext == this) item.Close();
                }
            }
        }


        public LoginViewModel()
        {
            User = new UserViewModel();
            LoginCommand = new RelayCommand(Executed_LoginCommand);
        }

    }
}
