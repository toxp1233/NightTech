using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NightTech.Application.common
{
    public class Base64Helper
    {
        public static string ToBase64Url(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            return Base64UrlEncoder.Encode(bytes);
        }

        public static string FromBase64Url(string input)
        {
            var bytes = Base64UrlEncoder.DecodeBytes(input);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
