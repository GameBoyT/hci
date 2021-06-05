using Model;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        List<Medicine> GetByVerification(VerificationType verification);

        List<Medicine> GetNotRejected();

        Medicine GetByName(string name);

        List<Medicine> GetAvaliableAlternatives(int id);
    }
}
