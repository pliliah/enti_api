using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace enti_api.Controllers
{
    public class LoginController : ApiController
    {       
        // POST: api/Login
        public Models.ReturnValue<string> Post([FromBody]Models.Login login)
        {
            byte[] nonce = Convert.FromBase64String(login.nonce);
            string decodedNonce = Encoding.UTF8.GetString(nonce);

            // given, a password in a string
            string password = "123123";

            // byte array representation of that string
            byte[] encodedPassword = new UTF8Encoding().GetBytes(decodedNonce + login.date + password);

            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();

            var encodedBytes = System.Text.Encoding.UTF8.GetBytes(encoded);
            string encodedToBase64 = Convert.ToBase64String(encodedBytes);

            if (encodedToBase64 == login.digest)
            {
                //login successfull
                return new Models.ReturnValue<string>(Models.Codes.Accepted, "User logged in successfully!");
            }
            else
            {
                //login not successfull
                return new Models.ReturnValue<string>(Models.Codes.Unauthorized, "Credentials not valid. Login unsuccessfull!");
            }
        }

        //// PUT: api/Login/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Login/5
        //public void Delete(int id)
        //{
        //}
    }
}
