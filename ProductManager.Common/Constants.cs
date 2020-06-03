using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManager.Common
{
    public class Constants
    {
        public const int DEFAULT_PAGE_NUMBER = 1;
        public const int DEFAULT_PAGE_SIZE = 10;
        public const string ProductManagerDBConnection = "ProductManagerDBConnection";


        public const string USER_ROLE_NAME = "User";
        public const string ADMIN_ROLE_NAME = "Admin";
        public const string SECRET = "XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==";

        #region Http Codes
        public const string Ok = "200";
        public const string NoContent = "204";
        public const string BadRequest = "400";
        public const string UnAuthorized = "401";
        public const string NotFound = "404";
        public const string InternalServerError = "500";
        public const string Conflict = "409";
        public const string UnprocessableEntity = "422";
        #endregion
    }
}
