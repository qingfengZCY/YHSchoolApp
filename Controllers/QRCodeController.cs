using System;
using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using QRCoder;
using YHSchool.Services;
using YHSchool.Helper;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using Microsoft.AspNetCore.Hosting.Server;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.DrawingCore.Imaging;
using System.DrawingCore;
using YHSchool.Data;
using YHSchool.Models;

namespace YHSchool.Controllers
{
    [EnableCors("corA")]
    [Produces("application/json")]
    [Route("api/QR")]
    public class QRCodeController : Controller
    {
        private IHostingEnvironment _host = null;

        private readonly ILogger<QRCodeController> _logger;

        private readonly ApplicationDbContext _context;

        public QRCodeController(ApplicationDbContext context, IHostingEnvironment host, ILogger<QRCodeController> logger)
        {
            _context = context;
            this._host = host;
            _logger = logger;
        }

        [HttpGet("path")]
        public string GetPath()
        {  
            var logoPath = Path.Combine(AppContext.BaseDirectory, "images/yh_logo.png");
            return logoPath;
        }

        [HttpGet("webpath")]
        public string GetWebPath()
        {
            var logoPath = _host.WebRootPath+"/images/yh_logo.png";
            return logoPath;
        }

        /// <summary>
        /// 获取登录二维码 
        /// http://localhost:52783/api/QR/qrcode/field_3/x_field_1/1?url=https://jinshuju.net/f/TtBpz8?field_3=zmj
        /// </summary>
        /// <returns></returns>
        [HttpGet("qrcode/{strA}/{strB}/{isReplace}")]
        public void GetQRCode(string strA, string strB, int isReplace = 1)
        {
            Response.ContentType = "image/jpeg";
            string urls = Request.QueryString.ToString().Replace("?url=", "");

            if (isReplace == 1)
                urls = urls.Replace(strA, strB);
            AddInfoEvent("创建分享二维码(qrcode)", urls);
            // var url = "https://jinshuju.net/f/TtBpz8?xfiled_3=zmj";
            var bitmap = GetQRCode(urls, 4,true);

            bitmap.Save(Response.Body, ImageFormat.Jpeg);
        }

        /// <summary>
        /// 获取登录二维码 
        /// http://localhost:52783/api/QR/qrcode_A?url=https://jinshuju.net/f/TtBpz8?field_3=zmj
        /// </summary>
        /// <returns></returns>
        [HttpGet("qrcode_A")]
        public void GetQRCode()
        {
            Response.ContentType = "image/jpeg";
            string urls = Request.QueryString.ToString().Replace("?url=", "");

            urls = urls.Replace("field_3", "x_field_1");

            AddInfoEvent("创建分享二维码(qrcode_A)", urls);

            // var url = "https://jinshuju.net/f/TtBpz8?xfiled_3=zmj";
            var bitmap = GetQRCode(urls, 4,true);

            bitmap.Save(Response.Body, ImageFormat.Jpeg);
        }

        private void AddInfoEvent(string actionType,string urls)
        {
            var logObj = new EventLog();
            logObj.ActionType = actionType;
            logObj.LogLevel = YHSchool.Models.LogLevel.Info.ToString();
            logObj.Comments = urls;

            logObj.CreateDate = DateTime.Now;
            logObj.Creator = "System";
            _context.EventLogs.Add(logObj);
            _context.SaveChangesAsync();
        }

        /// <summary>
        /// 获取登录二维码 
        /// http://localhost:52783/api/QR/qrcode_b?url=https://jinshuju.net/f/TtBpz8?field_3=zmj
        /// </summary>
        /// <returns></returns>
        [HttpGet("qrcode_b")]
        public void GetQRNCode()
        {
            Response.ContentType = "image/jpeg";
            string urls = Request.QueryString.ToString().Replace("?url=", "");

            urls = urls.Replace("field_3", "x_field_1");
            AddInfoEvent("创建分享二维码(qrcode_b)", urls);
            // var url = "https://jinshuju.net/f/TtBpz8?xfiled_3=zmj";
            try
            {
                var bitmap = GetQRCode(urls, 4);


                bitmap.Save(Response.Body, ImageFormat.Jpeg);
            }
            catch (Exception er)
            {
                _logger.LogError(er, "api/QR/qucode_B error");
            }
            
        }


