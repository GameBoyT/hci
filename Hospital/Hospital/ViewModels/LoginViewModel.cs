using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.ViewModels
{
    public class LoginViewModel
    {
        public Injector Inject { get; set; }


        public LoginViewModel()
        {
            Inject = new Injector();
        }
    }
}
