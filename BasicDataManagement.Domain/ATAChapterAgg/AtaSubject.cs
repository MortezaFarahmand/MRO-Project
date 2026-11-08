namespace BasicDataManagement.Domain.ATAChapterAgg
{
    public class AtaSubject
    {
        public int Id { get; private set; }
        public int AtaSectionId { get; private set; }
        public AtaSection Section { get; private set; }

        public string Code { get; private set; }          // "01"
        public string FullCode { get; private set; }      // "32-41-01"
        public string TitleEn { get; private set; }
        public string? TitleFa { get; private set; }

        // برای Cross-Reference بین AMM / WDM / IPC
        public string? WdmChapterSection { get; private set; }
        public string? IpcChapterSection { get; private set; }
    }
}
