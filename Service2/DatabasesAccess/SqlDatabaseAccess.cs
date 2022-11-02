using Microsoft.EntityFrameworkCore;
using Service2.Data;
using Service2.Interfaces;
using Service2.Models;

namespace Service2.DatabasesAccess
{

    public class SqlDatabaseAccess : IDatabaseAccess
    {
        private readonly Service2Context _context;

        public SqlDatabaseAccess(Service2Context context)
        {
            _context = context;
        }

        public async Task<Client> GetClient(Client client)
        {
            if (client.Id != 0)
            {
                return await GetClientById(client.Id);
            }

            //Дальнейшее нужно будет для других форматов и обработчиков и пока не реализовано,
            // потому закомментровано

            //if (client.Name != null)
            //{
            //    return GetClientByName(client.Name);
            //}
            //if (client.Email != null)
            //{
            //    return GetClientByEmail(client.Email);
            //}
            //if (client.TelegramId != null)
            //{
            //    return GetClientByTelegramId(client.TelegramId);
            //}

            throw new Exception("Отсутствует информация о клиенте");
        }

        public async Task<Client> GetClientById(int Id)
        {
            try
            {
                return await _context.Client.SingleAsync(x => x.Id == Id);
            }
            catch
            {
                throw new Exception("Отсутствует информация о клиенте c Id = " + Id);
            }
        }

        public async Task<List<Product>> GetFailProducts(List<Product> products)
        {
            return  products.Except(await _context.Product.ToListAsync()).ToList();
        }

        public async Task SetOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
        }
        public Order GetOrder(Order order)
        {
            throw new NotImplementedException();
        }
        public bool UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
