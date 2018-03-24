using System;

namespace MOS
{
    public static class StringExtension
    {
        public static bool IsHex(this String str)
        {
                return System.Text.RegularExpressions.Regex.IsMatch(str, @"\A\b[0-9a-fA-F]+\b\Z");  // patikrint ar tai hex
        }

        public static int ToHex(this String str)
        {
            return Int32.Parse(str, System.Globalization.NumberStyles.HexNumber);  // šita naudojam visada, paverčia int į string(jį interpretuoja kaip hex)
        }

        


        public static int TwoLastSymbolsToHex(this String str)
        {
            return Int32.Parse(str.Substring(2, 2), System.Globalization.NumberStyles.HexNumber); // čia dėl ptr registro
        }
    }



    public static class IntegerExtension
    {
        public static string ToHex(this int a)
        {
            return $"{a:x}"; // paverčia int į hex(jis int laiko kaip dešimtainį skaičių)
        }
    }
}
