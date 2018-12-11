using System;
using QRCoder;
using System.Drawing;

/// <summary>
/// Class for generating QR-Code
/// Implements the QR-Coder Library
/// </summary>
namespace Smartcard_Terminal
{
    /// <summary>
    /// Helper Class for creating a QR-Code 
    /// </summary>
    class Helper_QR_Code
    {
        private QRCodeGenerator qrGenerator;
        private QRCodeData qrCodeData;
        private QRCode qrCode;
        private Bitmap qrCodeImage;
        private Smartcard_Terminal terminal;

        /// <summary>
        /// Contructor of Class
        /// </summary>
        /// <param name="terminal">Smartcard_Terminal Instance</param>
        public Helper_QR_Code(Smartcard_Terminal terminal)
        {
            this.terminal = terminal;
            qrGenerator = new QRCodeGenerator();
        }

        /// <summary>
        /// Method for generating the QR-Code with the Bluetooth-Adress
        /// </summary>
        /// <param name="encrypt">true is Data should be encrypted</param>
        /// <param name="debugCode">Debug Code for Android-App</param>
        /// <returns>Image object of QR-COde</returns>
        public Image genQRCode(Boolean encrypt, int debugCode)
        {
            String text;
            if (encrypt)
            {
                text = terminal.Get_BT_Address() + ">>" + terminal.Get_public_p() + 
                                ">>" + terminal.Get_public_g() + ">>" + terminal.Get_public_A() + 
                                ">>" + debugCode.ToString();
            }
            else
            {
                text = terminal.Get_BT_Address() + ">>" + debugCode.ToString();
            }
                   
            qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            qrCode = new QRCode(qrCodeData);
            qrCodeImage = qrCode.GetGraphic(6);

            return qrCodeImage;
        }
    }
}
