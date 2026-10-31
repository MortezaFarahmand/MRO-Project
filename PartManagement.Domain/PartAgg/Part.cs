using _0_Framework.Domain;
using PartManagement.Domain.PartAgg.Enum;
using PartManagement.Domain.PartAgg.Events;

namespace PartManagement.Domain.PartAgg
{
    public class Part : AggregateRoot
    {
        // ========================================
        // ۱. Properties (فیلدها)
        // ========================================
        public string PartNumber { get; private set; }
        public string Revision { get; private set; }
        public string Description { get; private set; }
        public string Keyword { get; private set; }
        public string Specification { get; private set; }

        public PartControlType ControlType { get; private set; }
        public bool IsEngineRelated { get; private set; }
        public bool IsUncontrolled { get; private set; }

        public long? OwnerOrganizationId { get; private set; }
        public bool IsGlobal { get; private set; }

        public long? PartTypeId { get; private set; }
        public long? EquipmentTypeId { get; private set; }
        public long? ClassificationId { get; private set; }
        public long? PurchaseUnitId { get; private set; }
        public long? StorageUnitId { get; private set; }
        public long? StockPolicyId { get; private set; }

        public decimal? FloatQuantity { get; private set; }
        public decimal? AveragePrice { get; private set; }
        public string Note { get; private set; }

        // Collections
        public ICollection<PartManufacturer> Manufacturers { get; private set; } = new List<PartManufacturer>();
        public ICollection<PartUtilisation> Utilisations { get; private set; } = new List<PartUtilisation>();
        public ICollection<PartOrganization> Organizations { get; private set; } = new List<PartOrganization>();
        public ICollection<PartHierarchy> Children { get; private set; } = new List<PartHierarchy>();
        public ICollection<PartHierarchy> Parents { get; private set; } = new List<PartHierarchy>();


        // ========================================
        // ۲. Constructor خصوصی (برای EF)
        // ========================================
        private Part() { }


        // ========================================
        // ۳. Factory Method (ساخت)
        // ========================================
        public static Part Create(
            string partNumber,
            string description,
            PartControlType controlType,
            bool isGlobal,
            long? ownerOrganizationId,
            long createdBy)
        {
            if (string.IsNullOrWhiteSpace(partNumber))
                throw new DomainException("شماره قطعه الزامی است");

            if (!isGlobal && ownerOrganizationId == null)
                throw new DomainException("قطعه غیرجهانی باید سازمان مالک داشته باشد");

            var part = new Part
            {
                PartNumber = partNumber.Trim().ToUpper(),
                Description = description?.Trim(),
                ControlType = controlType,
                IsGlobal = isGlobal,
                OwnerOrganizationId = isGlobal ? null : ownerOrganizationId,
                CreatedBy = createdBy
            };

            return part;
        }

        public void RaiseCreatedEvent()
        {
            AddDomainEvent(new PartCreatedEvent(
                Id, PartNumber, Description, IsGlobal, OwnerOrganizationId));
        }
        // ========================================
        // ۴. متدهای ویرایش اصلی
        // ========================================
        public void Edit(
            string description,
            string keyword,
            string specification,
            string note,
            long modifiedBy)
        {
            Description = description?.Trim();
            Keyword = keyword?.Trim();
            Specification = specification?.Trim();
            Note = note?.Trim();

            ModifiedAt = DateTime.UtcNow;
            ModifiedBy = modifiedBy;

            AddDomainEvent(new PartEditedEvent(Id, PartNumber));
        }

        public void MakeGlobal(long modifiedBy)
        {
            if (IsGlobal)
                return;

            IsGlobal = true;
            OwnerOrganizationId = null;

            ModifiedAt = DateTime.UtcNow;
            ModifiedBy = modifiedBy;

            AddDomainEvent(new PartMadeGlobalEvent(Id, PartNumber));
        }
        // ========================================
        // ۵. مدیریت Manufacturer
        // ========================================
        public void AddManufacturer(long manufacturerId, bool isPrimary, string note = null)
        {
            if (Manufacturers.Any(x => x.ManufacturerId == manufacturerId))
                throw new DomainException("این سازنده قبلاً برای این قطعه ثبت شده است");

            if (isPrimary)
            {
                foreach (var manufacturer in Manufacturers)
                    manufacturer.SetAsSecondary();
            }

            var partManufacturer = PartManufacturer.Create(Id, manufacturerId, isPrimary, note);
            Manufacturers.Add(partManufacturer);
        }

        public void RemoveManufacturer(long manufacturerId)
        {
            var manufacturer = Manufacturers.FirstOrDefault(x => x.ManufacturerId == manufacturerId);
            if (manufacturer == null)
                throw new DomainException("سازنده مورد نظر یافت نشد");

            Manufacturers.Remove(manufacturer);
        }

