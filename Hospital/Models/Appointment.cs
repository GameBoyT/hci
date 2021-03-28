/***********************************************************************
 * Module:  Appointment.cs
 * Author:  Vladimir
 * Purpose: Definition of the Class Appointment
 ***********************************************************************/

using System;

public class Appointment
{
    public Boolean SetAppointment(int id, DateTime time, Double duration)
    {
        // TODO: implement
        return null;
    }

    public Appointment GetAppointment(int id)
    {
        // TODO: implement
        return null;
    }

    public List<Appointment> GetAllAppointments()
    {
        // TODO: implement
        return null;
    }

    public Room room;
    public Patient patient;
    public Doctor doctor;

    private int Id;
    private DateTime TimeStart;
    private Double Duration;

}