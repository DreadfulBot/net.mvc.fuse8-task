using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using net.mvc.fuse8_task.Models;

namespace net.mvc.fuse8_task.Controllers
{
    public class HomeController : Controller
    {
        public IndexViewModel IndexViewModel;

        public ActionResult Index(string startDate, string endDate)
        {
            IndexViewModel data;
            // если заданы временные промежутки - отображаем запросы по ним
            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
            {
                data = GetDefaultResult();
            }
            // иначе - выводим записи по умолчанию
            else
            {
                data = GetIntervalResult(startDate, endDate);
                Session["startDateTxt"] = startDate;
                Session["endDateTxt"] = endDate;
            }
            return View(data);
        }

        // метод возвращает данные для отображения для случая, когда не установлены временные границы
        public IndexViewModel GetDefaultResult()
        {
            var nm = new NorthwindModel();
            var orderList = (from order in nm.Order
                join orderDetail in nm.OrderDetail on order.ID equals orderDetail.OrderID
                join product in nm.Product on orderDetail.ProductID equals product.ID
                select new OrderDetailProduct {Order = order, OrderDetail = orderDetail, Product = product})
                .OrderByDescending(o => o.Order.OrderDate)
                .Take(100)
                .ToList();

            var orderDate = orderList.First().Order.OrderDate;

            IndexViewModel = new IndexViewModel
            {
                IsDateIntervalSet = false,
                Products = orderList,
                StartDate = orderDate?.AddDays(-1) ?? DateTime.Now.AddDays(-1),
                EndDate = orderDate ?? DateTime.Now
            };

            return IndexViewModel;
        }

        // метод возвращает данные для отображения для случая, когда установлены временные границы
        public IndexViewModel GetIntervalResult(string startDate, string endDate)
        {
            if(string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
                throw new Exception("Неверно заданы временные границы");

            try
            {
                IndexViewModel = new IndexViewModel
                {
                    IsDateIntervalSet = true,
                    StartDate = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.CurrentCulture,
                        DateTimeStyles.None),
                    EndDate = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.CurrentCulture,
                        DateTimeStyles.None)
                };

                var nm = new NorthwindModel();
                var orderList = (
                    from order in nm.Order
                    join orderDetail in nm.OrderDetail on order.ID equals orderDetail.OrderID
                    join product in nm.Product on orderDetail.ProductID equals product.ID
                    select new OrderDetailProduct {Order = order, OrderDetail = orderDetail, Product = product})
                    .Where(
                        o => o.Order.OrderDate >= IndexViewModel.StartDate &&
                             o.Order.OrderDate <= IndexViewModel.EndDate
                    )
                    .OrderByDescending(o => o.Order.OrderDate)
                    .ToList();

                IndexViewModel.Products = orderList;


            }
            catch (Exception ex)
            {
                IndexViewModel = new IndexViewModel
                {
                    IsDateIntervalSet = false,
                    IsErrorSet = true,
                    ErrorMessage = ex.Message
                };
                return IndexViewModel;
            }

            return IndexViewModel;
        }
    }
}