        public void SetPrimaryManufacturer(long manufacturerId)
        {
            var manufacturer = Manufacturers.FirstOrDefault(x => x.ManufacturerId == manufacturerId);
            if (manufacturer == null)
                throw new DomainException("سازنده مورد نظر یافت نشد");

            foreach (var item in Manufacturers)
                item.SetAsSecondary();

            manufacturer.SetAsPrimary();
        }
        // ========================================
        // ۶. مدیریت Hierarchy (فرزندان)
        // ========================================
        public void AddChild(
            long childPartId,
            decimal quantity,
            int level,
            PartRelationType relationType)
        {
            if (childPartId == Id)
                throw new DomainException("قطعه نمی‌تواند فرزند خودش باشد");

            if (Children.Any(x => x.ChildPartId == childPartId))
                throw new DomainException("این قطعه قبلاً به عنوان فرزند ثبت شده است");

            if (quantity <= 0)
                throw new DomainException("تعداد باید بزرگ‌تر از صفر باشد");

            var hierarchy = PartHierarchy.Create(Id, childPartId, quantity, level, relationType);
            Children.Add(hierarchy);

            AddDomainEvent(new PartHierarchyChangedEvent(Id));
        }

        public void RemoveChild(long childPartId)
        {
            var child = Children.FirstOrDefault(x => x.ChildPartId == childPartId);
            if (child == null)
                throw new DomainException("فرزند مورد نظر یافت نشد");

            Children.Remove(child);
            AddDomainEvent(new PartHierarchyChangedEvent(Id));
        }

        public void UpdateChildQuantity(long childPartId, decimal newQuantity)
        {
            var child = Children.FirstOrDefault(x => x.ChildPartId == childPartId);
            if (child == null)
                throw new DomainException("فرزند مورد نظر یافت نشد");

            child.UpdateQuantity(newQuantity);
            AddDomainEvent(new PartHierarchyChangedEvent(Id));
        }
        // ========================================
        // ۷. مدیریت مجوز سازمان‌ها
        // ========================================
        public void AddOrganizationPermission(
            long organizationId,
            bool isOwner,
            bool canUse = true,
            bool canPurchase = false,
            bool canSell = false,
            bool canRepair = false,
            bool canLease = false,
            bool canShip = false,
            string note = null)
        {
            if (Organizations.Any(x => x.OrganizationId == organizationId))
                throw new DomainException("این سازمان قبلاً برای این قطعه ثبت شده است");

            var permission = PartOrganization.Create(
                Id,
                organizationId,
                isOwner,
                canUse,
                canPurchase,
                canSell,
                canRepair,
                canLease,
                canShip,
                note);

            Organizations.Add(permission);

            AddDomainEvent(new PartOrganizationPermissionChangedEvent(Id, organizationId));
        }

        public void UpdateOrganizationPermission(
            long organizationId,
            bool canUse,
            bool canPurchase,
            bool canSell,
            bool canRepair,
            bool canLease,
            bool canShip)
        {
            var permission = Organizations.FirstOrDefault(x => x.OrganizationId == organizationId);
            if (permission == null)
                throw new DomainException("مجوز این سازمان یافت نشد");

            permission.UpdatePermissions(canUse, canPurchase, canSell, canRepair, canLease, canShip);

            AddDomainEvent(new PartOrganizationPermissionChangedEvent(Id, organizationId));
        }

        public void RemoveOrganizationPermission(long organizationId)
        {
            var permission = Organizations.FirstOrDefault(x => x.OrganizationId == organizationId);
            if (permission == null)
                throw new DomainException("مجوز این سازمان یافت نشد");

            if (permission.IsOwner)
                throw new DomainException("نمی‌توان مجوز سازمان مالک را حذف کرد");

            Organizations.Remove(permission);

            AddDomainEvent(new PartOrganizationPermissionChangedEvent(Id, organizationId));
        }
        // ========================================
        // ۸. متدهای کمکی
        // ========================================
        public void SetFloatQuantity(decimal? floatQuantity)
        {
            if (floatQuantity.HasValue && floatQuantity < 0)
                throw new DomainException("مقدار Float نمی‌تواند منفی باشد");

            FloatQuantity = floatQuantity;
        }

        public void SetAveragePrice(decimal? averagePrice)
        {
            if (averagePrice.HasValue && averagePrice < 0)
                throw new DomainException("قیمت میانگین نمی‌تواند منفی باشد");

            AveragePrice = averagePrice;
        }

        public void SetNote(string note)
        {
            Note = note?.Trim();
        }












    }
    }
