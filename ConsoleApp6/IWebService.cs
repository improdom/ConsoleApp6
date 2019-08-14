using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    [ServiceContract]
    public interface IWebService
    {
        [OperationContract]
        [WebGet(UriTemplate = "{*path}")]
        Stream GetResource(string path);
    }
}
