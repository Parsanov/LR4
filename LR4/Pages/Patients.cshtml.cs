using LR4.Application.Adapters;
using LR4.Application.Services;
using LR4.Application.Session;
using LR4.Core.Interfaces;
using LR4.Core.Memento;
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

        public TableSettings Settings { get; set; } = new();
        public bool CanUndo { get; set; }
        public bool CanRedo { get; set; }

        [BindProperty] public string FontFamily { get; set; } = "Arial";
        [BindProperty] public int    FontSize   { get; set; } = 14;
        [BindProperty] public string FontColor  { get; set; } = "#212529";
        [BindProperty] public bool   IsBold     { get; set; }
        [BindProperty] public bool   IsItalic   { get; set; }

        public PatientsModel(
            IDentistryPatientService patientService,
            ExcelExportService excelService)
        {
            _patientService = patientService;
            _excelService   = excelService;
        }

        public async Task OnGetAsync()
        {
            Patients = await _patientService.GetAll();
            LoadState();
        }

        public async Task<IActionResult> OnPostSetAsync()
        {
            Patients = await _patientService.GetAll();

            var settings = TableSettingsSession.LoadSettings(HttpContext.Session);
            var history  = TableSettingsSession.LoadHistory(HttpContext.Session);

            history.Push(settings.Save());

            settings.FontFamily = FontFamily;
            settings.FontSize   = FontSize;
            settings.FontColor  = FontColor;
            settings.IsBold     = IsBold;
            settings.IsItalic   = IsItalic;

            TableSettingsSession.SaveSettings(HttpContext.Session, settings);
            TableSettingsSession.SaveHistory(HttpContext.Session, history);

            Settings = settings;
            CanUndo  = history.CanUndo;
            CanRedo  = history.CanRedo;

            return Page();
        }

        public async Task<IActionResult> OnPostUndoAsync()
        {
            Patients = await _patientService.GetAll();

            var settings = TableSettingsSession.LoadSettings(HttpContext.Session);
            var history  = TableSettingsSession.LoadHistory(HttpContext.Session);

            if (history.CanUndo)
            {
                var previous = history.Undo(settings.Save());
                settings.Restore(previous);

                TableSettingsSession.SaveSettings(HttpContext.Session, settings);
                TableSettingsSession.SaveHistory(HttpContext.Session, history);
            }

            Settings = settings;
            CanUndo  = history.CanUndo;
            CanRedo  = history.CanRedo;

            return Page();
        }

        public async Task<IActionResult> OnPostRedoAsync()
        {
            Patients = await _patientService.GetAll();

            var settings = TableSettingsSession.LoadSettings(HttpContext.Session);
            var history  = TableSettingsSession.LoadHistory(HttpContext.Session);

            if (history.CanRedo)
            {
                var next = history.Redo(settings.Save());
                settings.Restore(next);

                TableSettingsSession.SaveSettings(HttpContext.Session, settings);
                TableSettingsSession.SaveHistory(HttpContext.Session, history);
            }

            Settings = settings;
            CanUndo  = history.CanUndo;
            CanRedo  = history.CanRedo;

            return Page();
        }

        public async Task<IActionResult> OnPostResetAsync()
        {
            Patients = await _patientService.GetAll();

            var settings = new TableSettings();
            var history  = TableSettingsSession.LoadHistory(HttpContext.Session);
            history.Reset();

            TableSettingsSession.SaveSettings(HttpContext.Session, settings);
            TableSettingsSession.SaveHistory(HttpContext.Session, history);

            Settings = settings;
            CanUndo  = false;
            CanRedo  = false;

            return Page();
        }

        public async Task<IActionResult> OnGetExportExcel()
        {
            var patients = await _patientService.GetAll();
            var adapter  = new AdapterDentistryPatientList(patients);
            var data     = adapter.ToTable();

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

            return File(bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"patients_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
        }

        private void LoadState()
        {
            Settings = TableSettingsSession.LoadSettings(HttpContext.Session);
            var history = TableSettingsSession.LoadHistory(HttpContext.Session);
            CanUndo = history.CanUndo;
            CanRedo = history.CanRedo;

            FontFamily = Settings.FontFamily;
            FontSize   = Settings.FontSize;
            FontColor  = Settings.FontColor;
            IsBold     = Settings.IsBold;
            IsItalic   = Settings.IsItalic;
        }
    }
}
