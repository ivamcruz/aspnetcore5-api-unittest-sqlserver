using Portal.DevTest.Business.Interfaces;
using Portal.DevTest.Date.Filters;
using Portal.DevTest.Date.Interfaces;
using Portal.DevTest.Date.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.DevTest.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly StringBuilder _logsErros;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IUserRepository userRepository
            )
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _logsErros = new StringBuilder();
        }

        public StringBuilder AddNew(OrderModel order)
        {
            order.Id = Guid.NewGuid();
            order.CreationDate = DateTime.Now;
            order.IsActive = true;

            if (!ValidarUsuario(order.UserId))
                return _logsErros;

            if (order.lstOrderItems.Count() > 0)
                for (int i = 0; i < order.lstOrderItems.Count(); i++)
                {
                    if (!ValidarProduto(order.lstOrderItems[i].ProductId))
                        return _logsErros;

                    order.lstOrderItems[i].OrderId = order.Id;
                    order.lstOrderItems[i].Id = Guid.NewGuid();
                    order.lstOrderItems[i].IsActive = true;
                    order.lstOrderItems[i].CreationDate = DateTime.Now;
                }

            if (!string.IsNullOrEmpty(_logsErros.ToString()))
                return _logsErros;

            try { _orderRepository.Add(order); }
            catch (Exception ex) { return _logsErros.AppendFormat("An error occurred while trying to save: " + ex.Message); }

            return _logsErros;
        }

        public List<OrderModel> Search(OrdersFilter orderFilter)
        {
            List<OrderModel> lstOrders = new List<OrderModel>();

            try
            {
                lstOrders = _orderRepository.Search(a => a.IsActive.HasValue).Result.ToList();

                if (orderFilter.StartDate.HasValue && orderFilter.EndDate.HasValue)
                    lstOrders = lstOrders.Where(x => x.CreationDate >= orderFilter.StartDate && x.CreationDate <= orderFilter.EndDate)
                                         .ToList();

                if (orderFilter.MinTotal.HasValue)
                    lstOrders = lstOrders.Where(x => x.lstOrderItems.Sum(s => s.CurrentPrice * s.Amount) >= Convert.ToInt32(orderFilter.MinTotal)).ToList();

                if (orderFilter.MaxTotal.HasValue)
                    lstOrders = lstOrders.Where(x => x.lstOrderItems.Sum(s => s.CurrentPrice * s.Amount) >= Convert.ToInt32(orderFilter.MinTotal)).ToList();
            }
            catch (Exception) { return lstOrders; }

            return lstOrders;
        }

        public bool ValidarUsuario(Guid idUser)
        {
            if (_userRepository.GetById(idUser).Result == null)
            {
                _logsErros.AppendLine("User Id:" + idUser.ToString() + " not found");
                return false;
            }

            return true;
        }

        public bool ValidarProduto(Guid idProduct)
        {
            if (_productRepository.GetById(idProduct).Result == null)
            {
                _logsErros.AppendLine("Product Id: " + idProduct.ToString() + " not found");
                return false;
            }

            return true;
        }
    }
}
