using Rekrutacja.Workers.KalkulatorPola;
using Rekrutacja.Workers.WorkerParams;
using Rekrutacja.Workers.Utilities;
using Soneta.Business;
using Soneta.Kadry;
using System;

[assembly: Worker(typeof(KalkulatorPolaWorker), typeof(Pracownicy))]
namespace Rekrutacja.Workers.KalkulatorPola
{
    public class KalkulatorPolaWorker
    {
        [Context]
        public Context Cx { get; set; }

        [Context]
        public WorkerParams<Figura, double> Parametry { get; set; }

        [Action("Kalkulator Pól",
           Description = "Kalkulator do liczenia pola",
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
                        pracownikZSesja.Features["Wynik"] = (double)Utils.ObliczPole(Parametry.ZmiennaA, Parametry.ZmiennaB, Parametry.ZmiennaOperacyjna);
                    }
                    trans.CommitUI();
                }
                nowaSesja.Save();
            }
        }
    }
}
