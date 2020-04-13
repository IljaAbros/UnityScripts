using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteCollection : IEnumerable {
    public SpriteCollection() {
        sprites = new Dictionary<string, Sprite>();
        CreateNoSprite();
    }

    Dictionary<string, Sprite> sprites;
    private static Sprite noResourceSprite;

    public IEnumerator<string> GetKeys() {
        return sprites.Keys.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetValues();
    }

    public IEnumerator<Sprite> GetValues() {
        return sprites.Values.GetEnumerator();
    }

    public int Count {
        get { return sprites.Count; }
    }

    public Sprite[] GetAll() {
        return sprites.Values.ToArray();
    }

    public bool HasSprite(string key) {
        return sprites.ContainsKey(key);
    }

    public Sprite GetSprite(string key) {
        if (key.Empty()) { return null; }
        if (sprites.ContainsKey(key) == false) {
            return null;
        }

        return sprites[key];
    }

    public Sprite GetSprite(string key, string mod) {
        if (sprites.ContainsKey(key+"_"+mod) == false) {
            return GetSprite(key);
        }

        return sprites[key + "_" + mod];
    }

    public void Log(bool detail) {
        string text = "Sprites - " + sprites.Count;

        if (detail) {
            foreach (Sprite item in sprites.Values) {
                text += "\n" + item.name;
            }
        }

        Debug.Log(text);
    }



    public void LoadFromResources(string folder) {
        Load(Resources.LoadAll<Sprite>(folder));
    }

    public void Load(Sprite[] list) {
        foreach (Sprite obj in list) {
            sprites[obj.name] = obj;
        }
    }

    public void Load(string folder) {
        LoadFromResources(folder);
        LoadLocals(folder);
    }


    public void LoadLocals(string folder) {
        string directoryPath = Funcs.CombinePath(Application.streamingAssetsPath, folder);
        if (System.IO.Directory.Exists(directoryPath)) {
            LoadSpriteFiles(directoryPath);
        }
    }

    /// <summary>
    /// Loads the sprites from the given directory path.
    /// </summary>
    /// <param name="directoryPath">Directory path.</param>
    private void LoadSpriteFiles(string directoryPath) {
        // First, we're going to see if we have any more sub-directories,
        // if so -- call LoadSpritesFromDirectory on that.
        List<string> filesInDir = Funcs.GetDirectoryFiles(directoryPath, "");

        foreach (string fileName in filesInDir) {
            Sprite[] loaded = ImageExtensions.LoadSpriteImageWithXmlData(fileName);

            foreach (Sprite sprite in loaded) {
                //string spriteCategory = System.IO.Path.GetDirectoryName(fileName); ;
                //spriteCategory = new System.IO.DirectoryInfo(spriteCategory).Name;
                //string spriteName = spriteCategory + "/" + sprite.name;
                //sprite.name = spriteName;
                sprites[sprite.name] = sprite;
            }
        }
    }

    /// <summary>
    /// Creates the no resource texture.
    /// </summary>
    private void CreateNoSprite() {
        if(noResourceSprite != null) {
            return;
        }

        noResourceSprite = Resources.Load<Sprite>("Placeholder");

        if(noResourceSprite != null) {
            return;
        }

        // Generate a 32x32 magenta image
        Texture2D noResourceTexture = new Texture2D(32, 32, TextureFormat.ARGB32, false);
        Color32[] pixels = noResourceTexture.GetPixels32();
        for (int i = 0; i < pixels.Length; i++) {
            pixels[i] = new Color32(255, 0, 255, 255);
        }

        noResourceTexture.SetPixels32(pixels);
        noResourceTexture.Apply();

        noResourceSprite = Sprite.Create(noResourceTexture, new Rect(Vector2.zero, new Vector3(32, 32)), new Vector2(0.5f, 0.5f), 32);
    }

}
