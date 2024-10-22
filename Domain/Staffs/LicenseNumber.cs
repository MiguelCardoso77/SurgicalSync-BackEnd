using System;
using System.Text.RegularExpressions;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    public class LicenseNumber : EntityId
    {
        // Define o formato do número de licença usando uma expressão regular
        private static readonly Regex LicenseNumberFormat = new Regex(@"^(N|D|O)\d{4}\d{5}$");

        // Construtor que recebe o valor do número de licença e valida o formato
        public LicenseNumber(string value) : base(value)
        {
            if (!IsValidFormat(value))
            {
                throw new ArgumentException("Invalid license number format. It must follow the format '(N | D | O)yyyynnnnn'.");
            }
        }

        // Método para verificar se o valor segue o formato correto
        private static bool IsValidFormat(string value)
        {
            return LicenseNumberFormat.IsMatch(value);
        }

        // Criação a partir de string, com validação do formato
        protected override object createFromString(string text)
        {
            if (!IsValidFormat(text))
            {
                throw new ArgumentException("Invalid license number format. It must follow the format '(N | D | O)yyyynnnnn'.");
            }
            return text;
        }

        // Representação do LicenseNumber como string
        public override string AsString()
        {
            return Value;
        }

        // Sobrescreve ToString para garantir a saída como string
        public override string ToString()
        {
            return Value;
        }
    }
}