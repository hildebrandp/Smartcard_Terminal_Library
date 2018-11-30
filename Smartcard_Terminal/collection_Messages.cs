using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartcard_Terminal
{
    public class collection_Messages
    {
        public int _id;
        public int _code;
        public string _message;

        public collection_Messages(int _id, int _code, string message)
        {
            this._id = _id;
            this._code = _code;
            this._message = message;
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
