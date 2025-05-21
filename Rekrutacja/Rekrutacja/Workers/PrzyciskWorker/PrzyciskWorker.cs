using Soneta.Business;
using Soneta.Types;

namespace Rekrutacja.Workers.PrzyciskWorker
{

    public class PrzyciskWorker<T,TValue> : ContextBase
    {
        [Caption("A")]
        public TValue ZmiennaA { get; set; }
        [Caption("B")]
        public TValue ZmiennaB { get; set; }
        [Caption("Operacyjna")]
        public T ZmiennaOperacyjna { get; set; }
        [Caption("Data Obliczeń")]
        public Date DataObliczen { get; set; }

        public PrzyciskWorker(Context context) : base(context) 
        {
            DataObliczen = Date.Today;
        }
    }
}
