using Hospital.ViewModels;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital.View.Doctor
{
    public partial class PastAppointmentsReportWindow : Window
    {
        public Injector Inject { get; set; }
        public PatientViewModel Patient { get; set; }
        public ObservableCollection<Anamnesis> PastAppointments { get; set; }

        public DateTime BeginningDate { get; set; }
        public DateTime EndDate { get; set; }
        public PastAppointmentsReportWindow(ObservableCollection<Anamnesis> pastAppointments, DateTime beginningDate, DateTime endDate)
        {
            InitializeComponent();
            DataContext = this;
            Inject = new Injector();
            Patient = Inject.PatientConverter.ConvertModelToViewModel(Inject.PatientService.GetByJmbg("5"));
            PastAppointments = pastAppointments;
            BeginningDate = beginningDate.Date;
            EndDate = endDate.Date;
        }
    }
}
