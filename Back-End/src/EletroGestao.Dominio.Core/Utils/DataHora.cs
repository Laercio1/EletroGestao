using System.Globalization;
using TimeZoneConverter;

namespace EletroGestao.Dominio.Core.Utils
{
    public static class DataHora
    {
        public static TimeZoneInfo RetornarTimeZoneInfo(string zone = "E. South America Standard Time")
        {
            TimeZoneInfo timezone;
            try
            {
                timezone = TimeZoneInfo.FindSystemTimeZoneById(zone);
            }
            catch
            {
                timezone = TimeZoneInfo.FindSystemTimeZoneById(TZConvert.WindowsToIana(zone));
            }

            return timezone;
        }

        public static DateTimeOffset ReturnTimeOnServer(string clientString)
        {
            string format = @"dd/MM/yyyy H:m:s zzz";
            TimeSpan serverOffset = TimeZoneInfo.Local.GetUtcOffset(DateTimeOffset.Now);

            try
            {
                CultureInfo ptBR = new CultureInfo("pt-BR");
                DateTimeOffset clientTime = DateTimeOffset.ParseExact(clientString, format, ptBR);
                DateTimeOffset serverTime = clientTime.ToOffset(serverOffset);
                return serverTime;
            }
            catch (FormatException)
            {
                return DateTimeOffset.MinValue;
            }
        }
    }
}
