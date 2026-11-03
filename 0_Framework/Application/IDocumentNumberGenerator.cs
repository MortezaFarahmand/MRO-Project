namespace _0_Framework.Application
{
    public interface IDocumentNumberGenerator
    {
        /// <summary>
        /// مثال: documentType = "SM" → SM-2026-000123
        /// </summary>
        string Next(string documentType, int? baseId = null);
    }
}