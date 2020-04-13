using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public static class ImageExtensions {

    public static Sprite[] LoadSpriteImageWithXmlData(string filePath){
        string baseSpriteName = Path.GetFileNameWithoutExtension(filePath);
        string basePath = Path.GetDirectoryName(filePath);
        string xmlPath = Path.Combine(basePath, baseSpriteName + ".xml");

        Texture2D imageTexture;
        List<Sprite> list = new List<Sprite>();
        XmlReader reader = null;

        try{
            if (File.Exists(xmlPath)) {
                reader = XmlSerializationFuncs.XmlReaderFromFile(xmlPath);
                FilterMode fm = (FilterMode)reader.GetIntAttribute("filtermode", 1);
                TextureWrapMode twm = (TextureWrapMode)reader.GetIntAttribute("wrapmode", 1);

                imageTexture = ImportTexture2D(filePath, fm, twm);

                if (reader.ReadToDescendant("Sprite")){
                    do {
                        Sprite sprite = LoadSpriteWithXmlData(reader, imageTexture);
                        list.Add(sprite);
                    } while (reader.ReadToNextSibling("Sprite"));
                }
            } else {
                imageTexture = ImportTexture2D(filePath);
                Sprite sprite = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), new Vector2(0.5f, 0.5f), 64);
                sprite.name = baseSpriteName;

                list.Add(sprite);
            }
        }
        catch {
            return new Sprite[0];
        }

        return list.ToArray();
    }

    public static Sprite LoadSpriteWithXmlData(XmlReader reader, Texture2D texture) {
        string name = reader.GetAttribute("name");
        int x = reader.GetIntAttribute("x", 0);
        int y = reader.GetIntAttribute("y", 0);
        int w = reader.GetIntAttribute("w", texture.width);
        int h = reader.GetIntAttribute("h", texture.height);

        w = Mathf.Clamp(w, 1, texture.width - x);
        h = Mathf.Clamp(h, 1, texture.height - y);

        float px = reader.GetFloatAttribute("pivotx", 0.5f);
        float py = reader.GetFloatAttribute("pivoty", 0.5f);
        int ppu = reader.GetIntAttribute("pixelsPerUnit", 64);

        Sprite sprite = Sprite.Create(texture, new Rect(x, y, w, h), new Vector2(px, py), ppu);
        sprite.name = name;

        return sprite;
    }

	public static Texture2D ImportTexture2D (string filePath, FilterMode fm = FilterMode.Bilinear, TextureWrapMode wm = TextureWrapMode.Clamp) {
        if (AnyExtensionSupported(filePath, supportedImageExtensions) == false)
            return null;

        byte[] imageBytes = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);

        if (texture.LoadImage(imageBytes)) {
            texture.filterMode = fm;
            texture.wrapMode = wm;
            return texture;
        }

        return null;
    }

    #region supportedExtensions
    public static string[] supportedImageExtensions = new string[] { ".png", ".jpg", ".jpeg" };

    public static bool IsExtensionSupported(string filePath, string req) {
        string fileExtension = Path.GetExtension(filePath);
        if (req == fileExtension) {
            return true;
        }

        return false;
    }

    public static bool AnyExtensionSupported(string filePath, string[] reqs){
        string fileExtension = Path.GetExtension(filePath);
        foreach (string e in reqs) {
            if (fileExtension == e) {
                return true;
            }
        }
        return false;
    }
    #endregion
}
