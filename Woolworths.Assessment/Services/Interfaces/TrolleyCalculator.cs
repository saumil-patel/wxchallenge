using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Woolworths.Assessment.Models;

namespace Woolworths.Assessment.Services.Interfaces
{
    public class TrolleyCalculator : ITrolleyCalculator
    {
        public decimal CalculateTrolleyTotal(TrolleyTotalRequest trolleyTotalRequest)
        {
            var priceLookup = GetProductPriceLookup(trolleyTotalRequest);

            var quantitiesLookup = GetOrderedQuantitiesInLookupFormat(trolleyTotalRequest).ToImmutableDictionary();

            return GetLowestTotal(priceLookup, trolleyTotalRequest.Specials.ToImmutableList(), quantitiesLookup, 0);
        }

        private decimal GetLowestTotal(Dictionary<string, ProductPrice> priceLookup, ImmutableList<Special> sortedSpecials, ImmutableDictionary<string, int> quantitiesLookup, decimal currentTotal)
        {
            var lowestCost = new SortedSet<decimal>();
            foreach (var specialOffer in sortedSpecials)
            {

                if (SpecialOfferCanBeApplied(specialOffer, quantitiesLookup))
                {

                    var remainingQuantity = GetQuantityAfterOffer(specialOffer, quantitiesLookup);
                    var offerTotal = currentTotal + specialOffer.Total;

                    var remainingLowest = GetLowestTotal(priceLookup, sortedSpecials, remainingQuantity, offerTotal);
                    lowestCost.Add(remainingLowest);
                }
            }

            lowestCost.Add(currentTotal + GetNoOfferRemainingTotal(quantitiesLookup, priceLookup));
            return lowestCost.Min;
        }

        private ImmutableDictionary<string, int> GetQuantityAfterOffer(Special specialOffer, ImmutableDictionary<string, int> quantitiesLookup)
        {
            var newQuantitiesLookup = new Dictionary<string, int>();
            foreach (var specialQuantity in specialOffer.Quantities)
            {
                newQuantitiesLookup.Add(specialQuantity.Name, quantitiesLookup[specialQuantity.Name] - specialQuantity.Number);
            }

            return newQuantitiesLookup.ToImmutableDictionary();
        }

        private decimal GetNoOfferRemainingTotal(ImmutableDictionary<string, int> quantitiesLookup, Dictionary<string, ProductPrice> priceLookup)
        {
            decimal remainingTotal = 0;

            foreach (var pair in quantitiesLookup)
            {
                remainingTotal += pair.Value * priceLookup[pair.Key].Price;
            }

            return remainingTotal;
        }

        private static Dictionary<string, ProductPrice> GetProductPriceLookup(TrolleyTotalRequest trolleyTotalRequest)
        {
            var priceLookup = new Dictionary<string, ProductPrice>();
            foreach (var product in trolleyTotalRequest.Products)
            {
                priceLookup.Add(product.Name, product);
            }

            return priceLookup;
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


        private static bool SpecialOfferCanBeApplied(Special special, ImmutableDictionary<string, int> quantitiesLookup)
        {
            return special.Quantities.ToList().TrueForAll(p =>
                quantitiesLookup.ContainsKey(p.Name) && quantitiesLookup[p.Name] >= p.Number);
        }
    }
}