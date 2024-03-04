using CloudNautical.Domain.Entities.Order;
using System.Data;

namespace CloudNautical.DataAccess.ADO.Repository
{
    public class OrderRepository : BaseRepository
    {
        public OrderRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<OrderDetails> GetDeliveryDetails(string customerId, string userId)
        {
            var sql = "SELECT o.ORDERID, o.ORDERDATE, o.DELIVERYEXPECTED, o.CONTAINSGIFT,\r\n       c.FIRSTNAME, c.LASTNAME, c.EMAIL, c.HOUSENO, c.STREET, c.TOWN, c.POSTCODE,\r\n       p.PRODUCTNAME, p.COLOUR, p.SIZE, oi.QUANTITY, oi.PRICE\r\nFROM ORDERS o\r\nJOIN CUSTOMERS c ON o.CUSTOMERID = c.CUSTOMERID\r\nJOIN ORDERITEMS oi ON o.ORDERID = oi.ORDERID\r\nJOIN PRODUCTS p ON oi.PRODUCTID = p.PRODUCTID\r\nWHERE o.ORDERDATE = (\r\n    SELECT MAX(ORDERDATE)\r\n    FROM ORDERS\r\n    WHERE CUSTOMERID = @CustomerId);\r\n";
            var param = new Dictionary<string, object>
            {
                { "@CustomerId",customerId}
            };
            try
            {
                return ExecuteQuery(sqlQuery: sql, MapCustomerOrder, parameters: param).First();
            }
            catch (Exception ex)
            {
                return new OrderDetails();
            }
        }
        private OrderDetails MapCustomerOrder(IDataReader reader)
        {
            OrderDetails customer = new OrderDetails();
            customer.OrderItems = new List<OrderItem>();
            while (reader.Read())
            {
                customer.OrderId = Convert.ToInt32(reader["OrderId"]);
                customer.FirstName = reader["FirstName"].ToString();
                customer.LastName = reader["LastName"].ToString();
                customer.Email = reader["Email"].ToString();
                customer.OrderDate = reader["OrderDate"].ToString();
                customer.DeliveryExpected = reader["DeliveryExpected"].ToString();
                customer.ContainsGift = Convert.ToInt32(reader["ContainsGift"]);
                customer.HouseNo = reader["HouseNo"].ToString();
                customer.Street = reader["Street"].ToString();
                customer.Town = reader["Town"].ToString();
                customer.Postcode = reader["Postcode"].ToString();
                customer.OrderItems.Add(new OrderItem
                {
                    ProductName = reader["ProductName"].ToString(),
                    Colour = reader["Colour"].ToString(),
                    Size = reader["Size"].ToString(),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    Price = Convert.ToDecimal(reader["Price"]),
                });
            }
            return customer;
        }
    }

   
}
