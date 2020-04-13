using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

public static class XmlSerializationFuncs {

    public static string GetSerializabledObjectText<T>(T serializable, string path, string file) where T : IXmlSerializable {
        string filename = file + ".xml";
        string directory = Funcs.CombinePath(Application.streamingAssetsPath, path);
        string filepath = Funcs.CombinePath(directory, filename);

        try {
            Funcs.CheckFile(filepath, true);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            TextWriter writer = new StringWriter();
            serializer.Serialize(writer, serializable);
            writer.Close();
            return writer.ToString();
        } catch {
            Debug.LogError("Serialization failed");
            return "";
        }
    }

    public static bool SaveSerializableObject<T>(T serializable, string path, string file) where T : IXmlSerializable {
        string filename = file + ".xml";
        string directory = Funcs.CombinePath(Application.streamingAssetsPath, path);
        string filepath = Funcs.CombinePath(directory, filename);

        Funcs.CheckFile(filepath, true);
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        TextWriter writer = new StringWriter();
        serializer.Serialize(writer, serializable);
        writer.Close();

        File.WriteAllText(filepath, writer.ToString());

        try {

            return true;
        } catch {
            Debug.LogError("Serialization failed");
            return false;
        }
    }

    public static XmlReader XmlReaderFromFile(string file, bool moveToContent = true) {
        string data = Funcs.ReadTextFile(file);

        XmlReader reader = new XmlTextReader(new StringReader(data));
        if (moveToContent)
            reader.MoveToContent();

        return reader;
    }

    public static string XmlDataFrom<T>(T obj) where T : IXmlSerializable {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        TextWriter writer = new StringWriter();
        serializer.Serialize(writer, obj);
        writer.Close();
        return writer.ToString();
    }

    public static void LoadXmlAssetsFromResources(string path, System.Action<XmlReader> Func) {
        if (Func == null) { return; }
        TextAsset[] files = Resources.LoadAll<TextAsset>(path);

        foreach (TextAsset asset in files) {
            XmlTextReader reader = new XmlTextReader(new StringReader(asset.text));
            reader.MoveToContent();
            Func(reader);
        }
    }

    public static void LoadXmlAssetsFromFolder(string path, System.Action<XmlReader> Func) {
        if (Func == null) { return; }
        if (Directory.Exists(path) == false) { return; }
        List<string> files = Funcs.GetDirectoryFiles(path, "*.xml");

        foreach (string file in files) {
            XmlReader reader = XmlReaderFromFile(file, true);
            Func(reader);
        }
    }

}
