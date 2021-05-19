using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<MedicalAppointment>
    {
        List<MedicalAppointment> GetAppointmentsForDoctor(String jmbg);

        List<MedicalAppointment> GetAppointmentsForPatient(String jmbg);

        List<MedicalAppointment> GetAppointmentsForRoom(int roomId);

    }
}
