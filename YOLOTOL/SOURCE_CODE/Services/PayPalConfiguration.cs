using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOLOTOL.Services
{
    public class PayPalConfiguration
    {
        public readonly static string ClientId;
        public readonly static string ClientSecret;

        static PayPalConfiguration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        public static Dictionary<string, string> GetConfig()
        {
            return new Dictionary<string, string> {

                { "clientId", "AZSkGGhQfUdbsOZZYHt-RpQwo7h-LOb8zGi70dSoOYQA4l9M8YqXfn9Aj5v2NPJWYnWqAzQh5CFcJha7"},
                { "clientSecret", "ENJcgNefzoyKZLERfUczbBwd9ApkT3fVuz2d_-VuC7UOloUdW2JKFa55IPCrUoTpZSmjNd5Wr4nFF04U" }
            };
        }

        private static string GetAccssToken()
        {
            string accessToken = new OAuthTokenCredential
                (ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        public static APIContext GetAPIContext(string accessToken = "")
        {
            var apiContext = new APIContext(string.IsNullOrEmpty(accessToken) ?
                GetAccssToken() : accessToken);
            apiContext.Config = GetConfig();

            return apiContext;
        }
    }
}
