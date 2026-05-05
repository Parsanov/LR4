using LR4.Core.Model;
using LR4.Persistence.Data;

namespace LR4.Persistence
{
    public static class DentistryPatientSeeder
    {
        public static async Task SeedAsync(DbDataContext context)
        {
            if (context.DentistryPatients.Any()) return;

            var patients = new List<DentistryPatient>
            {
                new() { Surname="Ковальчук", Name="Олег",    Pname="Васильович",  Dr=new DateOnly(1985,3,12),  Gender=true,  StateComments="Алергія на лідокаїн",          DoctorCode="D001", Posluga="Лікування карієсу",          TimeVisit=new DateTime(2024,11,5,9,0,0),   Cost=850.00  },
                new() { Surname="Петренко",  Name="Ірина",   Pname="Миколаївна",  Dr=new DateOnly(1990,7,23),  Gender=false, StateComments=null,                           DoctorCode="D002", Posluga="Видалення зубного каменю",    TimeVisit=new DateTime(2024,11,5,10,0,0),  Cost=600.00  },
                new() { Surname="Бондаренко",Name="Сергій",  Pname="Олексійович", Dr=new DateOnly(1978,1,15),  Gender=true,  StateComments="Гіпертонія, обережно з анестезією", DoctorCode="D001", Posluga="Протезування коронкою",   TimeVisit=new DateTime(2024,11,6,11,0,0),  Cost=4500.00 },
                new() { Surname="Кравченко", Name="Наталія", Pname="Павлівна",    Dr=new DateOnly(1995,5,8),   Gender=false, StateComments=null,                           DoctorCode="D003", Posluga="Відбілювання зубів",          TimeVisit=new DateTime(2024,11,6,12,0,0),  Cost=1200.00 },
                new() { Surname="Мельник",   Name="Андрій",  Pname="Іванович",    Dr=new DateOnly(1982,9,30),  Gender=true,  StateComments="Цукровий діабет 2 типу",       DoctorCode="D002", Posluga="Видалення зуба",              TimeVisit=new DateTime(2024,11,7,9,30,0),  Cost=700.00  },
                new() { Surname="Ткаченко",  Name="Оксана",  Pname="Сергіївна",   Dr=new DateOnly(1988,12,3),  Gender=false, StateComments=null,                           DoctorCode="D003", Posluga="Лікування пульпіту",          TimeVisit=new DateTime(2024,11,7,10,30,0), Cost=1500.00 },
                new() { Surname="Шевченко",  Name="Дмитро",  Pname="Анатолійович",Dr=new DateOnly(1975,4,20),  Gender=true,  StateComments="Бруксизм",                     DoctorCode="D001", Posluga="Виготовлення капи",           TimeVisit=new DateTime(2024,11,8,9,0,0),   Cost=2200.00 },
                new() { Surname="Гриценко",  Name="Людмила", Pname="Вікторівна",  Dr=new DateOnly(1993,8,17),  Gender=false, StateComments=null,                           DoctorCode="D002", Posluga="Лікування карієсу",           TimeVisit=new DateTime(2024,11,8,11,0,0),  Cost=900.00  },
                new() { Surname="Савченко",  Name="Максим",  Pname="Олегович",    Dr=new DateOnly(2000,2,28),  Gender=true,  StateComments=null,                           DoctorCode="D003", Posluga="Рентген зуба",                TimeVisit=new DateTime(2024,11,11,9,0,0),  Cost=150.00  },
                new() { Surname="Лисенко",   Name="Катерина",Pname="Михайлівна",  Dr=new DateOnly(1987,6,10),  Gender=false, StateComments="Вагітність 2-й триместр",      DoctorCode="D001", Posluga="Консультація",                TimeVisit=new DateTime(2024,11,11,10,0,0), Cost=200.00  },
                new() { Surname="Романенко", Name="Ігор",    Pname="Петрович",    Dr=new DateOnly(1970,11,25), Gender=true,  StateComments="Повна адентія верхньої щелепи",DoctorCode="D002", Posluga="Протезування знімним протезом",TimeVisit=new DateTime(2024,11,12,9,0,0),  Cost=8500.00 },
                new() { Surname="Захаренко", Name="Валентина",Pname="Юріївна",    Dr=new DateOnly(1965,3,5),   Gender=false, StateComments="Остеопороз",                   DoctorCode="D003", Posluga="Імплантація зуба",            TimeVisit=new DateTime(2024,11,12,11,30,0),Cost=12000.00},
                new() { Surname="Марченко",  Name="Руслан",  Pname="Дмитрович",   Dr=new DateOnly(1998,7,19),  Gender=true,  StateComments=null,                           DoctorCode="D001", Posluga="Лікування карієсу",           TimeVisit=new DateTime(2024,11,13,9,0,0),  Cost=750.00  },
                new() { Surname="Поліщук",   Name="Тетяна",  Pname="Олексіївна",  Dr=new DateOnly(1991,10,14), Gender=false, StateComments=null,                           DoctorCode="D002", Posluga="Відбілювання зубів",          TimeVisit=new DateTime(2024,11,13,10,30,0),Cost=1200.00 },
                new() { Surname="Кузьменко", Name="Олексій", Pname="Борисович",   Dr=new DateOnly(1983,1,7),   Gender=true,  StateComments="Схильність до альвеолітів",    DoctorCode="D003", Posluga="Видалення зуба мудрості",     TimeVisit=new DateTime(2024,11,14,9,0,0),  Cost=950.00  },
                new() { Surname="Даниленко", Name="Юлія",    Pname="Анатоліївна", Dr=new DateOnly(1996,4,22),  Gender=false, StateComments=null,                           DoctorCode="D001", Posluga="Лікування пародонтиту",       TimeVisit=new DateTime(2024,11,14,11,0,0), Cost=1800.00 },
                new() { Surname="Білоус",    Name="Артем",   Pname="Іванович",    Dr=new DateOnly(2003,9,1),   Gender=true,  StateComments=null,                           DoctorCode="D002", Posluga="Лікування карієсу",           TimeVisit=new DateTime(2024,11,15,9,30,0), Cost=800.00  },
                new() { Surname="Горбаль",   Name="Ганна",   Pname="Степанівна",  Dr=new DateOnly(1979,12,31), Gender=false, StateComments="Серцева недостатність",        DoctorCode="D003", Posluga="Консультація та огляд",       TimeVisit=new DateTime(2024,11,15,11,0,0), Cost=200.00  },
            };

            await context.DentistryPatients.AddRangeAsync(patients);
            await context.SaveChangesAsync();
        }
    }
}
