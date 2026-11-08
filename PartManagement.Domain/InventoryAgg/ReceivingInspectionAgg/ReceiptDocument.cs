using _0_Framework.Domain;
using PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg.Enum;

namespace PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg
{
    public class ReceiptDocument : EntityBase
    {
        public long ReceiptId { get; private set; }
        public long? ReceiptLineId { get; private set; }
        public ReceiptDocumentType DocumentType { get; private set; }
        public string DocumentNumber { get; private set; }
        public DateTime? DocumentDate { get; private set; }
        public string FilePath { get; private set; }
        public string Notes { get; private set; }

        protected ReceiptDocument() { }

        public ReceiptDocument(
            long receiptId,
            long? receiptLineId,
            ReceiptDocumentType documentType,
            string documentNumber,
            DateTime? documentDate,
            string filePath,
            string notes)
        {
            ReceiptId = receiptId;
            ReceiptLineId = receiptLineId;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            DocumentDate = documentDate;
            FilePath = filePath;
            Notes = notes;
        }

        public void Update(
            ReceiptDocumentType documentType,
            string documentNumber,
            DateTime? documentDate,
            string filePath,
            string notes)
        {
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            DocumentDate = documentDate;
            FilePath = filePath;
            Notes = notes;
        }
    }
}