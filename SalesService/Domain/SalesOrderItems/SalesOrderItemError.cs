using Shared.Results;

namespace SalesService.Domain.SalesOrderItems
{
    public static class SalesOrderItemError
    {
        public static Error InvalidOrderId=> 
            Error.Validation("InvalidOrderId", "The order ID is invalid.");
        public static Error InvalidProductId=>
            Error.Validation("InvalidProductId", "The product ID is invalid.");
        public static Error InvalidQuantity =>
            Error.Validation("InvalidQuantity", "The quantity must be greater than zero.");
        public static Error InvalidUnitPrice =>
            Error.Validation("InvalidUnitPrice", "The unit price must be greater than zero.");
        public static Error InvalidDiscount =>
            Error.Validation("InvalidDiscount", "The discount must be greater than or equal to zero.");
        public static Error InvalidTax =>
            Error.Validation("InvalidTax", "The tax must be greater than or equal to zero.");
    }
}
