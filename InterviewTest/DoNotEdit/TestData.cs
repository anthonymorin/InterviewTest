using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest
{
    public static class TestData
    {
        public static IEnumerable<IUserInput> GetUserInputs()
        {
            UserInfo
                Alice = new UserInfo("Alice", "Alice@fake.com"),
                Bob = new UserInfo("Bob", "Bob.email@fake.com"),
                Charlie = new UserInfo("Charlie", "Charlie@fake.com");
            Address
                AliceAddress = new Address("123 Fake Street", "Pleasanton", "Ca", "94588"),
                BobAddress = new Address("123 Fake Street", "Pleasanton", "Ca", "94588"),
                CharlieAddress = new Address("123 Fake Street", "Pleasanton", "Ca", "94588");
            CreditCard
                AliceCard = new CreditCard(Alice.Name, "1234-5678-9012-3456", AliceAddress, 123),
                AliceCardBad = new CreditCard(Alice.Name, "1234-5678-9012-3456", AliceAddress, 12310),
                BobCard = new CreditCard(Bob.Name, "1234-5678-9012-3456", AliceAddress, 234),
                CharlieCard = new CreditCard(Charlie.Name, "1234-5678-9012-3456", AliceAddress, 345);
            Order
                Order1 = new Order(145, 10.45m, 6),
                Order2 = new Order(5746, 61.30m, 2),
                Order3 = new Order(2, 9.73m, 5),
                Order4 = new Order(525, 12.99m, 17),
                Order5 = new Order(8633, 103.00m, 3),
                Order6 = new Order(23, 1.64m, 35),
                Order7 = new Order(63, 19.95m, 1),
                Order8 = new Order(834, 14.53m, 12);

            ShoppingCart
                Cart1 = new ShoppingCart(new[] { Order1, Order3, Order4 }),
                Cart2 = new ShoppingCart(new[] { Order2 }),
                Cart3 = new ShoppingCart(new[] { Order5, Order6, Order7 }),
                Cart4 = new ShoppingCart(new[] { Order1, Order3, Order4 }),
                Cart5 = new ShoppingCart(new[] { Order1, Order2, Order3, Order4, Order5, Order6, Order7, Order8, });

            UserInput
                inputA = new UserInput(Alice, AliceAddress, Cart1, AliceCardBad),
                inputB = new UserInput(Bob, BobAddress, Cart2, BobCard),
                inputC = new UserInput(Bob, BobAddress, Cart3, BobCard),
                inputD = new UserInput(Alice, AliceAddress, Cart4, AliceCard),
                inputE = new UserInput(Charlie, CharlieAddress, Cart5, CharlieCard);

            return new[] { inputA, inputB, inputC, inputD, inputE };
        }
    }
}
