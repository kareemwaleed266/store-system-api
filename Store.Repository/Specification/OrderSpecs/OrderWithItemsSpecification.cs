using Store.Data.Entities.OrderEntities;

namespace Store.Repository.Specification.OrderSpecs
{
    public class OrderWithItemsSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsSpecification(string buyerEmail)
            : base(order => order.BuyerEmail == buyerEmail)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
            AddOrderByDescinding(order => order.OrderDate);
            AddOrderByDescinding(order => order.SubTotal);
            AddOrderBy(order => order.SubTotal);
        }

        public OrderWithItemsSpecification(Guid id, string buyerEmail)
            : base(order => order.BuyerEmail == buyerEmail && order.Id == id)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
        }
    }
}
