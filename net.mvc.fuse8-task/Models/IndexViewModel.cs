using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace net.mvc.fuse8_task.Models
{
    public class IndexViewModel
    {
        public List<string> TableHead = new List<string>
        {
            "Номер заказа", "Дата заказа", "Артикул товара",
            "Название товара", "Количество реализованных единиц товара",
            "Цена реализации за единицу продукции"
        };

        public string ExcelFilename = HttpContext.Current.Server.MapPath("~/App_Data/report.xlsx");
         
        public IEnumerable<OrderDetailProduct> Products;
        public bool IsDateIntervalSet = false;
        public bool IsErrorSet = false;
        public string ErrorMessage;
        private DateTime? _startDate;

        public DateTime? StartDate
        {
            get { return _startDate?.Date ?? DateTime.Now; }
            set { _startDate = value; }
        }

        private DateTime? _endDate;

        public DateTime? EndDate
        {
            get { return _endDate?.Date ?? DateTime.Now; }
            set { _endDate = value; }
        }
    }
}