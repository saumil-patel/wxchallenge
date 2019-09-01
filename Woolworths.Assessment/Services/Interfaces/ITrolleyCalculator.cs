using Woolworths.Assessment.Models;

namespace Woolworths.Assessment.Services.Interfaces
{
    public interface ITrolleyCalculator
    {
        decimal CalculateTrolleyTotal(TrolleyTotalRequest trolleyTotalRequest);
    }
}