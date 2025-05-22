using Soneta.Business;
using System;
using Soneta.Kadry;
using Rekrutacja.Workers.PrzyciskWorker;
using Rekrutacja.Workers.ProstyKalkulator;
using Rekrutacja.Workers.Utilities;

[assembly: Worker(typeof(ProstyKalkulatorWorker), typeof(Pracownicy))]
namespace Rekrutacja.Workers.ProstyKalkulator
{
    public class ProstyKalkulatorWorker
    {
        [Context]
        public Context Cx { get; set; }
        [Context]
        public WorkerParams<string, double> Parametry { get; set; }
        [Action("Kalkulator",
           Description = "Prosty kalkulator ",
           Priority = 10,
           Mode = ActionMode.ReadOnlySession,
           Icon = ActionIcon.Accept,
           Target = ActionTarget.ToolbarWithText)]
        public void WykonajAkcje()
        {
            DebuggerSession.MarkLineAsBreakPoint();
            var zaznaczeniPracownicy = Utils.ZwrocZaznaczonychPracownikow(Cx);

            using (Session nowaSesja = this.Cx.Login.CreateSession(false, false, "ModyfikacjaPracownika"))
            {
                using (ITransaction trans = nowaSesja.Logout(true))
                {
                    foreach (var zaznaczonyPracownik in zaznaczeniPracownicy)
                    {
                        var pracownikZSesja = nowaSesja.Get(zaznaczonyPracownik);
                        pracownikZSesja.Features["DataObliczen"] = Parametry.DataObliczen;
                        pracownikZSesja.Features["Wynik"] = Utils.Oblicz(Parametry.ZmiennaA, Parametry.ZmiennaB, Parametry.ZmiennaOperacyjna);
                    }
                    trans.CommitUI();
                }
                nowaSesja.Save();
            }
        }
    }
}
