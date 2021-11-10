using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.DevTest.Business.Interfaces;
using Portal.DevTest.Date.Filters;
using Portal.DevTest.Date.Interfaces;
using Portal.DevTest.Date.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalTele.DevTest.API.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        public OrderController(
            IMapper mapper,
            IOrderService orderService,
            IOrderRepository orderRepository
            )
        {
            _mapper = mapper;
            _orderService = orderService;
            _orderRepository = orderRepository;
        }

        [HttpPost("orders")]
        [Authorize("write:messages")]
        public ActionResult<string> Add(OrderModel orderModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            var logsAttemptSaveNewOrder = _orderService.AddNew(orderModel);

            if (!string.IsNullOrEmpty(logsAttemptSaveNewOrder.ToString()))
                return BadRequest(logsAttemptSaveNewOrder.ToString());

            return Ok("Order Created");
        }

        [HttpGet("orders")]
        [Authorize("read:messages")]
        public ActionResult<List<OrderModel>> GetAll()
        {
            List<OrderModel> lstOrders;

            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            try { lstOrders = _orderRepository.GetAll().Result.ToList(); } catch (Exception ex) { return BadRequest(ex.Message.ToString()); }

            return Ok(lstOrders);
        }

        [HttpPost("orders/search")]
        [Authorize("read:messages")]
        public ActionResult<List<OrderModel>> Search(OrdersFilter orderFilter)
        {
            List<OrderModel> lstOrders;

            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            try { lstOrders = _orderService.Search(orderFilter).ToList(); } catch (Exception ex) { return BadRequest(ex.Message.ToString()); }

            return Ok(lstOrders);
        }
    }
}
