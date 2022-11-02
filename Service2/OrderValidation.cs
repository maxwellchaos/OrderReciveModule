using Service2.Interfaces;
using Service2.Models;

namespace Service2
{
    public class OrderValidation
    {
        private readonly IDatabaseAccess _databaseAccess;
        public string? ErrorMessage { get; set; }
        public OrderValidation(IDatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        public async Task<bool> IsValid(Order? order)
        {
            if (order == null)
            {
                ErrorMessage = "Заказ отсутствует";
                return false;
            }

            try
            {
                await _databaseAccess.GetClient(order.Client);
            }
            catch (Exception)
            {
                ErrorMessage = "Не указан или указан неправильно клиент";
                return false;
            }

            //Проверка товаров в заказе
            List<Product> products = await _databaseAccess.GetFailProducts(order.Products);
            if (products.Count != 0)
            {
                List<string> productIds = new List<string>();
                foreach (Product product in products)
                {
                    productIds.Add(product.Name + " - " + product.Id);
                }
                ErrorMessage = "Ошибки в следующих товарах:"
                    + productIds;
                return false;
            }

            return true;

        }


    }
}
