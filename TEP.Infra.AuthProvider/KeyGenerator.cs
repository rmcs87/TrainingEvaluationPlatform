using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;

namespace TEP.Infra.AuthProvider
{
    public static class KeyGenerator
    {
        private static RandomNumberGenerator Rng = RandomNumberGenerator.Create();        
        public static SecurityKey Loadkey(string MyJwkLocation)
        {
            if (File.Exists(MyJwkLocation))
                return JsonSerializer.Deserialize<JsonWebKey>(File.ReadAllText(MyJwkLocation));

            var newKey = CreateJWK();
            File.WriteAllText(MyJwkLocation, JsonSerializer.Serialize(newKey));
            return newKey;
        }

        private static JsonWebKey CreateJWK()
        {
            var symetricKey = new HMACSHA256(GenKey(64));
            var jwk = JsonWebKeyConverter.ConvertFromSymmetricSecurityKey(new SymmetricSecurityKey(symetricKey.Key));
            jwk.KeyId = Base64UrlEncoder.Encode(GenKey(16));
            return jwk;
        }

        private static byte[] GenKey(int bytes)
        {
            var data = new byte[bytes];
            Rng.GetBytes(data);
            return data;
        }
    }
}
