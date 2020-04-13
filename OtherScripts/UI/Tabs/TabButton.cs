using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler {

    [System.NonSerialized] public TabButtonGroup group;

    public Text label;
    public Image background;

    public UnityEvent OnTabSelected;
    public UnityEvent OnTabDeselected;

    private void Awake() {
        if(label == null) {
            label = GetComponentInChildren<Text>();
        }

        if(background == null) {
            background = GetComponent<Image>();
        }

        if(background == null) {
            Debug.LogError("ERROR: "+gameObject.name + " can't find an image component");
        }

        if (label != null) { label.color = Color.white; }
        if (background != null) { background.color = Color.white; }

    }

    public void OnPointerClick(PointerEventData eventData) {
        if (group == null) { Debug.LogError("ERROR: " + gameObject.name + "No group assigned!"); return; }
        group.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (group == null) { Debug.LogError("ERROR: " + gameObject.name + "No group assigned!"); return; }
        group.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (group == null) { Debug.LogError("ERROR: " + gameObject.name + "No group assigned!"); return; }
        group.OnTabExit(this);
    }

    public void Select() {
        if (OnTabSelected != null) {
            OnTabSelected.Invoke();
        }
    }

    public void Deselect() {
        if (OnTabDeselected != null) {
            OnTabDeselected.Invoke();
        }
    }

    public void Stop() {
        if (background != null) { background.StopAllCoroutines(); }
        if (label != null) { label.StopAllCoroutines(); }
    }
}
