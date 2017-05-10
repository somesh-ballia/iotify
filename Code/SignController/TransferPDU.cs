using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignController
{
    public class TransferPDU
    {
        private int m_SerialAddress;
        private int m_FreeValue;

        public TransferPDU(int SerialAddress, int FreeValue)
        {
            m_SerialAddress = SerialAddress;
            m_FreeValue = FreeValue;
        }

        public Byte[] Serialize()
        {            
            Byte[] retVal = new Byte[12];
            // Fixed
            retVal[0] = 2;             
            retVal[2] = 5;
            retVal[3] = 0;
            retVal[4] = 46;
            retVal[7] = 0;
            retVal[8] = 0;
            retVal[9] = 100;

            // calculated serial address
            retVal[1] = Convert.ToByte(m_SerialAddress);

            int FreeHigh = m_FreeValue / 256;
            int FreeLow = m_FreeValue % 256;
            int Checksum = 153 + m_SerialAddress + FreeHigh + FreeLow; 
            // calculated Free Value
            retVal[5] = Convert.ToByte(FreeLow);   // Free Low
            retVal[6] = Convert.ToByte(FreeHigh);  // Free high          

            int ChecksumHigh = Checksum / 256;
            int ChecksumLow = Checksum % 256;
            // calculated checksum
            retVal[10] = Convert.ToByte(ChecksumLow); // checksum low
            retVal[11] = Convert.ToByte(ChecksumHigh);  // checksum high            
            return retVal;
        }
    }
}
