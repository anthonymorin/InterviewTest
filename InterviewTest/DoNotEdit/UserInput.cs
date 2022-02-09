using System.Collections;

namespace InterviewTest
{
    public class UserInput : IUserInput
    {
        public UserInput(IUserInfo info, IAddress shippingAddress, IShoppingCart shoppingCart, ICreditCard creditCard)
        {
            this.UserInfo = info;
            this.ShippingAddress = shippingAddress;
            this.Cart = shoppingCart;
            this.Card = creditCard;
        }
        public IUserInfo UserInfo { get; }

        public IAddress ShippingAddress { get; }

        public IShoppingCart Cart { get; }

        public ICreditCard Card { get; }
    }
    public class UserInfo : IUserInfo
    {
        public UserInfo(string Name, string Email)
        {
            this.Name = Name;
            this.Email = Email;
        }

        public string Name { get; }

        public string Email { get; }
    }

    public class Address : IAddress
    {
        public Address(string Street, string City, string State, string PostalCode)
        {
            this.Street = Street;
            this.City = City;
            this.State = State;
            this.PostalCode = PostalCode;
        }
        public string Street { get; }

        public string City { get; }

        public string State { get; }

        public string PostalCode { get; }
    }

    public class ShoppingCart : IShoppingCart
    {
        public ShoppingCart(IEnumerable<IOrder> orders)
        {
            this.orders = new List<IOrder>(orders);
        }

        List<IOrder> orders { get; }

        public IEnumerator<IOrder> GetEnumerator() => orders.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class Order : IOrder
    {
        public Order(int ProductId, decimal UnitPrice, int Quantity)
        {
            this.ProductId = ProductId;
            this.UnitPrice = UnitPrice;
            this.Quantity = Quantity;
        }

        public int ProductId { get; }

        public decimal UnitPrice { get; }

        public int Quantity { get; }
    }

    public class CreditCard : ICreditCard
    {
        public CreditCard(string NameOnCard, string CreditCardNumber, IAddress BillingAddress, int CCV)
        {
            this.NameOnCard = NameOnCard;
            this.CreditCardNumber = CreditCardNumber;
            this.BillingAddress = BillingAddress;
            this.CCV = CCV;
        }
        public string NameOnCard { get; }

        public string CreditCardNumber { get; }

        public IAddress BillingAddress { get; }

        public int CCV { get; }
    }
}
