using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TabButtonGroup : MonoBehaviour {
    [System.Serializable]
    public class TabChangeEvent : UnityEvent<int> { }

    public TabGraphic idle;
    public TabGraphic hover;
    public TabGraphic selected;

    [System.Serializable]
    public class TabGraphic {
        public Sprite sprite;
        public Color bodyColor = Color.white;
        public Color textColor = Color.white;
        public float fadeDuration = 0.2f;
    }

    public List<TabButton> tabButtons;
    public TabChangeEvent OnTabChangeEvent;

    public KeyCode previousKey = KeyCode.None;
    public KeyCode nextKey = KeyCode.None;

    private int currentIndex = -1;

    public int Count {
        get { return tabButtons.Count; }
    }

    public int Index {
        get { return currentIndex; }
        set {
            int index = value;

            if(index < 0) {
                index = Count - 1;
            }
            if(index >= Count) {
                index = 0;
            }

            SetTab(index);
        }
    }

    public TabButton Current {
        get {
            if (currentIndex == -1) { return null; }
            return tabButtons[currentIndex];
        }
    }

    private void Awake() {
        foreach (TabButton button in tabButtons) {
            button.group = this;
            SetTabGraphic(button, idle);
        }
    }

    private void Start() {
        SetTab(0);
    }

    private void Update() {
        if (previousKey != KeyCode.None && Input.GetKeyDown(previousKey)) {
            Index -= 1;
        }
        if (nextKey != KeyCode.None && Input.GetKeyDown(nextKey)) {
            Index += 1;
        }
    }

    public void OnTabEnter(TabButton button) {
        ResetTabs();
        if (Current != button) {
            SetTabGraphic(button, hover);
        }
    }

    public void OnTabExit(TabButton button) {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button) {
        if (Current == button) { return; }
        if (tabButtons.Contains(button) == false) { return; }
        SetTab(tabButtons.IndexOf(button));
    }

    public void SetTab(int index) {
        if (currentIndex != -1) {
            if (currentIndex == index) { return; }
            Current.Deselect();
        }

        currentIndex = index;
        ResetTabs();

        if (Current != null) {
            Current.Select();
            SetTabGraphic(Current, selected);
        }

        if (OnTabChangeEvent != null) {
            OnTabChangeEvent.Invoke(index);
        }
    }

    public void ResetTabs() {
        foreach (TabButton button in tabButtons) {
            if (button == Current) { continue; }
            SetTabGraphic(button, idle);
        }
    }

    public void SetTabGraphic(TabButton button, TabGraphic graphic) {
        button.Stop();

        if (button.background.sprite != graphic.sprite) {
            button.background.sprite = graphic.sprite;
        }

        if (graphic.fadeDuration > 0) {
            button.background.CrossFadeColor(graphic.bodyColor, graphic.fadeDuration, true, true);
            if (button.label != null) { button.label.CrossFadeColor(graphic.textColor, graphic.fadeDuration, true, true); }
        } else {
            button.background.canvasRenderer.SetColor(graphic.bodyColor);
            if (button.label != null) { button.label.canvasRenderer.SetColor(graphic.textColor); }
        }
    }

}
