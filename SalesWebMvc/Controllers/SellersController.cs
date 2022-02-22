using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellersService;

        public SellersController(SellerService sellersService)
        {
            _sellersService = sellersService;
        }

        public IActionResult Index()
        {
            var list = _sellersService.FindAll();
            return View(list);
        }
    }
}
