using System.Collections.Generic;

namespace WebApi.Model.Request
{
    public class RequestItem
    {
        public string UserCode { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public List<RequestProductItem> ProductList { get; set; }
    }
    public class RequestProductItem
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }

    }
}
