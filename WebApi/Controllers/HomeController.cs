using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using WebApi.Model;
using WebApi.Utils;
using WebApi.Model.Request;
using WebApi.Model.Response;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public HomeController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        //test verileri
        static List<Products> Products = new List<Products>
        {
            new Products(){Id=1,Name="Laptop",Code="001",Type=(byte)ProductTypes.Genel ,Price=15000},
            new Products(){Id=2,Name="Sandalye",Code="002",Type=(byte)ProductTypes.Genel ,Price=2500},
            new Products(){Id=3,Name="Masa",Code="003",Type=(byte)ProductTypes.Genel ,Price=4500},
            new Products(){Id=4,Name="Çikolata",Code="004",Type=(byte)ProductTypes.Bakkaliye ,Price=5},
            new Products(){Id=4,Name="Ekmek",Code="005",Type=(byte)ProductTypes.Bakkaliye ,Price=6},
        };
        static List<Users> Users = new List<Users>
        {
            new Users(){Id=1,Name="Ali A",Code="1000",Type=(byte)UserTypes.Employee ,Date=DateTime.Now.AddYears(-1)},
            new Users(){Id=2,Name="Mehmet B",Code="1001",Type=(byte)UserTypes.Member ,Date=DateTime.Now.AddYears(-5)},
            new Users(){Id=3,Name="Mesut C",Code="1002",Type=(byte)UserTypes.Customer ,Date=DateTime.Now.AddMonths(5)},
            new Users(){Id=3,Name="Veli D",Code="1003",Type=(byte)UserTypes.Customer ,Date=DateTime.Now.AddYears(-3)},
        };

        [HttpPost("getbill")]
        public IActionResult GetBill([FromBody] RequestItem request)
        {
            var userItem = Users.Where(x => x.Code == request.UserCode).FirstOrDefault();
            ResponseItem responseItem = new ResponseItem();
            List<ResponseProductItem> ProductList = new List<ResponseProductItem>();
            decimal totalDiscount = 0, grandTotal = 0;
            if (userItem != null)
            {
                foreach (var item in request.ProductList)
                {
                    var prodItem = Products.Where(x => x.Code == item.ProductCode).FirstOrDefault();
                    grandTotal += prodItem.Price * item.Quantity;

                    if (prodItem.Type != (byte)ProductTypes.Bakkaliye)//bakkaliye ürünü değilse indirim uygula
                    {
                        if (userItem.Type == (byte)UserTypes.Employee)
                        {
                            totalDiscount += (prodItem.Price * item.Quantity * (decimal)0.30);
                        }
                        else if (userItem.Type == (byte)UserTypes.Member)
                        {
                            totalDiscount += (prodItem.Price * item.Quantity * (decimal)0.10);
                        }
                        else if (userItem.Type == (byte)UserTypes.Customer)
                        {
                            var datediff = (decimal)((DateTime.Now - userItem.Date).TotalDays / 365);
                            if (datediff >= (decimal)2)// 2 yıldır müşteri mi? 
                            {
                                totalDiscount += (prodItem.Price * item.Quantity * (decimal)0.05);
                            }
                        }
                    }
                    ProductList.Add(new ResponseProductItem { ProductCode = prodItem.Code, ProductName = prodItem.Name, Price = prodItem.Price, Quantity = item.Quantity });//alınan ürün bilgileri için dizii
                }
                if (grandTotal >= 100)
                {
                    totalDiscount += (int)(grandTotal / 100) * 5;
                }

                responseItem.ProductList = ProductList;
                responseItem.Payment = grandTotal - totalDiscount;
                responseItem.UserName = userItem.Name;
                return Ok(responseItem);
            }
            return BadRequest();

        }
    }
}
