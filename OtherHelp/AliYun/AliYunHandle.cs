using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OtherHelp.AliYun
{
    public class AliYunHandle
    {
        readonly static string AccessKeyId = AppSettings.GetEntityValue("AliYun:accessKeyId");
        readonly static string AccessKeySecret = AppSettings.GetEntityValue("AliYun:accessKeySecret");
        readonly static string Endpoint = AppSettings.GetEntityValue("AliYun:endpoint");
        readonly static string BucketName = AppSettings.GetEntityValue("AliYun:bucketName");
        public readonly static string Domain = AppSettings.GetEntityValue("AliYun:domain");

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileRoute"></param>
        /// <returns></returns>
        public static bool PutFiles(string filePath, string fileRoute)
        {
            try
            {
                var client = new OssClient(Endpoint, AccessKeyId, AccessKeySecret);
                var result = client.PutObject(BucketName, fileRoute, filePath);
                if (result != null && (int)result.HttpStatusCode == 200)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 上传图片流至OSS
        /// </summary>
        /// <param name="fileRoute"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool PutFilesStream(string fileRoute, Stream content)
        {
            try
            {
                ObjectMetadata metadata = new ObjectMetadata();

                var client = new OssClient(Endpoint, AccessKeyId, AccessKeySecret);
                var result = client.PutObject(BucketName, fileRoute, content);

                return (result != null && (int)result.HttpStatusCode == 200);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="fileRoute"></param>
        /// <returns></returns>
        public static bool DeleteObject(string fileRoute)
        {
            try
            {
                var client = new OssClient(Endpoint, AccessKeyId, AccessKeySecret);
                client.DeleteObject(BucketName, fileRoute);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
