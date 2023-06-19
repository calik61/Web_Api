using System.Collections.Generic;
using WebApi.Model.Request;

namespace WebApi.Model.Response
{
    public class ResponseItem
    {

        public string UserName { get; set; }
        public decimal Payment { get; set; }
        public List<ResponseProductItem> ProductList { get; set; }
    }
    public class ResponseProductItem
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
