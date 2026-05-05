using LR4.Core.Model;

namespace LR4.Application.Adapters
{
    /// <summary>
    /// Адаптер для колекції DentistryPatient.
    /// Перетворює List&lt;DentistryPatient&gt; у List&lt;List&lt;string&gt;&gt;
    /// для передачі в ExcelExportService.
    /// </summary>
    public class AdapterDentistryPatientList
    {
        private readonly List<DentistryPatient> _source;

        public AdapterDentistryPatientList(List<DentistryPatient> source)
        {
            _source = source;
        }

        public List<List<string>> ToTable()
        {
            return _source
                .Select(p => new AdapterDentistryPatient(p).ToRow())
                .ToList();
        }
    }
}
