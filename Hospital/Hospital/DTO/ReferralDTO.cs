using Model;
using System;

namespace DTO
{
    public class ReferralDTO
    {
        public Employee Doctor { get; set; }
        public String Description { get; set; }

        public ReferralDTO(Employee doctor, String description)
        {
            this.Doctor = doctor;
            this.Description = description;
        }

    }
}
