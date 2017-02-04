using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace naru.xml
{
    class XMLHelpers
    {
        public static XmlAttribute AddAttribute(ref XmlDocument xmlDoc, ref XmlNode nod, string sAttributeName, string sAttributeValue)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(sAttributeName), "The XML attribute name cannot be empty.");

            XmlAttribute att = xmlDoc.CreateAttribute(sAttributeName);
            att.InnerText = sAttributeValue;
            nod.Attributes.Append(att);
            return att;
        }

        public static XmlNode AddNode(ref XmlDocument xmlDoc, ref XmlNode nodParent, string sNodeName)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(sNodeName), "The XML node name cannot be empty.");
            XmlNode nod = xmlDoc.CreateElement(sNodeName);
            nodParent.AppendChild(nod);
            return nod;
        }

        public static XmlNode AddNode(ref XmlDocument xmlDoc, ref XmlNode nodParent, string sNodeName, string sInnerText)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(sNodeName), "The XML node name cannot be empty.");
            XmlNode nod = xmlDoc.CreateElement(sNodeName);
            if (!string.IsNullOrEmpty(sInnerText))
                nod.InnerText = sInnerText;
            nodParent.AppendChild(nod);
            return nod;
        }
    }
}
