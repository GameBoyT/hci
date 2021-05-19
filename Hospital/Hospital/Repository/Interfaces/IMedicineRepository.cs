using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        List<Medicine> GetByVerification(VerificationType verification);

        List<Medicine> GetNotRejected();

        Medicine GetByName(string name);

    }
}
