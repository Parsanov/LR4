using LR4.Core.Model;

namespace LR4.Core.Interfaces
{
    public interface IDentistryPatientService
    {
        Task<List<DentistryPatient>> GetAll();
        Task Add(DentistryPatient patient);
        Task AddRange(List<DentistryPatient> patients);
        Task Delete(int id);
    }
}
