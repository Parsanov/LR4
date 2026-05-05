using LR4.Core.Interfaces;
using LR4.Core.Model;
using LR4.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace LR4.Persistence
{
    public class DentistryPatientService : IDentistryPatientService
    {
        private readonly DbDataContext _context;

        public DentistryPatientService(DbDataContext context)
        {
            _context = context;
        }

        public async Task<List<DentistryPatient>> GetAll()
        {
            return await _context.DentistryPatients.ToListAsync();
        }

        public async Task Add(DentistryPatient patient)
        {
            await _context.DentistryPatients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(List<DentistryPatient> patients)
        {
            await _context.DentistryPatients.AddRangeAsync(patients);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var patient = await _context.DentistryPatients.FindAsync(id);
            if (patient != null)
            {
                _context.DentistryPatients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
