using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public static class UnityUIExtensions {

    public static void SetSprite(this Image image, Sprite sprite) {
        image.sprite = sprite != null? sprite:Resources.Load<Sprite>("Empty");
    }

    public static Text GetTextComponent(this MonoBehaviour obj) {
        Text text = obj.GetComponent<Text>();
        if(text == null) {
            text = obj.GetComponentInChildren<Text>();
        }
        return text;
    }

    public static void Set(this Button button, UnityAction action) {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }

    public static void Set(this Button button, string name, UnityAction action) {
        button.Set(name, name, action);
    }

    public static void Set(this Button button, string name, string text, UnityAction action) {
        button.name = name;

        Text textComponent = button.GetTextComponent();
        if (textComponent != null) {
            textComponent.text = text;
        }

        button.Set(action);
    }

}