using System;
using System.Collections.Generic;
using System.Text;

namespace OOP21pbm_csharp.AlessandroStefani
{
    class ProfileCredentials
    {
        private string _name { get; }
        private string _surname { get; }
        private string _fc { get; }
        private string _eMail { get; }
        private IPassword _password;

        public ProfileCredentials(string name, string surname, string fc, string eMail, IPassword password)
        {
            _name = name;
            _surname = surname;
            _fc = fc;
            _eMail = eMail;
            _password = password;
        }

        public string GetPassword() => _password.GetPassword();

        public void updatePassword(IPassword newPassword) => _password = newPassword;
    }
}