        /// <summary>
        /// 获取登录二维码 
        /// http://localhost:52783/api/QR/qrcode_b/field_3/x_field_1/1?url=https://jinshuju.net/f/TtBpz8?field_3=zmj
        /// </summary>
        /// <returns></returns>
        [HttpGet("qrcodeC/{strA}/{strB}/{isReplace}")]
        public void GetQRCodeC(string strA, string strB, int isReplace = 1)
        {
            Response.ContentType = "image/jpeg";
            string urls = Request.QueryString.ToString().Replace("?url=", "");

            if (isReplace == 1)
                urls = urls.Replace(strA, strB);
            AddInfoEvent("创建分享二维码(qrcodeC)", urls);
            // var url = "https://jinshuju.net/f/TtBpz8?xfiled_3=zmj";
            var bitmap = GetQRCode(urls, 3, true);

            bitmap.Save(Response.Body, ImageFormat.Jpeg);
        }

        /// <summary>
        /// 获取登录二维码 
        /// http://localhost:52783/api/QR/qrcode_c?url=https://jinshuju.net/f/TtBpz8?field_3=zmj
        /// </summary>
        /// <returns></returns>
        [HttpGet("qrcode_C")]
        public void GetQRCode_C()
        {
            Response.ContentType = "image/jpeg";
            string urls = Request.QueryString.ToString().Replace("?url=", "");

            urls = urls.Replace("field_3", "x_field_1");
            AddInfoEvent("创建分享二维码(qrcode_C)", urls);
            // var url = "https://jinshuju.net/f/TtBpz8?xfiled_3=zmj";
            var bitmap = GetQRCode(urls, 3, true);

            bitmap.Save(Response.Body, ImageFormat.Jpeg);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">存储内容</param>
        /// <param name="pixel">像素大小</param>
        /// <returns></returns>
        public Bitmap GetQRCode(string url, int pixel,bool hasLogo=false)
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData codeData = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M, true);
            QRCode qrcode = new QRCode(codeData);
            Bitmap qrImage = qrcode.GetGraphic(pixel);

            if (hasLogo)
            {
                System.IO.MemoryStream MStream = new System.IO.MemoryStream();
                qrImage.Save(MStream, ImageFormat.Png);

                System.IO.MemoryStream MStream1 = new System.IO.MemoryStream();
                var logoPath = _host.WebRootPath + "/images/yh_logo.png"; // Path.Combine(AppContext.BaseDirectory, "images/yh_logo.png");

                var resultImg = QRCodeHelper.CombinImage(qrImage, logoPath);
                Bitmap codeImg = new Bitmap(resultImg);

                return codeImg;
            }
            else
                return qrImage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">存储内容</param>
        /// <param name="pixel">像素大小</param>
        /// <returns></returns>
        public Bitmap GetQRCodeB(string url, int pixel, bool hasLogo = false)
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData codeData = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M, true);
            QRCode qrcode = new QRCode(codeData);
            Bitmap qrImage = qrcode.GetGraphic(pixel);

            if (hasLogo)
            {
                System.IO.MemoryStream MStream = new System.IO.MemoryStream();
                qrImage.Save(MStream, ImageFormat.Png);

                System.IO.MemoryStream MStream1 = new System.IO.MemoryStream();
                var logoPath = _host.WebRootPath + "/images/yh_logo_36.png"; // Path.Combine(AppContext.BaseDirectory, "images/yh_logo.png");

                var resultImg = QRCodeHelper.CombinImage(qrImage, logoPath);
                Bitmap codeImg = new Bitmap(resultImg);

                return codeImg;
            }
            else
                return qrImage;
        }
    }
}