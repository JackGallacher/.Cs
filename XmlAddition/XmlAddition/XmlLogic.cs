using System;
using System.Xml;

namespace XmlAddition
{
    class XmlLogic
    {
        public int sumXML(String xmlPath)
        {
            int total = 0;
            XmlDocument file = importXML(xmlPath);

            //loops through each child node in the xml.
            foreach(XmlNode node in file.DocumentElement.ChildNodes)
            {
                try
                {
                    //increments total based on parsed child nodes.
                    total += int.Parse(node.InnerText);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
            }
            return total;
        }

        //tried to be as abstract as possible.
        private XmlDocument importXML(String xmlPath)
        {
            XmlDocument file = new XmlDocument();
            try
            {
                file.Load(xmlPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            //prints xml content to screen.
            file.Save(Console.Out);
            return file;
        }
    }
}
