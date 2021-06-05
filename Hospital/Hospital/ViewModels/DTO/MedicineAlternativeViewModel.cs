using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.ViewModels.DTO
{
    public class MedicineAlternativeViewModel : ViewModel
    {
        private int id;

        private string name;

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

        public MedicineAlternativeViewModel()
        {
            _Medicine = new Medicine();
        }
    }
}
