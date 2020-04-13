using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public interface IXmlData {
    void ReadXml(XmlReader reader);
    void WriteXml(XmlWriter writer);
}

public static class XmlDataFuncs {
    public static void WriteXml(this IXmlData data, XmlWriter writer, string encapsulator) {
        writer.WriteStartElement(encapsulator);
        data.WriteXml(writer);
        writer.WriteEndElement();
    }
}