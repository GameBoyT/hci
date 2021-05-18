using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    interface IPatientRepository : IGenericRepository<Patient>
    {
        Patient GetByJmbg(String jmbg);

        Patient Delete(String jmbg);

        new Patient Update(Patient patient);

        int GenerateNewAnamnesisId();
    }
}
