using Model;
using System;

namespace Repository.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Patient GetByJmbg(String jmbg);

        Patient Delete(String jmbg);

        new Patient Update(Patient patient);

        int GenerateNewAnamnesisId();
    }
}
