using System;
using System.Text.RegularExpressions;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    public class LicenseNumber : EntityId
    {
        private static readonly Regex LicenseNumberFormat = new Regex(@"^(N|D|O)\d{4}\d{5}$");

        public LicenseNumber(string value) : base(value)
        {
            if (!IsValidFormat(value))
            {
                throw new ArgumentException("Invalid license number format. It must follow the format '(N | D | O)yyyynnnnn'.");
            }
        }

        private static bool IsValidFormat(string value)
        {
            return LicenseNumberFormat.IsMatch(value);
        }

        protected override object createFromString(string text)
        {
            if (!IsValidFormat(text))
            {
                throw new ArgumentException("Invalid license number format. It must follow the format '(N | D | O)yyyynnnnn'.");
            }
            return text;
        }

        public override string AsString()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}