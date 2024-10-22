using System;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Staffs;

namespace DDDNetCore.Application.Services
{
    public class LicenseNumberService
    {
        // Variável estática para rastrear o último número gerado
        private static int _lastGeneratedNumber = 10002; // Começa em 10002 para garantir 5 dígitos

        public static LicenseNumber GenerateLN(StaffType staffType)
        {
            // Define o prefixo com base no tipo de funcionário
            char prefix = staffType switch
            {
                StaffType.Doctor => 'D',
                StaffType.Nurse => 'N',
                _ => 'O' // Para outros tipos
            };

            // Obtém o ano atual
            string year = DateTime.Now.Year.ToString();

            // Gera um número crescente de 5 dígitos
            _lastGeneratedNumber = (_lastGeneratedNumber + 1) % 100000; // Incrementa e garante que permaneça em 5 dígitos
            if (_lastGeneratedNumber < 10000) // Se passar de 99999, reinicia
            {
                _lastGeneratedNumber = 10000;
            }

            // Formata o número da licença
            string licenseNumberValue = $"{prefix}{year}{_lastGeneratedNumber:D5}"; // Garante que seja sempre 5 dígitos

            // Retorna um novo objeto LicenseNumber
            return new LicenseNumber(licenseNumberValue);
        }
    }
}