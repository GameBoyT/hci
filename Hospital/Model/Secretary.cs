/***********************************************************************
 * Module:  Secretary.cs
 * Author:  Vladimir
 * Purpose: Definition of the Class Secretary
 ***********************************************************************/

using System;

namespace Model
{
   public class Secretary
   {
      public Secretary GetSecretary()
      {
         throw new NotImplementedException();
      }
      
      public System.Collections.ArrayList user;
      
      public System.Collections.ArrayList User
      {
         get
         {
            if (user == null)
               user = new System.Collections.ArrayList();
            return user;
         }
         set
         {
            RemoveAllUser();
            if (value != null)
            {
               foreach (User oUser in value)
                  AddUser(oUser);
            }
         }
      }
      
      public void AddUser(User newUser)
      {
         if (newUser == null)
            return;
         if (this.user == null)
            this.user = new System.Collections.ArrayList();
         if (!this.user.Contains(newUser))
            this.user.Add(newUser);
      }
      
      public void RemoveUser(User oldUser)
      {
         if (oldUser == null)
            return;
         if (this.user != null)
            if (this.user.Contains(oldUser))
               this.user.Remove(oldUser);
      }
      
      public void RemoveAllUser()
      {
         if (user != null)
            user.Clear();
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
            this.appointment.Add(newAppointment);
      }
      
      public void RemoveAppointment(Appointment oldAppointment)
      {
         if (oldAppointment == null)
            return;
         if (this.appointment != null)
            if (this.appointment.Contains(oldAppointment))
               this.appointment.Remove(oldAppointment);
      }
      
      public void RemoveAllAppointment()
      {
         if (appointment != null)
            appointment.Clear();
      }
   
   }
}