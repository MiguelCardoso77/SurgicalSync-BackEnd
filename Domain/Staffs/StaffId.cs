using System;
using System.Text.RegularExpressions;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    public class StaffId : EntityId
    {
        // Define o formato do StaffId usando uma expressão regular
        private static readonly Regex StaffIdFormat = new Regex(@"^(N|D|O)\d{4}\d{5}$");

        // Construtor que recebe o valor do StaffId e valida o formato
        public StaffId(string value) : base(value)
        {
            if (!IsValidFormat(value))
            {
                throw new ArgumentException("Invalid staff Id format. It must follow the format '(N | D | O)yyyynnnnn'.");
            }
        }

        // Método para verificar se o valor segue o formato correto
        private static bool IsValidFormat(string value)
        {
            return StaffIdFormat.IsMatch(value);
        }

        // Criação a partir de string, com validação do formato
        protected override object createFromString(string text)
        {
            if (!IsValidFormat(text))
            {
                throw new ArgumentException("Invalid staff Id format. It must follow the format '(N | D | O)yyyynnnnn'.");
            }
            return text;
        }

        // Representação do StaffId como string
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