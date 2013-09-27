using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Helpers
{
    public static class CSharpEscaper
    {
        public static string FromSQL(string userInput, bool attachResultVar)
        {
            string result = userInput;

            result = result.Replace("\"", "\\\"");

            result = result.Replace("'+@", "\" + ");
            result = result.Replace("+@", " + ");
            result = result.Replace("+'", " + \"");

            result = result.Replace(Environment.NewLine, "\\r\"+\n\"\\n");

            //replace the first and the last ' chars
            if (result.IndexOf('\'', 0, 1) > -1)
            {
                result = result.Remove(0, 1);
                result = "\"" + result;
                if (attachResultVar)
                    result = "result = " + result;
            }
            if (result.IndexOf('\'', result.Length - 1, 1) > -1)
            {
                result = result.Remove(result.Length - 1, 1);
                result += "\"";
                if (attachResultVar)
                    result += ";";
            }

            return result;
        }
    }
}