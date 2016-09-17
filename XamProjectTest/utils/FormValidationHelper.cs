

namespace XamProjectTest.utils
{
    public class FormValidationHelper
    {
       

        public static bool IsValueEmpty(string varArg)
        {
            if (varArg.Length < 1)
                return true;

            return false;
        }

        public static string ParseDateValue(string varArg)
        {
            if (varArg.Length < 1)
                return null;

            return varArg;
        }

        public static string ParseEmptyDate(string varArg)
        {
            if (varArg == null)
                return "";

            return varArg;
        }

    }
}