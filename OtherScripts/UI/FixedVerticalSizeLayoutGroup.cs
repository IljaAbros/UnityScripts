using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[AddComponentMenu("Layout/Fixed Vertical Size Layout Group", 151)]
public class FixedVerticalSizeLayoutGroup : FixedSizeLayoutGroup {
    public float childHeight = 35f;
    public bool flip;

    public override void CalculateLayoutInputHorizontal() {
        base.CalculateLayoutInputHorizontal();
        UpdateLayout(childHeight, true, flip);
    }

    public override void CalculateLayoutInputVertical() { }
    public override void SetLayoutHorizontal() { }
    public override void SetLayoutVertical() { }
}
