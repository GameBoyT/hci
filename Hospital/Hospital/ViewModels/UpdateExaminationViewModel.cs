using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;

namespace Hospital.ViewModels
{
    public class UpdateExaminationViewModel
    {
        Injector Injector { get; set; }
        public AppointmentViewModel Appointment { get; set; }

        public Patient Patient { get; set; }
        public UpdateExaminationViewModel(AppointmentViewModel appointment)
        {
            Injector = new Injector();
            Appointment = appointment;
        }

    }
}
