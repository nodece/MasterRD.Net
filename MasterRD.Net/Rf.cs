using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MasterRD.Net
{
    public class Rf
    {
        #region ErrorCode
        /// <summary>
        /// 打开串口失败
        /// </summary>
        private string Error_com
        {
           get { return "-1"; }
        }


        /// <summary>
        /// 寻卡失败
        /// </summary>
        private string Error_request
        {
            get { return "-2"; }
        }


        /// <summary>
        /// 卡防冲突失败
        /// </summary>
        private string Error_anticoll
        {
            get { return "-3"; }
        }
        #endregion

        #region MasterRD.Dll
        [DllImport("MasterRD.dll")]
        //rf_init_com(int port,long baud);
        public static extern int rf_init_com(int port, int baud);

        [DllImport("MasterRD.dll")]
        //rf_request(unsigned short icdev, unsigned char model, unsigned short *TagType);
        public static extern int rf_request(ushort icdev, byte model, ref ushort TagType);

        [DllImport("MasterRD.dll")]
        // rf_anticoll(unsigned short icdev, unsigned char bcnt, unsigned char *ppSnr, unsigned char* pRLength);
        public static extern int rf_anticoll(ushort icdev, byte bcnt, ref byte ppsnr, ref byte pRLength);

        [DllImport("MasterRD.dll")]
        public static extern int rf_ClosePort();
        #endregion

        /// <summary>
        /// COM串口号
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public string ReadCard(int port)
        {
     
            int status = 0;
            int band = 19200;
            ushort j = 0;
            byte b1 = 0;
            byte[] buf1 = new byte[200];
            byte model = 82;
            string result = null;

            status = rf_init_com(port, band);
            if (status != 0)
            {
                return Error_com;
            }


            status = rf_request(0, model, ref j);
            if (status != 0)
            {
                return Error_request;
            }


            status = rf_anticoll(0, 4, ref buf1[0], ref b1);
            if (status != 0)
            {
                return Error_anticoll;
            }


            for (int i = 0; i < b1; i++)
            {
                result += Convert.ToString(long.Parse(buf1[i].ToString()), 16).ToUpper();
            }

            rf_ClosePort();
            return result;
        }
    }
}

