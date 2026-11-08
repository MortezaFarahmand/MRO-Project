using PartManagement.Domain.StockAgg.StoreAgg.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartManagement.Application.Contracts.Stock.Commands
{
    public class UpdateStoreReceivingPolicyCommand
    {
        public long StoreId { get; set; }
        public InspectionMode InspectionMode { get; set; }
        public int? InspectionDeadlineHours { get; set; }
        public bool AllowOneStepReceiveAndInspect { get; set; }
        public long? DefaultReceivingHoldZoneId { get; set; }
    }
}
