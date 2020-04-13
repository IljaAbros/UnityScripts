using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

public class TemplateList<T> : IDList<T> , IXmlSerializable where T : ITemplate, new() {

    public TemplateList(){}
    public TemplateList(string path, string name, bool _override) {
        this._path = path;
        this._name = name;
        this._override = _override;
    }

    private readonly string _path;
    private readonly string _name;
    private readonly bool _override;

    public void Load() {
        LoadFromResources();
        LoadLocals();
    }

    public void LoadFromResources() {
        XmlSerializationFuncs.LoadXmlAssetsFromResources(_path, ReadXml );
    }

    public void LoadLocals() {
        XmlSerializationFuncs.LoadXmlAssetsFromFolder(Funcs.CombinePath(Application.streamingAssetsPath, _path), ReadXml);
    }

    #region xml
    public XmlSchema GetSchema() {
        return null;
    }

    public void ReadXml(XmlReader reader) {
        reader.ReadChildren(_name, () => {
            string ID = reader.GetStringAttribute("ID", "");
            if (ID.Empty()) { return; }

            if (Contains(ID) && !_override) {
                T template = Get(ID);
                template.ReadXml(reader);
            } else {
                T template = new T();
                template.ReadXml(reader);

                Edit(template);
            }
        });
    }

    public void WriteXml(XmlWriter writer) {
        foreach(T item in _content.Values) {
            writer.WriteStartElement(_name);
            item.WriteXml(writer);
            writer.WriteEndElement();
        }
    }
    #endregion

	
}
