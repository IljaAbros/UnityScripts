using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UnityExtensionMethods {
    public static bool Empty(this string line) {
        return string.IsNullOrEmpty(line);
    }

    public static string NewLine(this string line) {
        if (line.Empty()) {
            return line;
        }
        return line + "\n";
    }

    public static Vector2 Size(this RectTransform rt) {
        return new Vector2(rt.rect.width - rt.sizeDelta.x, rt.rect.height - rt.sizeDelta.y);
    }

    public static Vector2 Size(this RectTransform rt, GridLayoutGroup grid) {
        return new Vector2(rt.rect.width - (grid.padding.left + grid.padding.right), rt.rect.height - (grid.padding.bottom + grid.padding.top));
    }

    public static void SwitchActive(this GameObject go) {
        go.SetActive(!go.activeSelf);
    }
    public static void ResetTransformation(this Transform trans, bool local = false) {
        if (local) {
            trans.localPosition = Vector3.zero;
            trans.localRotation = Quaternion.identity;
        } else {
            trans.position = Vector3.zero;
            trans.rotation = Quaternion.identity;
        }

        trans.localScale = Vector3.one;
    }
    public static void DestroyChildren(this Transform parent) {
        while (parent.childCount > 0) {
            Transform c = parent.GetChild(0);
            c.SetParent(null);

            MonoBehaviour.Destroy(c.gameObject);
        }
    }

    public static void SetChildrenActive(this Transform parent, bool active) {
        for (int i = 0; i < parent.childCount; i++) {
            Transform child = parent.GetChild(i);
            child.gameObject.SetActive(active);
        }
    }

    public static void SetParentAndReset(this Transform trans, Transform parent, bool local = false) {
        trans.SetParent(parent);
        ResetTransformation(trans, local);
    }
    public static Vector2 Size(this Sprite sprite) {
        return new Vector2(sprite.rect.width, sprite.rect.height);
    }

    public static Vector3 GetCenter(this Transform transform) {
        Renderer renderer = transform.GetComponent<Renderer>();
        if (renderer != null) {
            return renderer.bounds.center;
        } else {
            return transform.position;
        }
    }
}