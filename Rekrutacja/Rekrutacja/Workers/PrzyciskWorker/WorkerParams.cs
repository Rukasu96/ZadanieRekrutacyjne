using Soneta.Business;
using Soneta.Types;

namespace Rekrutacja.Workers.PrzyciskWorker
{

    public class WorkerParams<T, TValue> : ContextBase
    {
        [Caption("A")]
        public TValue ZmiennaA { get; set; }
        [Caption("B")]
        public TValue ZmiennaB { get; set; }
        [Caption("Operacyjna")]
        public T ZmiennaOperacyjna { get; set; }
        [Caption("Data Obliczeń")]
        public Date DataObliczen { get; set; }

        public WorkerParams(Context context) : base(context)
        {
            DataObliczen = Date.Today;
        }
    }
}
