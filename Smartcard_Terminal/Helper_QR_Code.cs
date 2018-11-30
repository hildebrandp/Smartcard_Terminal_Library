using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using System.Drawing;

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

        public Helper_QR_Code(Smartcard_Terminal terminal)
        {
            this.terminal = terminal;
            qrGenerator = new QRCodeGenerator();
        }

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
