using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Class for APDU response Codes
/// Class is not used!!
/// </summary>
namespace Smartcard_Terminal
{
    class Helper_Smartcard_ResponseCode
    {
        string[,] responseCode = new string[,] 
        {
            { "9000" , "Successful"},
            { "6200", "No information given" },
            { "6281", "Part of returned data may be corrupted"},
            { "6282", "End of file/record reached before reading Le bytes"},
            { "6283", "Selected file invalidated"},
            { "6284", "FCI not formatted according to 1.1.5"},
            { "6300", "Authentication failed"},
            { "6381", "File filled up by the last write"},
            { "6400", "State of nonvolatile memory unchanged"},
            { "6500", "No information given"},
            { "6581", "Memory failure"},
            { "6700", "Wrong length"},
            { "6800", "No information given"},
            { "6881", "Logical channel not supported"},
            { "6882", "Secure messaging not supported"},
            { "6900", "No information given"},
            { "6981", "Command incompatible with file structure"},
            { "6982", "Security status not satisfied"},
            { "6983", "Authentication method blocked"},
            { "6984", "Reference data invalidated"},
            { "6985", "Conditions of use not satisfied"},
            { "6986", "Command not allowed"},
            { "6987", "Secure messaging data object missing"},
            { "6988", "Secure messaging data object incorrect"},
            { "6A00", "No information given"},
            { "6A80", "Incorrect parameters in the data field"},
            { "6A81", "Function not supported"},
            { "6A82", "File not found"},
            { "6A83", "Record not found"},
            { "6A84", "Not enough memory space in file"},
            { "6A85", "Lc inconsistent with TLV structure"},
            { "6A86", "Incorrect parameters P1 P2"},
            { "6A87", "Referenced data not found"},
            { "6D00", "Instruction code not supported or invalid"},
            { "6E00", "Class not supported"},
            { "6F00", "No precise diagnostics"},
            { "6250", "Card Locked"},
            { "63C2", "Wrong, 2 tries left"},
            { "63C1", "Wrong, 1 tries left"},
            { "63C0", "Wrong, 0 tries left.!"},
        };


        public String GetResponse(string resp)
        {
            for (int i = 0; i < responseCode.GetLength(1); i++)
            {
                if (responseCode[0, i].Equals(resp))
                {
                    return responseCode[1, i];
                }
            }
            return null;
        }
    }
}
