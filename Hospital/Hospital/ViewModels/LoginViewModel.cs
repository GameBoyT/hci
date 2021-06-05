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
            Injector injector = new Injector();
            Inject = injector;
            injector.AppointmentConverter.EmployeeConverter = injector.EmployeeConverter;
            injector.AppointmentConverter.PatientConverter = injector.PatientConverter;
            injector.AppointmentConverter.EmployeeService = injector.EmployeeService;
            injector.AppointmentConverter.PatientService = injector.PatientService;
            injector.AppointmentConverter.RoomConverter = injector.RoomConverter;
            injector.AppointmentConverter.RoomService = injector.RoomService;
        }
    }
}
