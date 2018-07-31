
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

public static class DDHelper
{
    //test unavailable
    public const string WEB_HOOK = "https://oapi.dingtalk.com/robot/send?access_token=bd52d16c5016e8ebb0dff2860f877287db5f39e097cb96017931b1544a5da53e";

    //test available
    //public const string WEB_HOOK = "https://oapi.dingtalk.com/robot/send?access_token=0c3b8296f4bf564519286ab3c1fc61a189cd3ba3c25d00b60a04649500e801d3";

    // 机器人列表    

    public static string SendMsg(string msg,string webHook)
    {
        try
        {
            String textMsg = "{ \"msgtype\": \"text\", \"text\": {\"content\": \"" + msg + "\"}}"; 
            string s = Post(webHook, textMsg, null); 
            return s;
        }
        catch(Exception er)
        {
            throw er;
        }        
    }
    


    #region Post  
    /// <summary>  
    /// 以Post方式提交命令  
    /// </summary>  
    /// <param name="apiurl">请求的URL</param>  
    /// <param name="jsonString">请求的json参数</param>  
    /// <param name="headers">请求头的key-value字典</param>  
    public static String Post(string apiurl, string jsonString, Dictionary<String, String> headers = null) 
    {  
          WebRequest request = WebRequest.Create(@apiurl);  
          request.Method = "POST";  
          request.ContentType = "application/json";  
           if (headers != null)  
          {
              foreach (var keyValue in headers) 
              {  
                 if (keyValue.Key == "Content-Type")  
                 {
                     request.ContentType = keyValue.Value;  
                     continue;  
                 }
                 request.Headers.Add(keyValue.Key, keyValue.Value);  
              }  
          }  
          if (String.IsNullOrEmpty(jsonString))  
          {
              request.ContentLength = 0;
          }
          else
          {
               byte[] bs = Encoding.UTF8.GetBytes(jsonString);
               request.ContentLength = bs.Length;  
               Stream newStream = request.GetRequestStream(); 
               newStream.Write(bs, 0, bs.Length);
               newStream.Close();
          }
           WebResponse response = request.GetResponse();  
           Stream stream = response.GetResponseStream(); 
           Encoding encode = Encoding.UTF8;  
           StreamReader reader = new StreamReader(stream, encode); 
           string resultJson = reader.ReadToEnd();  
           return resultJson;  
    }
 #endregion  

}