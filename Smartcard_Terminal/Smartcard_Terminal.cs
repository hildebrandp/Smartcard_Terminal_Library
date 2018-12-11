using System;
using System.Windows.Forms;
using System.Drawing;


/// <summary>
/// Main Class of this Library
/// This Class provides the Functions to use your Smartphone as a Smartcard-Reader
/// </summary>
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

        /// <summary>
        /// Constructor of this Class which creates an instance from every used Library
        /// </summary>
        public Smartcard_Terminal()
        {
            sc_Response_Code = new Helper_Smartcard_ResponseCode();
            bt_Connection = new Helper_Bluetooth(this, new receiveMessageCallback(receiveMessage));
            qr_Code = new Helper_QR_Code(this);
            crypt_AES = new CryptLib();
            dh_Helper = new diffieHellman();
        }

        /// <summary>
        /// Method to Create QR-Code with the Bluetooth-Adress
        /// </summary>
        /// <param name="encrypt">If true Connection will be encrypted with AES</param>
        /// <param name="code">Debug value to show special informations on the Smrtphone</param>
        /// <returns>Return Image object with the QR-Code, null if there is an error</returns>
        public Image Gen_QRCode(Boolean encrypt, int code)
        {
            encryptData = encrypt;
            return qr_Code.genQRCode(encrypt, code);
        }

        /// <summary>
        /// Method that return the public prime p
        /// </summary>
        /// <returns>Public P from Diffie-Hellman</returns>
        public string Get_public_p()
        {
            return dh_Helper.get_public_p();
        }

        /// <summary>
        /// Method that return the public prime g
        /// </summary>
        /// <returns>Public G from Diffie-Hellman</returns>
        public string Get_public_g()
        {
            return dh_Helper.get_public_g();
        }

        /// <summary>
        /// Method that return the calculated public value A
        /// </summary>
        /// <returns>Public A from Diffie-Hellman</returns>
        public string Get_public_A()
        {
            return dh_Helper.get_public_A();
        }

        /// <summary>
        /// Method that calculates the private Key with the Public value B from the Smartphone
        /// </summary>
        /// <param name="pub_B">Public B from Smartphone</param>
        public void Set_public_B(String pub_B)
        {
            dh_Helper.set_public_B(pub_B);
        }

        /// <summary>
        /// Method that returns the calculated Key of the Diffie-Hellman Keyexchange
        /// </summary>
        /// <returns>Secret Key</returns>
        public string Get_Shared_Secret()
        {
            return dh_Helper.get_Shared_Secret();
        }

        /// <summary>
        /// Returns Boolean value if the send Data will be encrypted
        /// </summary>
        /// <returns>true if Data will be encrypted</returns>
        public Boolean Get_encryption_state()
        {
            return encryptData;
        }

        /// <summary>
        /// Method that starts the BluetoothListener
        /// </summary>
        /// <returns>true if there is no error</returns>
        public Boolean startBluetoothCon()
        {
            return bt_Connection.StartBTListener();
        }

        /// <summary>
        /// Method that stops the Bluetooth Connection
        /// </summary>
        public void Stop_BT_Connection()
        {
            bt_Connection.StopBTConnection();
        }

        /// <summary>
        /// Method that return the Bluetooth-Address of the integrated module
        /// </summary>
        /// <returns>Bluetooth Address as Hex-String</returns>
        public string Get_BT_Address()
        {
            return bt_Connection.Get_BT_Device();
        }

        /// <summary>
        /// Method that returns the Bluetooth-Name of the Connected Device
        /// </summary>
        /// <returns>false if no Device connected, else the Name of the Connected Device</returns>
        public string Get_BT_Name()
        {
            return bt_Connection.Get_BT_Name();
        }

        /// <summary>
        /// Method that Return the actual state of the Bluetooth-Connection
        /// </summary>
        /// <returns>State as String</returns>
        public string Get_BT_ConnectionState()
        {
            return BT_connectionState;
        }

        /// <summary>
        /// Boolean Method that returns if Blueooth-Connection is available
        /// </summary>
        /// <returns>true if connected</returns>
        public Boolean Get_BT_is_Connected()
        {
            return BT_is_Connected;
        }

        /// <summary>
        /// Method for accepting a Bluetooth-Connection, only when this Method is called a Connection will be accepted
        /// </summary>
        /// <returns>true if establishing of a connection was successfull</returns>
        public Boolean open_BT_Connection()
        {
            return bt_Connection.openConnection();
        }

        /// <summary>
        /// Synchron Method for sending APDU to Smartcard
        /// </summary>
        /// <param name="code">Code for Android-App</param>
        /// <param name="message">APDU which is send to Smartcard</param>
        /// <returns>Answer of Smartcard</returns>
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

        /// <summary>
        /// Asynchron Method for sending Message to Android-App or Smartcard
        /// </summary>
        /// <param name="code">Code for App</param>
        /// <param name="message">Message for App or Smartcard</param>
        /// <returns>true is Message is send</returns>
        public Boolean SendMessage(int code, string message)
        {
            if (BT_is_Connected)
            {
                return bt_Connection.SendMessage(code, message);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method which receives Answers of the async Method "SendMessage"
        /// </summary>
        /// <param name="recMessage">Recieved Message</param>
        private void receiveMessage(collection_Messages recMessage)
        {
            //Console.WriteLine("SC_Terminal: " + recMessage.Message);

            newMessageEventArgs myArgs = new newMessageEventArgs(recMessage._code, recMessage._message);
            newMessagereceived?.Invoke(this, myArgs);

            BT_is_Connected = bt_Connection.BT_is_Connected();
        }

        // END OF CLASS

        /// <summary>
        /// Class Object for received Messages
        /// </summary>
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
