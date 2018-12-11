using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class Object for received Bluetooth Messages
/// </summary>
namespace Smartcard_Terminal
{
    public class collection_Messages
    {
        public int _id;
        public int _code;
        public string _message;

        /// <summary>
        /// Constuctor for Creating Message Object
        /// </summary>
        /// <param name="_id">ID of Message</param>
        /// <param name="_code">Code of the Message</param>
        /// <param name="message">Received Message</param>
        public collection_Messages(int _id, int _code, string message)
        {
            this._id = _id;
            this._code = _code;
            this._message = message;
        }

        /// <summary>
        /// Getter and Setter Method for ID
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Getter and Setter Method for Code
        /// </summary>
        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        /// Getter and Setter Method for Message
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
