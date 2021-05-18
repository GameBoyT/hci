using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        List<Appointment> GetAppointmentsForDoctor(String jmbg);

        List<Appointment> GetAppointmentsForPatient(String jmbg);

        List<Appointment> GetAppointmentsForRoom(int roomId);

    }
}
