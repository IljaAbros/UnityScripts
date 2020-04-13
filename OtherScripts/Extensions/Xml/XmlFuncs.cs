using System.Xml;
using UnityEngine;
using System.Collections.Generic;

public static class XmlFuncs {
    public static string ID(this XmlReader reader) {
        return reader.GetAttribute("ID");
    }

    public static bool CheckAttributeElement(this XmlReader reader, string name) {
        return reader.Name == name && reader.HasAttributes;
    }

    public static bool GetBoolAttribute(this XmlReader reader, string att, bool v) {
        try {
            bool r = bool.Parse(reader.GetAttribute(att));
            return r;
        } catch {
            return v;
        }
    }

    public static string GetStringAttribute(this XmlReader reader, string att, string v) {
        string r = reader.GetAttribute(att);
        if (string.IsNullOrEmpty(r)) {
            return v;
        }
        return r;
    }

    public static int GetIntAttribute(this XmlReader reader, string att, int v) {
        try {
            int r = int.Parse(reader.GetAttribute(att));
            return r;
        } catch {
            return v;
        }
    }

    public static float GetFloatAttribute(this XmlReader reader, string att, float v) {
        try {
            float r = float.Parse(reader.GetAttribute(att));
            return r;
        } catch {
            return v;
        }
    }

    public static Color GetColorAttribute(this XmlReader reader, string att, Color v) {
        try {
            //get the value
            att = reader.GetAttribute(att);
            float[] rgba = Funcs.FloatMultiParse(att, 4, "RGBA(", ")");
            return new Color(rgba[0], rgba[1], rgba[2], rgba[3]);
        } catch { return v; }
    }

    public static Vector2 GetVector2(this XmlReader reader, string att, Vector2 v) {
        try {
            //get the value
            att = reader.GetAttribute(att);
            float[] result = Funcs.FloatMultiParse(att, 2, "(", ")");
            return new Vector2(result[0], result[1]);
        } catch {
            return v;
        }
    }

    public static Vector3 GetVector3(this XmlReader reader, string att, Vector3 v) {
        try {
            //get the value
            att = reader.GetAttribute(att);
            float[] result = Funcs.FloatMultiParse(att, 3, "(", ")");
            return new Vector3(result[0], result[1], result[2]);
        } catch {
            return v;
        }
    }

    public static Vector4 GetVector4(this XmlReader reader, string att, Vector4 v) {
        try {
            //get the value
            att = reader.GetAttribute(att);
            float[] result = Funcs.FloatMultiParse(att, 4, "(", ")");
            return new Vector4(result[0], result[1], result[2], result[3]);
        } catch {
            return v;
        }
    }

    public static Rect GetRectAttribute(this XmlReader reader, string att, Rect v) {
        try {
            //get the value
            att = reader.GetAttribute(att);
            float[] result = Funcs.FloatMultiParse(att, 4, "(", ")");
            return new Rect(result[0], result[1], result[2], result[3]);
        } catch {
            return v;
        }
    }

    public static float[] GetFloatArrayAttribute(this XmlReader reader, string att, float[] v, int length = 0) {
        try {
            att = reader.GetAttribute(att);
            float[] result = Funcs.FloatMultiParse(att, length, null, null);

            return result;
        } catch {
            return v;
        }
    }

    public static int[] GetIntArrayAttribute(this XmlReader reader, string att, int[] v, int length = 0) {
        try {
            att = reader.GetAttribute(att);
            int[] result = Funcs.IntMultiParse(att, length, null, null);

            return result;
        } catch {
            return v;
        }
    }

    public static T GetEnumAttribute<T>(this XmlReader reader, string att, T v) {
        try {
            //get the value
            att = reader.GetAttribute(att);
            T result = (T)System.Enum.Parse(typeof(T), att, true);
            return result;
        } catch {
            return v;
        }
    }

    public struct SubTreeNode {
        public SubTreeNode(string name, System.Action action) {
            this.name = name;
            this.action = action;
        }

        public string name;
        public System.Action action;
    }

    public static void ReadSubTrees(this XmlReader reader, params SubTreeNode[] actions) {
        Dictionary<string, System.Action> list = new Dictionary<string, System.Action>();
        foreach(SubTreeNode node in actions) {
            list.Add(node.name, node.action);
        }

        XmlReader subreader = reader.ReadSubtree();
        while (subreader.Read()) {
            string current = subreader.Name;
            if (list.ContainsKey(current) && list[current] != null) {
                list[current]();
            }
        }
    }

    public static void ReadChildren(this XmlReader reader, string name, System.Action action) {
        if (reader.ReadToDescendant(name)) {
            do {
                if (reader.HasAttributes) {
                    if (action != null)
                        action();
                }
            } while (reader.ReadToNextSibling(name));
        }
    }

    public static void WriteAttributeString (this XmlWriter writer, string name, int value) {
        writer.WriteAttributeString(name, value.ToString());
    }

    public static void WriteAttributeString(this XmlWriter writer, string name, float value) {
        writer.WriteAttributeString(name, value.ToString());
    }

    public static void WriteAttributeString(this XmlWriter writer, string name, bool value) {
        writer.WriteAttributeString(name, value.ToString());
    }

    public static void WriteAttributeString(this XmlWriter writer, string name, Color value) {
        writer.WriteAttributeString(name, value.ToString());
    }

    public static void WriteAttributeString(this XmlWriter writer, string name, int[] value, string front = null, string back = null) {
        string data = "";

        if (string.IsNullOrEmpty(front)) {
            data += front;
        }

        for (int i = 0; i < value.Length; i++) {
            data += value[i].ToString();
            if(i != value.Length - 1) {
                data += ",";
            }
        }

        if (string.IsNullOrEmpty(back)) {
            data += back;
        }

        writer.WriteAttributeString(name, data);
    }

    public static void WriteAttributeString(this XmlWriter writer, string name, float[] value, string front = null, string back = null) {
        string data = "";

        if (string.IsNullOrEmpty(front)) {
            data += front;
        }

        for (int i = 0; i < value.Length; i++) {
            data += value[i].ToString();
            if (i != value.Length - 1) {
                data += ",";
            }
        }

        if (string.IsNullOrEmpty(back)) {
            data += back;
        }

        writer.WriteAttributeString(name, data);
    }

}
