using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductSercice.Application.Featuers.Products.Dtos;
using ProductSercice.Application.Featuers.Products.Mapper;
using ProductSercice.Application.Interfaces;
using ProductSercice.Domain.Event;
using ProductSercice.Domain.Product;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Command.CreateProduct
{
    public class CreateProducrCommandHandler(IAppDbContext context,
        IMemoryCache cache, ILogger<CreateProducrCommandHandler> logger, IMediator mediator) : IRequestHandler<CreateProducrCommand, Result<ProductDto>>
    {
        public  async Task<Result<ProductDto>> Handle(CreateProducrCommand request, CancellationToken cancellationToken)
        {
            var product = await context.Products.AnyAsync(p => p.SKU == request.sKU || p.Barcode == request.barcode, cancellationToken);
            if (product)
            {
                logger.LogWarning("Product with SKU {SKU} or Barcode {Barcode} already exists.", request.sKU, request.barcode);
                return ProductAppError.IsExcist("product","SKU","Barcode", request.sKU, request.barcode);
            }
            var newProduct = Product.Create(Guid.NewGuid(),
                request.name,
                request.description,
                request.sKU, request.barcode,
                request.categoryId, request.brandId, request.costPrice, request.sellingPric, request.status);
            if (newProduct.IsError)
            {
                logger.LogWarning("Failed to create product: {Errors}", newProduct.Errors);
                return newProduct.Errors;
            }
            context.Products.Add(newProduct.Value);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("products");
            var productEvent = new ProductEvent(
                           eventId: Guid.NewGuid(),
                           productId: newProduct.Value.Id,
                           productName: newProduct.Value.Name);

            await mediator.Publish(productEvent,cancellationToken); 
            return newProduct.Value.ToProductDto();
        }
    }
}
