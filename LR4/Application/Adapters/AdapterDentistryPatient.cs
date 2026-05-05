using LR4.Core.Model;

namespace LR4.Application.Adapters
{
    /// <summary>
    /// Адаптер для класу DentistryPatient.
    /// Перетворює всі поля сутності до типу string
    /// для подальшого запису в комірки Excel.
    /// </summary>
    public class AdapterDentistryPatient
    {
        public string Surname { get; }
        public string Name { get; }
        public string Pname { get; }
        public string Dr { get; }
        public string Gender { get; }
        public string StateComments { get; }
        public string DoctorCode { get; }
        public string Posluga { get; }
        public string TimeVisit { get; }
        public string Cost { get; }

        public AdapterDentistryPatient(DentistryPatient patient)
        {
            Surname       = patient.Surname;
            Name          = patient.Name;
            Pname         = patient.Pname;
            Dr            = patient.Dr.ToString("dd.MM.yyyy");
            Gender        = patient.Gender ? "Чоловіча" : "Жіноча";
            StateComments = patient.StateComments ?? "—";
            DoctorCode    = patient.DoctorCode;
            Posluga       = patient.Posluga;
            TimeVisit     = patient.TimeVisit.ToString("dd.MM.yyyy HH:mm");
            Cost          = patient.Cost.ToString("F2");
        }

        public List<string> ToRow() =>
        [
            Surname, Name, Pname, Dr, Gender,
            StateComments, DoctorCode, Posluga, TimeVisit, Cost
        ];
    }
}
