namespace InterviewTest
{
    /// <summary>
    /// The root user input data structure.  You may assume the data is immutable
    /// </summary>
    public interface IUserInput
    {
        IUserInfo UserInfo { get; }
        IAddress ShippingAddress { get; }
        IShoppingCart Cart { get; }
        ICreditCard Card { get; }
    }

    /// <summary>
    /// User account information.  You may assume the data is immutable
    /// </summary>
    public interface IUserInfo
    {
        string Name { get; }
        string Email { get; }
    }

    /// <summary>
    /// Address information. You may assume the data is immutable
    /// </summary>
    public interface IAddress
    {
        string Street { get; }
        string City { get; }
        string State { get; }
        string PostalCode { get; }
    }

    /// <summary>
    /// Credit card information. You may assume the data is immutable
    /// </summary>
    public interface ICreditCard
    {
        public string NameOnCard { get; }
        public string CreditCardNumber { get; }
        public IAddress BillingAddress { get; }
        public int CCV { get; }
    }

    /// <summary>
    /// Individual product order information. You may assume the data is immutable
    /// </summary>
    public interface IOrder
    {
        int ProductId { get; }
        decimal UnitPrice { get; }
        int Quantity { get; }
    }

    /// <summary>
    /// The cumulative order information. You may assume the data is immutable
    /// </summary>
    public interface IShoppingCart : IEnumerable<IOrder>
    {

    }

}
