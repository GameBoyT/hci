using System;
using System.Collections.Generic;

namespace Model
{
   public class AppointmentStorage
   {
      private readonly String fileLocation;
      private List<Appointment> appointments;

        public List<Appointment> GetAll()
      {
         throw new NotImplementedException();
      }
      
      public Appointment GetById(int id)
      {
         throw new NotImplementedException();
      }
      
      public void Save(Appointment appointment)
      {
         throw new NotImplementedException();
      }
      
      public void Delete(int id)
      {
         throw new NotImplementedException();
      }
      
      public void Update(Appointment appointment)
      {
         throw new NotImplementedException();
      }
      
      public List<Appointment> GetAppointmentsForDoctor(String jmbg)
      {
         throw new NotImplementedException();
      }
      
      public List<Appointment> GetAppointmentsForPatient(String jmbg)
      {
         throw new NotImplementedException();
      }
   
   }
}