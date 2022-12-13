//using CSILIFEAgent;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace CreateRegistryXsmFromIdeaBankExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!"); 
            string xsd;
            string xsdFile = ConfigurationManager.AppSettings["Export2LISAXSD"].ToString();
            FileInfo fSchema = new FileInfo(xsdFile);
        }
    }
}
