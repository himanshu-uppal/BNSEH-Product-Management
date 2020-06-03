using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManager.Business.Services.Interface
{
    public interface ICryptoService
    {
        string GenerateSalt();
        string EncryptPassword(string password, string salt);
    }
}
