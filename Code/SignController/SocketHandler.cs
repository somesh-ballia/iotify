using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using ApplicationLogger;

namespace SignController
{
    public class SocketHandler
    {
        /// <summary>
        /// Logger
        /// </summary>
        private ILog m_log;

        private _HostControl m_hostControl;
        private int m_RuntimeXMLOutput;

        private enum Status
        {
            ENM_ACTIVE_RESPONDING,
            ENM_ACTIVE_NOT_RESPONDING,
            ENM_INACTIVE
        };

        public SocketHandler(_HostControl hostControl, int RuntimeXMLOutput)
        {
            m_hostControl = hostControl;
            m_RuntimeXMLOutput = RuntimeXMLOutput;
            m_log = AppLogger.GetLog("xxxxxx_SocketHandler");
        }

        
        public bool SendData(out String StatusMessage)
        {
            StatusMessage = "InActive";
            Status iStatus = Status.ENM_INACTIVE;
            bool bIsSendSuccessful = false;
            TcpClient client = null;
            NetworkStream stream = null;
            iStatus = Status.ENM_INACTIVE;

            try
            {
                m_log.Information("Creating connection to IP :" + m_hostControl.IP + " Port :" + m_hostControl.Port, 0, null);
                client = new TcpClient(m_hostControl.IP, m_hostControl.Port);           
                iStatus = Status.ENM_ACTIVE_NOT_RESPONDING;                
                m_log.Information("Connection Created", 0, null);
                
                // Translate the passed message into ASCII and store it as a Byte array.
                TransferPDU pdu = new TransferPDU(m_hostControl.SerialAddress, m_RuntimeXMLOutput);
                m_log.Information("PDU Created", 0, null);
                Byte[] data = pdu.Serialize();
                m_log.Information("PDU Searilized", 0, null);
                stream = client.GetStream();
                m_log.Information("Sending data :" + data[0].ToString() + "," +
                                   data[1].ToString() + "," +
                                   data[2].ToString() + "," +
                                   data[3].ToString() + "," +
                                   data[4].ToString() + "," +
                                   data[5].ToString() + "," +
                                   data[6].ToString() + "," +
                                   data[7].ToString() + "," +
                                   data[8].ToString() + "," +
                                   data[9].ToString() + "," +
                                   data[10].ToString() + "," +
                                   data[11].ToString(),
                                   0, null);

                m_log.Information("Writing Data to Socket", 0, null);
                stream.Write(data, 0, data.Length);
                m_log.Information("Write complete", 0, null);
                data = new Byte[256];
                String responseData = String.Empty;
                m_log.Information("Waiting for Response", 0, null);
                stream.ReadTimeout = 3000;
                Int32 bytes = stream.Read(data, 0, data.Length);
                m_log.Information("Response Received", 0, null);
                if ((0 < bytes) && (data[0] == 0x02))
                {
                    bIsSendSuccessful = true;
                    m_log.Information("Received data :" + data[0].ToString() + "," +
                                   data[1].ToString() + "," +
                                   data[2].ToString() + "," +
                                   data[3].ToString() + "," +
                                   data[4].ToString() + "," +
                                   data[5].ToString() + "," +
                                   data[6].ToString() + "," +
                                   data[7].ToString() + "," +
                                   data[8].ToString() + "," +
                                   data[9].ToString() + "," +
                                   data[10].ToString() + "," +
                                   data[11].ToString(),
                                   0, null);

                    iStatus = Status.ENM_ACTIVE_RESPONDING;         
                }
            }
            catch (Exception ex)
            {
                m_log.Error("Exception Ocoured while sending PDU, Message :" + ex.Message, 0, null);
            }
            finally
            {
                if (null != stream)
                    stream.Close();
                if (null != client)
                    client.Close();
            }

            switch (iStatus)
            {
                case Status.ENM_INACTIVE:
                    StatusMessage = "InActive";
                    break;
                case Status.ENM_ACTIVE_RESPONDING:
                    StatusMessage = "Active - Responding";
                    break;
                case Status.ENM_ACTIVE_NOT_RESPONDING:
                    StatusMessage = "Active - NotResponding";
                    break;
            }

            return bIsSendSuccessful;
        }
    }
}
