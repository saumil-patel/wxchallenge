using System.Collections.Generic;
using System.Linq;
using Woolworths.Assessment.Models;

namespace Woolworths.Assessment.Services.Interfaces
{
    public class TrolleyCalculator : ITrolleyCalculator
    {
        private class SpecialWithActuals
        {
            public Special Special { get; set; }
            public decimal DiscountedRate { get; set; }

            public decimal ActualTotal { get; set; }

        }


        public decimal CalculateTrolleyTotal(TrolleyTotalRequest trolleyTotalRequest)
        {
            //TODO: this algorithm (sorting offers by best value for money and using in that order to make use of max quantity) isn't working for all scenario
            //look at knapsack or salesman problems
            var priceLookup = new Dictionary<string, ProductPrice>();
            foreach (var product in trolleyTotalRequest.Products)
            {
                priceLookup.Add(product.Name, product);
            }

            var sortedSpecialActual = GetSpecialsSortedByBestValueForMoney(trolleyTotalRequest, priceLookup);

            var quantitiesLookup = GetOrderedQuantitiesInLookupFormat(trolleyTotalRequest);

            decimal minimumToPay = 0;

            foreach (var specialOffer in sortedSpecialActual)
            {
                
                while (SpecialOfferCanBeUsed(specialOffer, quantitiesLookup))
                {
                    minimumToPay += specialOffer.Special.Total;
                    foreach (var specialQuantity in specialOffer.Special.Quantities)
                    {
                        quantitiesLookup[specialQuantity.Name] -= specialQuantity.Number;
                    }
                }
            }

            foreach (var pair in quantitiesLookup)
            {
                minimumToPay += priceLookup[pair.Key].Price * pair.Value;
            }

            return minimumToPay;
        }

        private static Dictionary<string, int> GetOrderedQuantitiesInLookupFormat(TrolleyTotalRequest trolleyTotalRequest)
        {
            var quantitiesLookup = new Dictionary<string, int>();
            foreach (var quantity in trolleyTotalRequest.Quantities)
            {
                if (quantitiesLookup.ContainsKey(quantity.Name))
                {
                    quantitiesLookup[quantity.Name] += quantity.Number;
                }
                else
                {
                    quantitiesLookup.Add(quantity.Name, quantity.Number);
                }
            }

            return quantitiesLookup;
        }

        private static IOrderedEnumerable<SpecialWithActuals> GetSpecialsSortedByBestValueForMoney(TrolleyTotalRequest trolleyTotalRequest,
            Dictionary<string, ProductPrice> priceLookup)
        {
            var specialActual = new List<SpecialWithActuals>();


            foreach (var special in trolleyTotalRequest.Specials)
            {
                decimal actualPrice = 0;

                foreach (var specialQuantity in special.Quantities)
                {
                    actualPrice += priceLookup[specialQuantity.Name].Price * specialQuantity.Number;
                }


                var discountedRate = special.Total / actualPrice;
                if (discountedRate < 1)
                {
                    specialActual.Add(new SpecialWithActuals
                    {
                        Special = special,
                        ActualTotal = actualPrice,
                        DiscountedRate = discountedRate
                    });
                }
            }

            var sortedSpecialActual = specialActual.OrderBy(s => s.DiscountedRate);
            return sortedSpecialActual;
        }

        private static bool SpecialOfferCanBeUsed(SpecialWithActuals actuals, Dictionary<string, int> quantitiesLookup)
        {
            return actuals.Special.Quantities.ToList().TrueForAll(p =>
                quantitiesLookup.ContainsKey(p.Name) && quantitiesLookup[p.Name] >= p.Number);
        }
    }
}