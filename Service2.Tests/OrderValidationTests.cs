using Microsoft.IdentityModel.Tokens;
using Moq;
using Service2;
using Service2.Interfaces;
using Service2.Models;
using System.Buffers;

namespace Service2.Tests
{
    public class OrderValidationTests
    {
        [Fact]
        public async void OrderValidationIsNull()
        {
            var moqBd = new Mock<IDatabaseAccess>();
            OrderValidation orderValidation = new OrderValidation(moqBd.Object);

            //ѕроверка на нулевой заказ
            bool result = await orderValidation.IsValid(null);

            Assert.False(result);
        }


        [Fact]
        public async void OrderValidationBadProducts()
        {
            
            var moqBd = new Mock<IDatabaseAccess>();
            Client client = new Client()
            {
                Id = 1,
                Name = "Client1",
                Email = "Client1@mail.ru",
                TelegramId = "@Client1"
            };
            List<Product> products = new List<Product>();
            products.Add(new Product() { Id = 1, Name = "dsf", Price = 100 });

            //при любых входных данных в эти методы, если возвращаетс€ требуемое,
            //то должен быть соответствующий результат
            moqBd.Setup(db => db.GetClient(It.IsAny<Client>())).ReturnsAsync(client);
            moqBd.Setup(db => db.GetFailProducts(It.IsAny<List<Product>>())).ReturnsAsync(products);
            OrderValidation orderValidation = new OrderValidation(moqBd.Object);
            Order order = new Order()
            {
                Client = new Client { Id = 1 }
            };

            bool result = await orderValidation.IsValid(order);

            Assert.False(result);

        }

    }
}