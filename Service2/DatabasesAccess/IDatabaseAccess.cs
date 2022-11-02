using Service2.Models;

namespace Service2.Interfaces
{
    public interface IDatabaseAccess
    {
        /// <summary>
        /// поиск инфы о клиенте по одному из значений его полей
        /// </summary>
        /// <param name="client">объект клиента, где заполнено хотя бы одно поле</param>
        /// <returns>полная информация о клиенте, включая его ID</returns>
        public Task<Client> GetClient(Client client);

        /// <summary>
        /// записать заказ в БД
        /// </summary>
        /// <param name="order"></param>
        public Task SetOrder(Order order);

        /// <summary>
        /// Проверить товары на наличие в БД
        /// </summary>
        /// <param name="products"></param>
        /// <returns>Вернет список товаров, отсутствующих в БД</returns>
        public Task<List<Product>> GetFailProducts(List<Product> products);
    }
}
