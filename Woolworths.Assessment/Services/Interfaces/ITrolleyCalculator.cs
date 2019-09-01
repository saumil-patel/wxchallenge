using Woolworths.Assessment.Models;

namespace Woolworths.Assessment.Services.Interfaces
{
    public interface ITrolleyCalculator
    {
        double CalculateTrolleyTotal(TrolleyTotalRequest trolleyTotalRequest);
    }
}