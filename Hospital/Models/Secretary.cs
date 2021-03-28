/***********************************************************************
 * Module:  Secretary.cs
 * Author:  Vladimir
 * Purpose: Definition of the Class Secretary
 ***********************************************************************/

public class Secretary
{
    public Secretary GetSecretary()
    {
        // TODO: implement
        return null;
    }

    public System.Collections.ArrayList appointment;

    /// <pdGenerated>default getter</pdGenerated>
    public System.Collections.ArrayList GetAppointment()
    {
        if (appointment == null)
            appointment = new System.Collections.ArrayList();
        return appointment;
    }

    /// <pdGenerated>default setter</pdGenerated>
    public void SetAppointment(System.Collections.ArrayList newAppointment)
    {
        RemoveAllAppointment();
        foreach (Appointment oAppointment in newAppointment)
            AddAppointment(oAppointment);
    }

    /// <pdGenerated>default Add</pdGenerated>
    public void AddAppointment(Appointment newAppointment)
    {
        if (newAppointment == null)
            return;
        if (this.appointment == null)
            this.appointment = new System.Collections.ArrayList();
        if (!this.appointment.Contains(newAppointment))
            this.appointment.Add(newAppointment);
    }

    /// <pdGenerated>default Remove</pdGenerated>
    public void RemoveAppointment(Appointment oldAppointment)
    {
        if (oldAppointment == null)
            return;
        if (this.appointment != null)
            if (this.appointment.Contains(oldAppointment))
                this.appointment.Remove(oldAppointment);
    }

    /// <pdGenerated>default removeAll</pdGenerated>
    public void RemoveAllAppointment()
    {
        if (appointment != null)
            appointment.Clear();
    }
    public System.Collections.ArrayList user;

    /// <pdGenerated>default getter</pdGenerated>
    public System.Collections.ArrayList GetUser()
    {
        if (user == null)
            user = new System.Collections.ArrayList();
        return user;
    }

    /// <pdGenerated>default setter</pdGenerated>
    public void SetUser(System.Collections.ArrayList newUser)
    {
        RemoveAllUser();
        foreach (User oUser in newUser)
            AddUser(oUser);
    }

    /// <pdGenerated>default Add</pdGenerated>
    public void AddUser(User newUser)
    {
        if (newUser == null)
            return;
        if (this.user == null)
            this.user = new System.Collections.ArrayList();
        if (!this.user.Contains(newUser))
            this.user.Add(newUser);
    }

    /// <pdGenerated>default Remove</pdGenerated>
    public void RemoveUser(User oldUser)
    {
        if (oldUser == null)
            return;
        if (this.user != null)
            if (this.user.Contains(oldUser))
                this.user.Remove(oldUser);
    }

    /// <pdGenerated>default removeAll</pdGenerated>
    public void RemoveAllUser()
    {
        if (user != null)
            user.Clear();
    }

}