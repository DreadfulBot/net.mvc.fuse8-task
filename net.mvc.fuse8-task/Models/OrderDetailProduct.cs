using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace net.mvc.fuse8_task.Models
{
    public class OrderDetailProduct
    {
        public Order Order { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public Product Product { get; set; }
    }
}