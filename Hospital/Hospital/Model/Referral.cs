using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Model
{
    public class Referral
    {
        string Description { get; set; }
        Employee Doctor { get; set; }

        public Referral(string description, Employee doctor)
        {
            this.Description = description;
            this.Doctor = doctor;
        }


    }
}
