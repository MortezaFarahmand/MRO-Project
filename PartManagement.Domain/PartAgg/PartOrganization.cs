using _0_Framework.Domain;

namespace PartManagement.Domain.PartAgg
{
    public class PartOrganization : EntityBase
    {
        public long PartId { get; private set; }
        public long OrganizationId { get; private set; }

        public bool IsOwner { get; private set; }              // مالک اصلی
        public bool CanUse { get; private set; }               // مجاز به استفاده در انبار
        public bool CanPurchase { get; private set; }          // مجاز به خرید
        public bool CanSell { get; private set; }              // مجاز به فروش
        public bool CanRepair { get; private set; }            // مجاز به تعمیر
        public bool CanLease { get; private set; }             // مجاز به اجاره دادن/گرفتن
        public bool CanShip { get; private set; }              // مجاز به حمل‌ونقل

        public string Note { get; private set; }


        private PartOrganization() { }

        public static PartOrganization Create(
            long partId,
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
            return new PartOrganization
            {
                PartId = partId,
                OrganizationId = organizationId,
                IsOwner = isOwner,
                CanUse = canUse,
                CanPurchase = canPurchase,
                CanSell = canSell,
                CanRepair = canRepair,
                CanLease = canLease,
                CanShip = canShip,
                Note = note
            };
        }

        public void UpdatePermissions(
            bool canUse, bool canPurchase, bool canSell,
            bool canRepair, bool canLease, bool canShip)
        {
            CanUse = canUse;
            CanPurchase = canPurchase;
            CanSell = canSell;
            CanRepair = canRepair;
            CanLease = canLease;
            CanShip = canShip;
        }
    }
}