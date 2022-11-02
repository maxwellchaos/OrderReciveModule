using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service2.Data;
using Service2.DatabasesAccess;
using Service2.Interfaces;
using Service2.Models;

namespace Service2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly Service2Context _context;
        private readonly IDatabaseAccess _databasesAccess;
        private readonly OrderValidation _orderValidator;

        public OrdersController(Service2Context context)
        {
            _context = context;
            _databasesAccess = new SqlDatabaseAccess(_context);
            _orderValidator = new OrderValidation(_databasesAccess);

        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _context.Order.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            if (await _orderValidator.IsValid(order))
            {
                await _databasesAccess.SetOrder(order);
                return CreatedAtAction("GetOrder", new { id = order.Id }, order);
            }
            return BadRequest(_orderValidator.ErrorMessage);
            
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
