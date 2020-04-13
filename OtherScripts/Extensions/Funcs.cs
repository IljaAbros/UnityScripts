using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Funcs {

    public static T AttemptClone<T>(T obj) {
        if (obj is IClonaable<T>) {
            return ((IClonaable<T>)obj).Clone();
        }
        return obj;
    }

    public static Sprite LoadResourceSprite(string path, string request) {
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        foreach (Sprite sprite in sprites) {
            if (sprite.name == request) {
                return sprite;
            }
        }
        return null;
    }

    public static bool CheckFile(string path, bool create) {
        if (File.Exists(path) == false) {
            string directory = Path.GetDirectoryName(path);
            if (Directory.Exists(directory) == false) {
                //Directory.CreateDirectory(path);
                if (create) {
                    Debug.Log(directory + " does not exist. Creating...");
                    Directory.CreateDirectory(directory);
                } else {
                    Debug.Log(directory + " does not exist.");
                    return false;
                }
            }
            return false;
        }
        return true;
    }

    public static bool DeleteFile(string path) {
        if (File.Exists(path) == true) {
            File.Delete(path);
            return true;
        } else {
            return false;
        }
    }


    public static Vector3 ScreenCenter() {
        return new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
    }

    public static bool InScreenBounds(Vector2 pos) {
        return pos.x > 0 && pos.x < Screen.width && pos.y > 0 && pos.y < Screen.height;
    }

    public static Rect ScreenRect() {
        return new Rect(0, 0, Screen.width, Screen.height);
    }

    static int[] POW_OF_2_VALUES = new int[] { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768, 65536 };
    public static int GetPowOfTwoValue(int index) {
        return POW_OF_2_VALUES[index];
    }
	
	public static int GetByteValue(params bool[] bits) {
        int v = 0;

        for (int i = 0; i < bits.Length; i++) {
            if (bits[i]) {
                v += GetPowOfTwoValue(i);
            }
        }

        return v;
    }

    public static float Round(float value, int decimals) {
        if(decimals == 0) {
            return Mathf.Round(value);
        } else {
            float multi = Mathf.Pow(10.0f, (float)decimals);
            return Mathf.Round(value * multi) / multi;
        }
    }

    public static string BuildString(params object[] objs) {
        System.Text.StringBuilder writer = new System.Text.StringBuilder();
        foreach (object obj in objs) {
            writer.Append(obj);
        }
        return writer.ToString();
    }

    public static float[] FloatMultiParse(string data, int length, string front = "(", string back = ")") {
        try {
            if(string.IsNullOrEmpty(front) == false) { data = data.Replace(front, ""); }
            if (string.IsNullOrEmpty(back) == false) { data = data.Replace(back, ""); }

            //split
            string[] splitData = data.Split(',');
            if(length == 0) { length = splitData.Length; }

            float[] result = new float[length];

            for (int i = 0; i < Mathf.Max(length, splitData.Length); i++) {
                result[i] = float.Parse(splitData[i]);
            }

            return result;
        }
        catch { return null; }
    }

    public static int[] IntMultiParse(string data, int length, string front = "(", string back = ")") {
        try {
            if (string.IsNullOrEmpty(front) == false) { data = data.Replace(front, ""); }
            if (string.IsNullOrEmpty(back) == false) { data = data.Replace(back, ""); }

            //split
            string[] splitData = data.Split(',');
            if (length == 0) { length = splitData.Length; }

            int[] result = new int[length];

            for (int i = 0; i < Mathf.Max(length, splitData.Length); i++) {
                result[i] = int.Parse(splitData[i]);
            }

            return result;
        } catch { return null; }
    }

    public static string CombinePath(params string[] strings) {
        if (strings.Length == 0)  {
            return "";
        }

        string path = strings[0];

        if (strings.Length > 1){
            for (int i = 1; i < strings.Length; i++)  {
                path = Path.Combine(path, strings[i]);
            }
        }

        return path;
    }

    public static List<string> GetDirectoryFiles(string path, string searchPattern, int maxdepth = 25, int depth = 0) {
        depth += 1;

        List<string> files;

        if (string.IsNullOrEmpty(searchPattern))
            files = new List<string>(Directory.GetFiles(path));
        else
            files = new List<string>(Directory.GetFiles(path, searchPattern));

        string[] subDirs = Directory.GetDirectories(path);

        if (depth < maxdepth) {
            foreach (string sd in subDirs) {
                files.AddRange( GetDirectoryFiles(sd, searchPattern, maxdepth, depth) );
            }
        }

        return files;
    }

    public static List<FileInfo> GetDirectoryFileInfos(string path, string searchPattern, int maxdepth = 25) {
        List<string> found = GetDirectoryFiles(path, searchPattern, maxdepth);

        List<FileInfo> files = new List<FileInfo>();
        foreach (string f in found) {
            FileInfo fi = new FileInfo(f);
            files.Add(fi);
        }

        return files;
    }

    public static string ReadTextAsset(string path) {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        if(textAsset != null) {
            return textAsset.text;
        }
        return "";
    }

    public static string[] ReadTextAssetLines(string path) {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        if (textAsset != null) {
            return textAsset.text.Split('\n');
        }
        return new string[0];
    }

    public static string ReadTextFile(string file) {
        if (File.Exists(file) == false) {
            Debug.LogWarning("ERROR: " + file + " does not exist!");
            return null;
        }

        string data = File.ReadAllText(file);

        return data;
    }

    public static void WriteTextFile(string file, string data) {
        string path = new FileInfo(file).Directory.FullName;

        if(Directory.Exists(path) == false) {
            Directory.CreateDirectory(path);
        }

        File.WriteAllText(file, data);
    }

    public static string RepeatChar(char c, char n, int count, int max) {
        string line = "";

        for (int i = 0; i < max; i++) {
            if (count > i) {
                line += c;
            } else {
                line += n;
            }
        }

        return line;
    }

    public static string RepeatChar(char c, int count) {
        string line = "";

        for (int i = 0; i < count; i++) {
            line += c;
        }

        return line;
    }


}
