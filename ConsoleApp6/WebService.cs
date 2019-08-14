using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel.Web;

namespace ConsoleApp6
{
    public class WebService : IWebService
    {
        private string basePath = null;
        private MimetypeHelper baseMimetypeHelper = null;

        public WebService()
        {
            basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot");

            this.baseMimetypeHelper = MimetypeHelper.GetInstance();
        }

        public Stream GetResource(string path)
        {
            Stream resourceStream = null;

            if (string.IsNullOrEmpty(path))
            {
                path = "";
            }

            try
            {

                string mimetype = this.baseMimetypeHelper.GetMimetype(Path.GetExtension(path));

                if (mimetype == null)
                {

                    resourceStream = this.GetErrorResponseStream(404, "Not Found");

                }
                else
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = mimetype;

                    resourceStream = new MemoryStream(File.ReadAllBytes(Path.Combine(basePath, path)));
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                resourceStream = this.GetErrorResponseStream(404, "Not Found");
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                resourceStream = this.GetErrorResponseStream(404, "Not Found");
            }
            catch (Exception ex)
            {
                resourceStream = this.GetErrorResponseStream(500, "Internal Server Error");
            }

            return resourceStream;
        }

        #region private

        private Stream GetErrorResponseStream(int errorCode, string message)
        {
            string pageFormat = @"<!DOCTYPE html>
                < html xmlns ='http://www.w3.org/1999/xhtml'><head><title>Error</title>
                  </head><body> 
                   <font style=\'font-size: 35px;\'>{0} {1}</font></body></html>";

            return new MemoryStream(ASCIIEncoding.ASCII.GetBytes
                       (string.Format(pageFormat, errorCode, message)));
        }

        #endregion
    }

}
