using _0_Framework.Domain;

namespace PartManagement.Domain.ValueObjects
{
    public class PartNumber : ValueObject
    {
        public string Value { get; }

        private PartNumber(string value)
        {
            Value = value;
        }

        public static PartNumber Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("شماره قطعه نمی‌تواند خالی باشد");

            value = value.Trim().ToUpper();

            // قوانین ساده ATA (قابل گسترش)
            if (value.Length > 32)
                throw new DomainException("طول شماره قطعه بیش از حد مجاز است");

            return new PartNumber(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
