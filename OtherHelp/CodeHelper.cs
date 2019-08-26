using OtherHelp.AliYun;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;

namespace OtherHelp
{
    public class Code
    {
        public bool IsGenerate { get; set; }
        public string ImagePath { get; set; }
        public string Message { get; set; }
    }

    public class CodeHelper
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="plainText">内容</param>
        /// <param name="iconPath">图标地址</param>
        /// <param name="isWork">是否为网络地址</param>
        /// <param name="pixelsPer">像素点大小</param>
        /// <param name="iconSize">图标尺寸</param>
        /// <param name="iconBorder">图标边框厚度</param>
        /// <param name="whiteEdge">二维码白边</param>
        public static Code CreateCode(string plainText, string iconPath = null, bool isWork = true, int pixelsPer = 15, int iconSize = 15, int iconBorder = 1, bool whiteEdge = true)
        {
            Code code = new Code();

            try
            {
                var qrCodePath = AppSettings.GetEntityValue("QRCode:QRCodePath");
                if (string.IsNullOrWhiteSpace(qrCodePath)) throw new Exception("二维码上传配置【字段：QRCodePath】找不到");

                QRCodeGenerator generator = new QRCodeGenerator();

                QRCodeData codeData = generator.CreateQrCode(plainText, QRCodeGenerator.ECCLevel.M, true);

                QRCode qrcode = new QRCode(codeData);

                Bitmap qrImage = null;

                if (!string.IsNullOrWhiteSpace(iconPath))
                {
                    //带有logo的二维码
                    Bitmap icon = null;
                    if (isWork)
                        icon = new Bitmap(GetStream(iconPath));
                    else
                        icon = new Bitmap(iconPath);

                    qrImage = qrcode.GetGraphic(pixelsPer, Color.Black, Color.White, icon, iconSize, iconBorder, whiteEdge);
                }
                else
                {
                    qrImage = qrcode.GetGraphic(pixelsPer, Color.Black, Color.White, whiteEdge);
                }

                MemoryStream ms = new MemoryStream();

                qrImage.Save(ms, ImageFormat.Bmp);

                Stream stream = new MemoryStream(ms.GetBuffer());

                string uploadUrl = qrCodePath + DateTime.Now.ToString("yyyyMMdd") + "/" + Guid.NewGuid().ToString() + ".jpeg";

                code.IsGenerate = AliYunHandle.PutFilesStream(uploadUrl, stream);
                code.ImagePath = code.IsGenerate ? AliYunHandle.Domain + uploadUrl : string.Empty;

                return code;
            }
            catch (Exception ex)
            {
                code.IsGenerate = false;
                code.Message = ex.Message;
                return code;
            }
        }

        public static Stream GetStream(string imgUrl)
        {
            WebRequest request = WebRequest.Create(imgUrl);
            WebResponse response = request.GetResponse();
            return response.GetResponseStream();
        }
    }

}
