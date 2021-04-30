using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Model
{
    public class Referral
    {
        public Referral(string doctorJmbg, string description)
        {
            this.DoctorJmbg = doctorJmbg;
            this.Description = description;
        }

        public string DoctorJmbg { get; set; }
        
        public string Description { get; set; }
    }
}
