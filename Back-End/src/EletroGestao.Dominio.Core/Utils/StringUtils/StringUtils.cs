using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using OfficeOpenXml;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace EletroGestao.Dominio.Core.Utils.StringUtils
{
    public static class StringUtils
    {
        private static string _numero;

        public static string ApenasNumeros(this string str)//, string input)
        {
            return new string(str.Where(char.IsDigit).ToArray());
        }

        public static void AddDigito(string digito)
        {
            _numero = string.Concat(_numero, digito);
        }

        public static bool ValidaCPF(this string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma = 0;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace("_", string.Empty).Replace(".", string.Empty).Replace("-", string.Empty);

            if (cpf.Length != 11)
                return false;

            // Caso coloque todos os numeros iguais
            switch (cpf)
            {
                case "11111111111":
                    return false;
                case "00000000000":
                    return false;
                case "22222222222":
                    return false;
                case "33333333333":
                    return false;
                case "44444444444":
                    return false;
                case "55555555555":
                    return false;
                case "66666666666":
                    return false;
                case "77777777777":
                    return false;
                case "88888888888":
                    return false;
                case "99999999999":
                    return false;
            }

            tempCpf = cpf.Substring(0, 9);
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static bool ValidaCNPJ(this string vrCNPJ)
        {

            string CNPJ = vrCNPJ.Replace(".", string.Empty).Replace("/", string.Empty).Replace("-", string.Empty);
            int[] resultado = new int[2] { 0, 0 };
            bool[] CNPJOk = new bool[2] { false, false };
            int[] soma = new int[2] { 0, 0 };
            int[] digitos = new int[14];

            int nrDig;
            string ftmt = "6543298765432";

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (
                         resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));
                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidaCNPJouCPF(this string vrCNPJouCPF)
        {
            return ValidaCNPJ(vrCNPJouCPF.ApenasNumeros()) || ValidaCPF(vrCNPJouCPF.ApenasNumeros());
        }

        public static bool validaEmail(this string email)
        {
            string strModelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (System.Text.RegularExpressions.Regex.IsMatch(email, strModelo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string RemoveQuebraDeLinhas(this string texto)
        {
            return texto.Replace("\n", string.Empty).Replace("\r", string.Empty);
        }

        public static string RemoveTodoEspaco(this string Texto)
        {
            try
            {
                Texto = Texto.Trim();

                while (Texto.Contains(" "))
                {
                    Texto = Texto.Remove(Texto.IndexOf(' '), 1);
                }

                return Texto;
            }
            catch
            {
                return "";
            }
        }

        public static string RemoveExcessoEspaco(this string texto)
        {
            try
            {
                if (texto == null)
                    return "";

                texto = texto.Trim();

                while (texto.Contains("  "))
                {
                    texto = texto.Replace("  ", " ");
                }

                return texto;
            }
            catch
            {
                return "";
            }
        }

        public static string RemoveAcento(this string texto)
        {
            try
            {
                if (texto == null)
                    return "";

                string s = texto.Normalize(NormalizationForm.FormD);

                StringBuilder sb = new StringBuilder();

                for (int k = 0; k < s.Length; k++)
                {
                    UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                    if (uc != UnicodeCategory.NonSpacingMark)
                    {
                        sb.Append(s[k]);
                    }
                }
                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        public static string RemoveZerosEsquerda(this string texto)
        {
            while (texto.StartsWith("0"))
            {
                texto = texto.Remove(0, 1);
            }

            return texto;
        }

        public static string RemoveSimbolos(this string Texto)
        {
            try
            {
                if (Texto == null)
                    return "";

                string Retorno = string.Empty;

                foreach (char item in Texto)
                {
                    if (!char.IsSymbol(item) && item != '&' && item != 'º' && item != 'ª')
                    {
                        Retorno += item.ToString();
                    }
                }

                return Retorno;
            }
            catch
            {
                return "";
            }
        }

        public static string RemovePontuacao(this string Texto)
        {
            string Retorno = string.Empty;

            foreach (char item in Texto)
            {
                if (!char.IsPunctuation(item))
                {
                    Retorno += item.ToString();
                }
            }

            return Retorno;
        }

        public static string ApenasNumeros(this string texto, int tamanho = 0, bool zerosAEsquerda = true)
        {
            string retorno = "";
            try
            {
                try
                {
                    retorno = texto.Where(char.IsNumber).Aggregate(string.Empty, (current, item) => current + item.ToString());
                }
                catch
                {
                    retorno = string.Empty;
                }

                if (tamanho > 0)
                {
                    if (retorno.Length > tamanho)
                        retorno = retorno.Substring(0, tamanho);

                    while (retorno.Length < tamanho)
                    {
                        if (zerosAEsquerda)
                            retorno = ("0" + retorno);
                        else
                            retorno = (retorno + "0");
                    }
                }
            }
            catch
            {

            }

            return retorno;
        }

        public static string ConvertHexToString(string HexValue)
        {
            string StrValue = "";
            while (HexValue.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }
            return StrValue;
        }

        public static string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }

        public static string ToHexadecimal(this string data)
        {
            var sHex = "";
            while (data.Length > 0)
            {
                string sValue = Conversion.Hex(Strings.Asc(data.Substring(0, 1)));
                data = data.Substring(1, data.Length - 1);
                sHex = sHex + sValue;
            }

            return sHex.ToLower();
        }

        public static bool DataValida(object valor)
        {
            DateTime dataValida;
            return DateTime.TryParse(valor.ToString(), out dataValida);
        }

        public static byte[] ImageToByteArray(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static Image ByteArrayToImage(this byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static string GeraNumeroAleatorio8Digitos()
        {
            string primeiraFaixa = default(string), segundaFaixa = default(string);
            Random rand = new Random();

            primeiraFaixa = rand.Next(1, 9).ToString("0") + rand.Next(999).ToString("000");
            segundaFaixa = rand.Next(9999).ToString("0000");

            //certifica que não vai repetir a numeração das faixas
            while (primeiraFaixa.Equals(segundaFaixa))
                segundaFaixa = rand.Next(9999).ToString("0000");

            return primeiraFaixa + segundaFaixa;
        }

        public static string gerarHashCSRT(string ChaveAcesso, string CSRT, string idCSRT)
        {
            CSRT = CSRT.Trim();
            idCSRT = idCSRT.Trim();
            ChaveAcesso = ChaveAcesso.Trim();

            string passo1 = ChaveAcesso + CSRT;

            string passo2 = passo1.GetHash("SHA1");

            string passo3 = passo2.Criptografa();

            return passo3;
        }

        public static List<Planilha> LerPlanilha(IFormFile arquivo)
        {
            var pedidos = new List<Planilha>();

            using var stream = new MemoryStream();
            arquivo.CopyTo(stream);
            stream.Position = 0;

            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets[0];

            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var pedido = LerLinhaPlanilha(worksheet, row);
                pedidos.Add(pedido);
            }

            return pedidos;
        }

        private static Planilha LerLinhaPlanilha(ExcelWorksheet worksheet, int row)
        {
            return new Planilha
            {
                CPF_CNPJ = ObterValorCelula(worksheet, row, 1),
                NomeRazaoSocial = ObterValorCelula(worksheet, row, 2),
                CEP = ObterValorCelula(worksheet, row, 3),
                Produto = ObterValorCelula(worksheet, row, 4),
                NumeroPedido = ObterValorCelula(worksheet, row, 5),
                Data = ObterValorCelula(worksheet, row, 6)
            };
        }

        private static string ObterValorCelula(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            return cellValue != null ? cellValue.ToString() : string.Empty;
        }
    }

    public class Planilha
    {
        public string CPF_CNPJ { get; set; }
        public string NomeRazaoSocial { get; set; }
        public string CEP { get; set; }
        public string Produto { get; set; }
        public string NumeroPedido { get; set; }
        public string Data { get; set; }
    }
}
