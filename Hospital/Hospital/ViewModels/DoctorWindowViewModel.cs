using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hospital.ViewModels
{
    public class DoctorWindowViewModel
    {
        private Injector inject;

        public ObservableCollection<AppointmentViewModel> Appointments { get; set; }

        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }

        public DoctorWindowViewModel()
        {
            Inject = new Injector();
            Appointments = new ObservableCollection<AppointmentViewModel>(Inject.AppointmentConverter.ConvertCollectionToViewModel(Inject.AppointmentService.GetAllForLoggedInDoctor()));
        }
    }
}
