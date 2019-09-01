using Newtonsoft.Json;
using NUnit.Framework;
using Woolworths.Assessment.Models;
using Woolworths.Assessment.Services.Interfaces;

namespace Woolworths.Assessment.TestProject
{
    public class TrolleyCalculatorTests
    {
        [Test,Explicit("Yet to be fixed.")]
        public void ItReturns14()
        {
            TrolleyTotalRequest trolleyTotalRequest = JsonConvert.DeserializeObject<TrolleyTotalRequest>(@"{""Products"":[{""Name"":""1"",""Price"":2.0},{""Name"":""2"",""Price"":5.0}],""Specials"":[{""Quantities"":[{""Name"":""1"",""Quantity"":3},{""Name"":""2"",""Quantity"":0}],""Total"":5.0},{""Quantities"":[{""Name"":""1"",""Quantity"":1},{""Name"":""2"",""Quantity"":2}],""Total"":10.0}],""Quantities"":[{""Name"":""1"",""Quantity"":3},{""Name"":""2"",""Quantity"":2}]}");
            ITrolleyCalculator trolleyCalculator = new TrolleyCalculator();
            var result = trolleyCalculator.CalculateTrolleyTotal(trolleyTotalRequest);
            Assert.AreEqual(result, (double)14.0);
        }

        [Test]
        public void ItReturnsAbout119()
        {
            TrolleyTotalRequest trolleyTotalRequest = JsonConvert.DeserializeObject<TrolleyTotalRequest>(@"{""Products"":[{""Name"":""Product 0"",""Price"":6.24631655926179},{""Name"":""Product 1"",""Price"":8.77974736680265},{""Name"":""Product 2"",""Price"":2.43343426260791},{""Name"":""Product 3"",""Price"":0.221432021456506},{""Name"":""Product 4"",""Price"":10.9459652639674},{""Name"":""Product 5"",""Price"":7.42980477047609}],""Specials"":[{""Quantities"":[{""Name"":""Product 0"",""Quantity"":4},{""Name"":""Product 1"",""Quantity"":5},{""Name"":""Product 2"",""Quantity"":1},{""Name"":""Product 3"",""Quantity"":9},{""Name"":""Product 4"",""Quantity"":8},{""Name"":""Product 5"",""Quantity"":5}],""Total"":24.105041162944},{""Quantities"":[{""Name"":""Product 0"",""Quantity"":9},{""Name"":""Product 1"",""Quantity"":7},{""Name"":""Product 2"",""Quantity"":9},{""Name"":""Product 3"",""Quantity"":6},{""Name"":""Product 4"",""Quantity"":1},{""Name"":""Product 5"",""Quantity"":7}],""Total"":12.4321490726757},{""Quantities"":[{""Name"":""Product 0"",""Quantity"":7},{""Name"":""Product 1"",""Quantity"":3},{""Name"":""Product 2"",""Quantity"":2},{""Name"":""Product 3"",""Quantity"":4},{""Name"":""Product 4"",""Quantity"":7},{""Name"":""Product 5"",""Quantity"":6}],""Total"":24.6869145355206},{""Quantities"":[{""Name"":""Product 0"",""Quantity"":1},{""Name"":""Product 1"",""Quantity"":8},{""Name"":""Product 2"",""Quantity"":8},{""Name"":""Product 3"",""Quantity"":7},{""Name"":""Product 4"",""Quantity"":1},{""Name"":""Product 5"",""Quantity"":1}],""Total"":14.4725819804986},{""Quantities"":[{""Name"":""Product 0"",""Quantity"":0},{""Name"":""Product 1"",""Quantity"":6},{""Name"":""Product 2"",""Quantity"":5},{""Name"":""Product 3"",""Quantity"":0},{""Name"":""Product 4"",""Quantity"":0},{""Name"":""Product 5"",""Quantity"":2}],""Total"":19.5892991503881},{""Quantities"":[{""Name"":""Product 0"",""Quantity"":9},{""Name"":""Product 1"",""Quantity"":5},{""Name"":""Product 2"",""Quantity"":0},{""Name"":""Product 3"",""Quantity"":9},{""Name"":""Product 4"",""Quantity"":9},{""Name"":""Product 5"",""Quantity"":1}],""Total"":14.6178697281498},{""Quantities"":[{""Name"":""Product 0"",""Quantity"":4},{""Name"":""Product 1"",""Quantity"":8},{""Name"":""Product 2"",""Quantity"":8},{""Name"":""Product 3"",""Quantity"":0},{""Name"":""Product 4"",""Quantity"":3},{""Name"":""Product 5"",""Quantity"":0}],""Total"":15.8639300218439},{""Quantities"":[{""Name"":""Product 0"",""Quantity"":9},{""Name"":""Product 1"",""Quantity"":6},{""Name"":""Product 2"",""Quantity"":3},{""Name"":""Product 3"",""Quantity"":7},{""Name"":""Product 4"",""Quantity"":5},{""Name"":""Product 5"",""Quantity"":9}],""Total"":0.382519387492927}],""Quantities"":[{""Name"":""Product 0"",""Quantity"":3},{""Name"":""Product 1"",""Quantity"":0},{""Name"":""Product 2"",""Quantity"":9},{""Name"":""Product 3"",""Quantity"":4},{""Name"":""Product 4"",""Quantity"":1},{""Name"":""Product 5"",""Quantity"":9}]}");
            ITrolleyCalculator trolleyCalculator = new TrolleyCalculator();
            var result = trolleyCalculator.CalculateTrolleyTotal(trolleyTotalRequest);
            Assert.AreEqual(119.339794325334794m, result);
        }
    }
}