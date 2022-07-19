using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasarERPBanking.Models
{
    public class AlertMessage
    {
        private string _message = null;
        public string Message
        {
            set
            {
                _message = value;
            }
        }

        public bool HayMensaje()
        {
            return _message != null;
        }

        public string getMessage()
        {
            string res = _message;
            _message = null;
            return res;
        }
    }
}