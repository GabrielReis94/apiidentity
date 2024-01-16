using System.Runtime.CompilerServices;

namespace MicroserviceIdentityAPI.Domain.Domain.Services.Extensions
{
    public static class Extension
    {
        public static string RemoveDotsDashBars(this string value)
        {
            var newValue = string.Empty;

            if(!string.IsNullOrEmpty(value))
                newValue = value.Replace(".", "").Replace("/", "").Replace("-", "");
            
            return newValue;
        }

        public static string RemoveDashParentheses(this string value)
        {
            var newValue = string.Empty;

            if(!string.IsNullOrEmpty(value))
                newValue = value.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Replace("+", "");
            
            return newValue;
        }

        public static bool NotIsNullOrEmpty(this string value)
        {
            if(!string.IsNullOrEmpty(value))
                return true;
            
            return false;
        }

        public static bool IsValidCpf(this string cpf)
        {
            int[] mult1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf, digit, rest;
            // string digit;
            int sum;
            //int rest;

            cpf = cpf.Trim().RemoveDotsDashBars();
            // cpf = cpf.RemoveDotsDashBars();

            if(cpf.Length != 11)
                return false;
            
            tempCpf = cpf.Substring(0, 9);

            sum = CalcSum(9, mult1, tempCpf);
            rest = CalcRest(sum);

            digit = rest;

            tempCpf = tempCpf + digit;

            sum = CalcSum(10, mult2, tempCpf);
            rest = CalcRest(sum);

            digit += rest;

            return cpf.EndsWith(digit);
        }

        public static bool IsValidCnpj(this string cnpj)
        {
            int[] mult1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum;
            string digit, tempCnpj, rest;

            cnpj = cnpj.Trim().RemoveDotsDashBars();

            if(cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);

            sum = CalcSum(12, mult1, tempCnpj);
            rest = CalcRest(sum);

            digit = rest;

            tempCnpj = tempCnpj + digit;

            sum = CalcSum(13, mult2, tempCnpj);
            rest = CalcRest(sum);

            digit += rest;

            return cnpj.EndsWith(digit);
        }

        private static int CalcSum(int length, int[] mult, string tempDoc)
        {
            int sum = 0;

            for (int i = 0; i < length; i++)
            {
                sum += int.Parse(tempDoc[i].ToString()) * mult[i];
            }

            return sum;
        }

        private static string CalcRest(int sum)
        {
            int rest;
            int mode = (sum % 11);

            if(mode < 2)
                rest = 0;
            else
                rest = 11 - mode;

            return rest.ToString();
        }
    }
}