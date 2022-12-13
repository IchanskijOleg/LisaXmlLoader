using CSILIFEAgent;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace LisaXmlLoader
{
    public struct Exp2LISAMsg
    {
        public string Message;
        public string Type;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Exp2LISAMsg[] expMsgs;
            string xsd;
            string xsdFile = ConfigurationManager.AppSettings["Export2LISAXSD"].ToString();
            FileInfo fSchema = new FileInfo(xsdFile);
            using (StreamReader reader = fSchema.OpenText())
            {
                xsd = reader.ReadToEnd();
            }

            string strXmlText = "";
            string win1251 = "";
            byte[] xmlBites;

            XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load("c:\\ТП1104783609_04_2020 16_43_57.xml");
            //xmlDoc.Load("C:\\TEST93.XML");
            //xmlDoc.Load("C:\\СД1105156716_06_2020 15_29_46.xml");
            //xmlDoc.Load("C:\\ТІ3100139121_05_2020 18_43_06 (1).xml");
            //xmlDoc.Load("C:\\ІЮ1105057417_06_2020 09_40_54.xml");
            //xmlDoc.Load("C:\\T260220201000024367.XML");
            //xmlDoc.Load("C:\\test.XML");
            // xmlDoc.Load("C:\\XMLs\\universalApi.xml");
            //xmlDoc.Load("C:\\XMLs\\TASComBant1.XML");
            //xmlDoc.Load("C:\\XMLs\\OnLife.XML");
            //xmlDoc.Load("C:\\Private.xml");
            //xmlDoc.Load("C:\\PrivateBank2.XML");
            //xmlDoc.Load("C:\\PrivateBankWithDoc.XML");

            //xmlDoc.Load("C:\\XMLs\\ІЛ1108454608testnew3.xml");
            //xmlDoc.Load("C:\\XMLs\\TASLikar.XML");
            xmlDoc.Load("C:\\XMLs\\OnlifeNEW1.XML");


            using (StringWriter sw = new StringWriter())
            {
                using (XmlTextWriter tx = new XmlTextWriter(sw))
                {
                    xmlDoc.WriteTo(tx);
                    strXmlText = sw.ToString();
                }
            }

             xmlBites = Encoding.UTF8.GetBytes(strXmlText);
             //win1251 = Encoding.UTF8.GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("windows-1251"), xmlBites));
              win1251 = Encoding.GetEncoding("windows-1251").GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("windows-1251"), xmlBites));
            //win1251 = Encoding.GetEncoding("windows-1251").GetString(Encoding.Convert( Encoding.GetEncoding("windows-1251"), Encoding.GetEncoding("windows-1251"), xmlBites));



            //CSILIFEAgent.CSILIFEAgent csExporter = new CSILIFEAgent.CSILIFEAgent();
            CSILIFEAgentClass csExporter = new CSILIFEAgentClass();
            //var csExporter = new CSILIFEAgent.CSILIFEAgent();
            ICSILIFELog errLog = null; 
            try
            {
                // настройки!!!
                errLog = csExporter.Load(//"10.72.1.9",
                                         "10.73.2.152",//ConfigurationManager.AppSettings["LISAServAppIP"].ToString(),
                                         211, // Convert.ToInt32(ConfigurationManager.AppSettings["LISAServAppPort"]),
                                         "lisa", //ConfigurationManager.AppSettings["Exp2LISAUser"].ToString(),
                                         "lisatest", //ConfigurationManager.AppSettings["Exp2LISAPassw"].ToString(),
                                         1, 
                                         win1251, 
                                         xsd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            expMsgs = new Exp2LISAMsg[errLog.Count];
            bool res = true;
            for (int i = 0; i <= errLog.Count - 1; i++)
            {
                if (errLog.ItemType[i] == CSILIFEAgent.MessageType.mtError)
                {
                    res = false;

                    expMsgs[i].Type = errLog.ItemType[i].ToString();
                    expMsgs[i].Message = errLog.Item[i];
                    Console.WriteLine(errLog.Item[i]);
                }

            }

            Console.WriteLine("УРА!");
            Console.ReadKey();
        }
    }
}
