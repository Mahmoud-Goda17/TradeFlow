using Shared.Commen;
using Shared.Results;

namespace InventoryService.Domain.Warehouse
{
    public sealed class WhereHouse : AuditableEntity
    {
        public string Name { get; private set; } = default!;
        public string Code { get; private set; } = default!;
        public string Address { get; private set; } = default!;
        public bool IsActive { get; private set; }

        private WhereHouse()
        {
        }

        private WhereHouse(Guid id,string name, string code, string address, bool isActive): base(id) 
        {
            Name = name;
            Code = code;
            Address = address;
            IsActive = isActive;
        }

        public static Result<WhereHouse> Create(Guid id, string name, string code, string address, bool isActive)
        {
            var errors = Errors(name, code, address);
            if (errors.Count > 0)
            {
                return errors;
            }
            var warehouse = new WhereHouse(id, name, code, address, isActive);
            return warehouse;
        }
        public Result<Updated> Update(string name, string code, string address)
        {
            var errors = Errors(name, code, address);
            if (errors.Count > 0)
            {
                return errors;
            }
            Name = name;
            Code = code;
            Address = address;
            return Result.Updated;
        }
        public Result<Updated> Activate()
        {
            if (IsActive)
            {
                return WhwerHouseError.WarehouseAlreadyActive;
            }
            IsActive = true;
            return Result.Updated;
        }
        public Result<Updated> Deactivate()
        {
            if (!IsActive)
            {
                return WhwerHouseError.WarehouseAlreadyInactive;
            }
            IsActive = false;
            return Result.Updated;
        }

        private static List<Error> Errors(string name, string code, string address)
        {
            var errors = new List<Error>();
            if (string.IsNullOrEmpty(name))
            {
                errors.Add(WhwerHouseError.NameIsErquired);
            }
            if (string.IsNullOrEmpty(code))
            {
                errors.Add(WhwerHouseError.CodeIsErquired);
            }
            if (string.IsNullOrEmpty(address))
            {
                errors.Add(WhwerHouseError.AddressIsErquired);
            }
            return errors;
        }

    }
}
