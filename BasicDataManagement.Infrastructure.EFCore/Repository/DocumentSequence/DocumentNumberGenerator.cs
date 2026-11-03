using _0_Framework.Application;
using System;
using System.Linq;

namespace BasicDataManagement.Infrastructure.EFCore.Repository.DocumentSequence
{
    public class DocumentNumberGenerator : IDocumentNumberGenerator
    {
        private readonly BasicDataContext _context; 

        public DocumentNumberGenerator(BasicDataContext context)
        {
            _context = context;
        }

        public string Next(string documentType, int? baseId = null)
        {
            var year = DateTime.Now.Year;
            var prefix = string.IsNullOrWhiteSpace(documentType)
                ? "DOC"
                : documentType.Trim().ToUpper();

            var last = _context.DocumentSequences
                .FirstOrDefault(x =>
                    x.DocumentType == prefix &&
                    x.Year == year &&
                    x.BaseId == baseId);

            if (last == null)
            {
                last = new DocumentSequence
                {
                    DocumentType = prefix,
                    Year = year,
                    BaseId = baseId,
                    LastValue = 1
                };
                _context.DocumentSequences.Add(last);
            }
            else
            {
                last.LastValue++;
            }

            _context.SaveChanges();
            return $"{prefix}-{year}-{last.LastValue:D6}";
        }
    }

   
}