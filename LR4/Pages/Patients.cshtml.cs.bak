using LR4.Application.Adapters;
using LR4.Application.Services;
using LR4.Core.Interfaces;
using LR4.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LR4.Pages
{
    public class PatientsModel : PageModel
    {
        private readonly IDentistryPatientService _patientService;
        private readonly ExcelExportService _excelService;

        public List<DentistryPatient> Patients { get; set; } = new();

        public PatientsModel(
            IDentistryPatientService patientService,
            ExcelExportService excelService)
        {
            _patientService = patientService;
            _excelService = excelService;
        }

        public async Task OnGetAsync()
        {
            Patients = await _patientService.GetAll();
        }

        /// <summary>
        /// Обробник для кнопки «Завантажити Excel».
        /// Використовує патерн Adapter для перетворення даних.
        /// </summary>
        public async Task<IActionResult> OnGetExportExcel()
        {
            var patients = await _patientService.GetAll();

            // Adapter: перетворюємо колекцію Entity → List<List<string>>
            var adapter = new AdapterDentistryPatientList(patients);
            var data = adapter.ToTable();

            var header = new List<string>
            {
                "Прізвище", "Ім'я", "По-батькові", "Дата народження",
                "Стать", "Стан пацієнта", "Код лікаря",
                "Послуга", "Дата та час візиту", "Вартість (грн)"
            };

            var bytes = _excelService.SaveToExcel(
                title: "Список пацієнтів стоматологічної клініки",
                header: header,
                data: data,
                footer: $"Сформовано: {DateTime.Now:dd.MM.yyyy HH:mm}  |  Записів: {patients.Count}"
            );

            return File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"patients_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
            );
        }
    }
}
