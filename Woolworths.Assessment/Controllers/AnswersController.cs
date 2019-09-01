using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Woolworths.Assessment.Enums;
using Woolworths.Assessment.Models;
using Woolworths.Assessment.Services.Interfaces;

namespace Woolworths.Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IProductSorter _productSorter;
        private readonly IWoolworthsResourceProvider _woolworthsResourceProvider;
        public AnswersController(AppSettings appSettings, IProductSorter productSorter, IWoolworthsResourceProvider woolworthsResourceProvider)
        {
            _appSettings = appSettings;
            _productSorter = productSorter;
            _woolworthsResourceProvider = woolworthsResourceProvider;
        }

        [HttpGet]
        [Route("User")]
        public IActionResult GetUser()
        {
            return Ok(new
            {
                name = _appSettings.UserName(),
                token = _appSettings.Token()
            });
        }


        [HttpGet]
        [Route("sort")]
        public async Task<IActionResult> GetProducts(SortOption? sortOption)
        {
            if (!sortOption.HasValue)
            {
                return BadRequest("Please provide valid sort option query parameter.");
            }
            var products = await _woolworthsResourceProvider.GetProducts();
            if (products == null)
            {
                return Ok((List<Product>) null);
            }

            var sortedProducts = await _productSorter.GetSortedProducts(sortOption.Value, products);

            return Ok(sortedProducts);
        }

        [HttpPost]
        [Route("trolleyTotal")]
        public async Task<IActionResult> CalculateTrolleyTotal(TrolleyTotalRequest trolleyTotalRequest)
        {
            if (trolleyTotalRequest == null)
            {
                return BadRequest("Please provide valid trolley total request.");
            }

            double trolleyTotal = await _woolworthsResourceProvider.CalculateTrolleyTotal(trolleyTotalRequest);

            return Ok(trolleyTotal);
        }


    }
}
