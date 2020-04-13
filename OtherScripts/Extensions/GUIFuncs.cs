using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GUIFuncs {
    public static string TextInput(string label, string value, int width = 200, bool group = true) {
        if (group) { GUILayout.BeginHorizontal(); }
        GUILayout.Label(label + " ");
        value = GUILayout.TextField(value, GUILayout.Width(width));
        if (group) { GUILayout.EndHorizontal(); }
        return value;
    }

    public static void ModeInput<T>(T obj, T[] modes, System.Action<T> func, Color color, Color selected, bool group = true) {
        if (group) { GUILayout.BeginHorizontal(); }
        foreach (T mode in modes) {
            if(obj != null) { GUI.color = obj.Equals(mode) ? selected : color; }
            if (GUILayout.Button(mode.ToString())) {
                func(mode);
            }
            if (obj != null) { GUI.color = color; }
        }
        if (group) { GUILayout.EndHorizontal(); }
    }


    public static int IntInput(string label, int value, int step = 1, bool group = true) {
        if (group) { GUILayout.BeginHorizontal(); }
        GUILayout.Label(label + ": " + value);
        if (GUILayout.Button("-", GUILayout.Width(20))) {
            value = value - step;
        }

        if (GUILayout.Button("+", GUILayout.Width(20))) {
            value = value + step;
        }
        if (group) { GUILayout.EndHorizontal(); }
        return value;
    }

    public static int IntInput(string label, int value, int min, int max, int step = 1, bool group = true) {
        if (group) { GUILayout.BeginHorizontal(); }
        GUILayout.Label(label + ": " + value);
        if (GUILayout.Button("-", GUILayout.Width(20))) {
            value = value - step;
        }

        if (GUILayout.Button("+", GUILayout.Width(20))) {
            value = value + step;
        }

        value = Mathf.Clamp(value, min, max);

        if (group) { GUILayout.EndHorizontal(); }
        return value;
    }

    public static float FloatInput(string label, float value, float step = 1f, bool group = true) {
        if (group) { GUILayout.BeginHorizontal(); }
        GUILayout.Label(label + ": " + value);
        if (GUILayout.Button("-", GUILayout.Width(20))) {
            value = value - step;
        }

        if (GUILayout.Button("+", GUILayout.Width(20))) {
            value = value + step;
        }
        if (group) { GUILayout.EndHorizontal(); }
        return value;
    }

    public static float FloatSlider(string label, float value, float min = 0, float max = 1, bool group = true, bool vertical = false) {
        if (group) { GUILayout.BeginHorizontal(); }
        GUILayout.Label(label + ": " + value, GUILayout.Width(200));
        if(vertical) {
            value = GUILayout.VerticalSlider(value, min, max);
        } else {
            value = GUILayout.HorizontalSlider(value, min, max);
        }
        if (group) { GUILayout.EndHorizontal(); }
        return value;
    }

    public static void FunctionInput<T>(string label, T value, System.Action<T> func, bool group = true) {
        if (group) { GUILayout.BeginHorizontal(); }

        GUILayout.Label(label);
        if (GUILayout.Button(value!=null?value.ToString():"", GUILayout.Width(200))) {
            func(value);
        }

        if (group) { GUILayout.EndHorizontal(); }
    }

    public static bool ColorInput(ref Color color) {
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);
        float _h = FloatSlider("Hue", h);
        float _s = FloatSlider("Saturation", s);
        float _v = FloatSlider("Value", v);
        color = Color.HSVToRGB(_h, _s, _v);

        return h == _h && s == _s && v == _v;
    }
}
