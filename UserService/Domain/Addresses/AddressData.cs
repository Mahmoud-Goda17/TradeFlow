namespace UserService.Domain.Addresses
{
    public sealed record class AddressData(Guid? Id,
    string City,
    string Street,
    string BuildingNo,
    string? PostalCode)
    {
    }
}
