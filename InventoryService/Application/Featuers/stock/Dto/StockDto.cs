namespace InventoryService.Application.Featuers.stock.Dto
{
    public class StockDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get;  set; }

        public Guid WarehouseId { get;  set; }

        public int Quantity { get; set; }

    }
}
