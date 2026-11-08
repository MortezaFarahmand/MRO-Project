namespace PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg.Enum
{
    public enum ReceiptDocumentType
    {
        CofC = 1,
        CAAIRI_Form1 = 2,
        EASA_Form1 = 3,
        FAA_8130 = 4,
        TCCA_Form1 = 5,
        DeliveryNote = 6,
        Invoice = 7,
        MaterialCert = 8,
        ShippingCart = 9,
        ServiceableTag = 10,
        JAA_Form1 = 11,        // before 28.Sep.2004
        Other = 99
    }
}