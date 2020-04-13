using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public static class XmlExtensions {
    public static void ReadXml(this Dictionary<string, int> dictionary, XmlReader reader, string element, string key, string value) {
        if (dictionary == null || reader == null) { return; }
        reader.ReadChildren(element, () => {
            string ID = reader.GetStringAttribute(key, "");
            int v = reader.GetIntAttribute("value", 0);
            if (!ID.Empty()) {
                dictionary[ID] = v;
            }
        });
    }

    public static void WriteXml(this Dictionary<string, int> dictionary, XmlWriter writer, string element, string key, string value, bool writeZero = true, string encapsulator = null) {
        if (dictionary == null || writer == null) { return; }

        bool encapsulate = !encapsulator.Empty();
        if (encapsulate) {
            writer.WriteStartElement(encapsulator);
        }

        foreach (KeyValuePair<string, int> v in dictionary) {
            if (!writeZero && v.Value == 0) { continue; }

            writer.WriteStartElement(element);
            writer.WriteAttributeString(key, v.Key);
            writer.WriteAttributeString(value, v.Value);
            writer.WriteEndElement();
        }

        if (encapsulate) {
            writer.WriteEndElement();
        }
    }
}
