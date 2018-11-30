using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Smartcard_Terminal
{
    public delegate void receiveMessageCallback(collection_Messages recMessage);

    public class Smartcard_Terminal : Form
    {
        private Helper_Smartcard_ResponseCode sc_Response_Code;
        private Helper_Bluetooth bt_Connection;
        private Helper_QR_Code qr_Code;
        private CryptLib crypt_AES;
        private diffieHellman dh_Helper;

        private static String BT_connectionState { get; set; }
        private static Boolean BT_is_Connected { get; set; }
        private Boolean encryptData = false;

        public delegate void newMessageHandler(object myObject, newMessageEventArgs myArgs);
        public event newMessageHandler newMessagereceived;

        public Smartcard_Terminal()
        {
            sc_Response_Code = new Helper_Smartcard_ResponseCode();
            bt_Connection = new Helper_Bluetooth(this, new receiveMessageCallback(receiveMessage));
            qr_Code = new Helper_QR_Code(this);
            crypt_AES = new CryptLib();
            dh_Helper = new diffieHellman();

        }

        public Image Gen_QRCode(Boolean encrypt, int code)
        {
            encryptData = encrypt;
            return qr_Code.genQRCode(encrypt, code);
        }

        public string Get_public_p()
        {
            return dh_Helper.get_public_p();
        }

        public string Get_public_g()
        {
            return dh_Helper.get_public_g();
        }

        public string Get_public_A()
        {
            return dh_Helper.get_public_A();
        }

        public void Set_public_B(String pub_B)
        {
            dh_Helper.set_public_B(pub_B);
        }

        public string Get_Shared_Secret()
        {
            return dh_Helper.get_Shared_Secret();
        }

        public Boolean Get_encryption_state()
        {
            return encryptData;
        }

        public Boolean startBluetoothCon()
        {
            return bt_Connection.StartBTListener();
        }

        public void Stop_BT_Connection()
        {
            bt_Connection.StopBTConnection();
        }

        public string Get_BT_Address()
        {
            return bt_Connection.Get_BT_Device();
        }

        public string Get_BT_Name()
        {
            return bt_Connection.Get_BT_Name();
        }

        public string Get_BT_ConnectionState()
        {
            return BT_connectionState;
        }

        public Boolean Get_BT_is_Connected()
        {
            return BT_is_Connected;
        }

        public Boolean open_BT_Connection()
        {
            return bt_Connection.openConnection();
        }

        public collection_Messages TranceiveData(int code, string message)
        {
            if (BT_is_Connected)
            {
                return bt_Connection.SendandReceiveMessage(code, message);
            }
            else
            {
                return null;
            }
        }

        private void receiveMessage(collection_Messages recMessage)
        {
            //Console.WriteLine("SC_Terminal: " + recMessage.Message);

            newMessageEventArgs myArgs = new newMessageEventArgs(recMessage._code, recMessage._message);
            newMessagereceived?.Invoke(this, myArgs);


            switch (recMessage.Code)
            {
                case 0:
                    BT_connectionState = recMessage.Message;
                    break;
                case 1:
                    BT_connectionState = recMessage.Message;
                    break;
                case 2:
                    BT_connectionState = recMessage.Message;
                    BT_is_Connected = true;
                    break;
                case 3:
                    BT_connectionState = recMessage.Message;
                    BT_is_Connected = false;
                    break;
                case 4:
                    break;
                case 5:
                    break;
                default:
                    BT_connectionState = recMessage.Message;
                    BT_is_Connected = false;
                    break;
            }




            // END OF CLASS
        }

        public class newMessageEventArgs : EventArgs
        {
            private int _code;
            private string _message;

            public newMessageEventArgs(int code, string message)
            {
                _code = code;
                _message = message;
            }

            public int Code
            {
                get
                {
                    return _code;
                }
            }

            public string Message
            {
                get
                {
                    return _message;
                }
            }
        }
    }













}
