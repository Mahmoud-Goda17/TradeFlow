using Shared.Results;

namespace SalesService.Domain.SalesOrders
{
    public static class SalesOrderError
    {
        public static Error OrderNotPending =>
            Error.Validation("OrderNotPending", "The order is not in a pending state.");
        public static Error ItemNotFound =>
            Error.Validation("ItemNotFound", "The item was not found in the order.");
        public static Error InvalidStatusTransition =>
            Error.Validation("InvalidStatusTransition", "The status transition is not valid.");
        public static Error OrderHasNoItems =>
            Error.Validation("OrderHasNoItems", "The order has no items.");
        public static Error CannotCancelCompletedOrder =>
            Error.Validation("CannotCancelCompletedOrder", "Cannot cancel a completed order.");
        public static Error OrderAlreadyCancelled =>
            Error.Conflict("OrderAlreadyCancelled", "The order is already cancelled.");
        public static Error InvalidOrderNumber =>
            Error.Validation("InvalidOrderNumber", "The order Number is invalid.");
        public static Error InvalidCustomerId =>
            Error.Validation("InvalidCustomerId", "The customer ID is invalid.");
        public static Error InvalidShippingAddress =>
            Error.Validation("InvalidShippingAddress", "The order Shipping Address is invalid.");
    }
}
