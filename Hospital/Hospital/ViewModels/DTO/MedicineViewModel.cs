using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hospital.ViewModels.DTO
{
    public class MedicineViewModel : ViewModel
    {
        private int id;

        private string name;

        private VerificationType verification;

        private string description;

        private string doctorComment;

        public ObservableCollection<string> Ingredients { get; set; }

        public ObservableCollection<MedicineAlternativeViewModel> Alternatives { get; set; }

        public Medicine _Medicine { get; set; }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public VerificationType Verification
        {
            get { return verification; }
            set
            {
                verification = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public string DoctorComment
        {
            get { return doctorComment; }
            set
            {
                doctorComment = value;
                OnPropertyChanged();
            }
        }
        

        public MedicineViewModel()
        {
            //Ingredients = new ObservableCollection<string>();
            //Alternatives = new ObservableCollection<MedicineAlternativeViewModel>();
            _Medicine = new Medicine();
        }
    }
}
