using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    interface IMedicineRepository : IGenericRepository<Medicine>
    {
        List<Medicine> GetVerified();

        List<Medicine> GetRejected();

        List<Medicine> GetNotRejected();

        List<Medicine> GetNotVerified();

        Medicine GetByName(string name);

    }
}
