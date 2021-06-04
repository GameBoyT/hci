using Hospital.ViewModels;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital.VMConverters
{
    public class PatientConverter
    {
        public PatientViewModel ConvertModelToViewModel(Patient patient)
        {
            PatientViewModel patientViewModel = new PatientViewModel
            {
                Jmbg = patient.User.Jmbg,
                FirstName = patient.User.FirstName,
                LastName = patient.User.LastName,
                _Patient = patient
            };

            return patientViewModel;
        }

        public ObservableCollection<PatientViewModel> ConvertCollectionToViewModel(List<Patient> patients)
        {
            ObservableCollection<PatientViewModel> vmPatients = new ObservableCollection<PatientViewModel>();
            PatientViewModel patientViewModel;
            foreach (Patient patient in patients)
            {
                patientViewModel = ConvertModelToViewModel(patient);
                vmPatients.Add(patientViewModel);
            }
            return vmPatients;
        }
    }
}
