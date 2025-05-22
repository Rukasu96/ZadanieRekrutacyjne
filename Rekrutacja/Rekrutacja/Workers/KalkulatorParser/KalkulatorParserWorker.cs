using Rekrutacja.Workers.KalkulatorParser;
using Rekrutacja.Workers.WorkerParams;
using Rekrutacja.Workers.Utilities;
using Soneta.Business;
using Soneta.Kadry;
using System;

[assembly: Worker(typeof(KalkulatorParserWorker), typeof(Pracownicy))]
namespace Rekrutacja.Workers.KalkulatorParser
{
    public class KalkulatorParserWorker
    {
        [Context]
        public Context Cx { get; set; }

        [Context]
        public WorkerParams<string, string> Parametry { get; set; }

        [Action("Kalkulator Parser",
           Description = "Prosty kalkulator z wykorzystaniem parsera",
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
                        pracownikZSesja.Features["Wynik"] = Utils.Oblicz(StringParser.ParseToInt(Parametry.ZmiennaA), StringParser.ParseToInt(Parametry.ZmiennaB), Parametry.ZmiennaOperacyjna);
                    }
                    trans.CommitUI();
                }
                nowaSesja.Save();
            }
        }
    }
}
