using Hospital.Commands;
using Hospital.ViewModels.DTO;
using System;
using System.Collections.Generic;
using System.Text;

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

            }
        }


        public LoginViewModel()
        {
            User = new UserViewModel();
            LoginCommand = new RelayCommand(Executed_LoginCommand);
        }

    }
}
