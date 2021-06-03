using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Model;

namespace Hospital.ViewModels
{
    class ExaminationViewModel : ViewModel
    {
        #region Polja
        private Injector inject;

        private ObservableCollection<Patient> patients;
        
        public DateTime ExaminationDate { get; set; }

        public string StartTime { get; set; }

        public Injector Inject
        {
            get { return inject; }
            set
            {
                inject = value;
            }
        }

        public ObservableCollection<Patient> Patients
        {
            get { return patients; }
            set
            {
                patients = value;
            }
        }

        #endregion

        #region Konstruktori
        public ExaminationViewModel()
        {
            Inject = new Injector();
            Patients = new ObservableCollection<Patient>(Inject.PatientService.GetAll());
            ExaminationDate = DateTime.Now;
            StartTime = "12:00";
    }

    #endregion
}
}
