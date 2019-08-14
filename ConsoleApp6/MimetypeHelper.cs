using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp6
{
    internal class MimetypeHelper
    {
        private Dictionary<string, string> baseMimetypes = null;
        private static MimetypeHelper INSTANCE = null;

        private MimetypeHelper()
        {
            this.baseMimetypes = new Dictionary<string, string>();
            Load(@"C:\Users\junio\source\repos\ConsoleApp6\ConsoleApp6\MimeTypes.xml");
        }

        public static MimetypeHelper GetInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new MimetypeHelper();
            }

            return INSTANCE;
        }

        public void Load(string path)
        {
            XElement element = XElement.Load(path);

            IEnumerable<XElement> mimetypeElements = element.Element("MimeTypes").Elements();

            foreach (XElement mimetype in mimetypeElements)
            {
                string extension = mimetype.Attribute("fileExtension").Value;
                string type = mimetype.Attribute("type").Value;

                this.baseMimetypes.Add(extension, type);
            }
        }

        public string GetMimetype(string fileExtension)
        {
            string value = null;

            if (this.baseMimetypes.ContainsKey(fileExtension))
            {
                value = this.baseMimetypes[fileExtension];
            }

            return value;
        }
    }
}
