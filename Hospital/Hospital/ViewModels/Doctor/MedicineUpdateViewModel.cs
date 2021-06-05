using Hospital.ViewModels.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Doctor
{
    public class MedicineUpdateViewModel
    {
        public Injector Inject { get; set; }
        public NavigationService NavService { get; }
        public MedicineViewModel Medicine { get; }

        public MedicineUpdateViewModel(NavigationService navService, MedicineViewModel medicine)
        {
            NavService = navService;
            Medicine = medicine;
            Inject = new Injector();
        }


    }
}
