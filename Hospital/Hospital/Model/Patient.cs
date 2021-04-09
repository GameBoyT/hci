/***********************************************************************
 * Module:  Patient.cs
 * Author:  Vladimir
 * Purpose: Definition of the Class Patient
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace Model
{
   public class Patient
   {
      public Patient GetPatient(int id)
      {
         throw new NotImplementedException();
      }
      
      public List<Patient> GetAll()
      {
         throw new NotImplementedException();
      }
      
      public Boolean CreatePatient(int id, String firstName, String lastName, String password, String username)
      {
         throw new NotImplementedException();
      }
      
      public Boolean DeletePatient(int id)
      {
         throw new NotImplementedException();
      }
      
      public Boolean UpdatePatient(int id, String firstName, String lastName, String password, String username)
      {
         throw new NotImplementedException();
      }
      
      public System.Collections.ArrayList appointment;
      
      public System.Collections.ArrayList Appointment
      {
         get
         {
            if (appointment == null)
               appointment = new System.Collections.ArrayList();
            return appointment;
         }
         set
         {
            RemoveAllAppointment();
            if (value != null)
            {
               foreach (Appointment oAppointment in value)
                  AddAppointment(oAppointment);
            }
         }
      }
      
      public void AddAppointment(Appointment newAppointment)
      {
         if (newAppointment == null)
            return;
         if (this.appointment == null)
            this.appointment = new System.Collections.ArrayList();
         if (!this.appointment.Contains(newAppointment))
         {
            this.appointment.Add(newAppointment);
            newAppointment.Patient = this;
         }
      }
      
      public void RemoveAppointment(Appointment oldAppointment)
      {
         if (oldAppointment == null)
            return;
         if (this.appointment != null)
            if (this.appointment.Contains(oldAppointment))
            {
               this.appointment.Remove(oldAppointment);
               oldAppointment.Patient = null;
            }
      }
      
      public void RemoveAllAppointment()
      {
         if (appointment != null)
         {
            System.Collections.ArrayList tmpAppointment = new System.Collections.ArrayList();
            foreach (Appointment oldAppointment in appointment)
               tmpAppointment.Add(oldAppointment);
            appointment.Clear();
            foreach (Appointment oldAppointment in tmpAppointment)
               oldAppointment.Patient = null;
            tmpAppointment.Clear();
         }
      }
      public User user;
      
      public User User
      {
         get
         {
            return user;
         }
         set
         {
            this.user = value;
         }
      }
   
   }
}