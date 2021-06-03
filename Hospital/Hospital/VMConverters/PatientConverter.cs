using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Hospital.ViewModels;
using Model;

namespace Hospital.VMConverters
{
    public class PatientConverter
    {
        public PatientViewModel ConvertModelToViewModel(Patient patient)
        {
            PatientViewModel patientViewModel = new PatientViewModel();

            patientViewModel.Jmbg = patient.User.Jmbg;
            patientViewModel.FirstName = patient.User.FirstName;
            patientViewModel.LastName = patient.User.LastName;
            patientViewModel._Patient = patient;

            return patientViewModel;
        }

        public ObservableCollection<PatientViewModel> ConvertCollectionToViewModel(ObservableCollection<Patient> students)
        {
            ObservableCollection<PatientViewModel> vmPatients = new ObservableCollection<PatientViewModel>();
            PatientViewModel patientViewModel;
            foreach (Patient patient in students)
            {
                patientViewModel = ConvertModelToViewModel(patient);
                vmPatients.Add(patientViewModel);
            }
            return vmPatients;
        }
    }
}
