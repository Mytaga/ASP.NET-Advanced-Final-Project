namespace PizzaOrderingSystem.Web.Controllers
{
    using Azure.Storage.Blobs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using PizzaOrderingSystem.Common;
    using System.IO;
    using System.Threading.Tasks;
    using System;

    [Authorize]
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        protected async Task<string> UploadPhoto(IFormFile imageUrl)
        {
            string fileUrl = string.Empty;

            if (imageUrl != null)
            {
                string connectionString = GlobalConstants.BlobConnectionString;
                string containerName = GlobalConstants.BlobContainer;

                var fileName = GenerateFileName(imageUrl);

                BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
                var blob = container.GetBlobClient(imageUrl.FileName);
                using (Stream stream = imageUrl.OpenReadStream())
                {
                    await blob.UploadAsync(stream);
                }

                fileUrl = blob.Uri.AbsoluteUri;
            }

            return fileUrl;
        }

        protected string GenerateFileName(IFormFile imageUrl)
        {
            string fileName = imageUrl.FileName;

            try
            {
                string strFileName = string.Empty;
                string[] strName = fileName.Split('.');
                strFileName = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/"
                   + DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." +
                   strName[strName.Length - 1];
                return strFileName;
            }
            catch (Exception ex)
            {
                return fileName;
            }
        }
    }
}