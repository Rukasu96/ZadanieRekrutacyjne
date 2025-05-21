using Rekrutacja.Workers.KalkulatorParser;
using Rekrutacja.Workers.PrzyciskWorker;
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
        public PrzyciskWorker<string, string> Parametry { get; set; }
        [Action("Kalkulator Parser",
           Description = "Prosty kalkulator z wykorzystaniem parsera",
           Priority = 10,
           Mode = ActionMode.ReadOnlySession,
           Icon = ActionIcon.Accept,
           Target = ActionTarget.ToolbarWithText)]
        public void WykonajAkcje()
        {
            DebuggerSession.MarkLineAsBreakPoint();
            Pracownik[] pracownik = null;
            if (this.Cx.Contains(typeof(Pracownik[])))
            {
                pracownik = (Pracownik[])this.Cx[typeof(Pracownik[])];
            }

            using (Session nowaSesja = this.Cx.Login.CreateSession(false, false, "ModyfikacjaPracownika"))
            {
                using (ITransaction trans = nowaSesja.Logout(true))
                {
                    foreach (var p in pracownik)
                    {
                        var pracownikZSesja = nowaSesja.Get(p);
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
