using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Layout/Fixed Horizontal Size Layout Group", 151)]
public class FixedHorizontalSizeLayoutGroup : FixedSizeLayoutGroup {
    public float childWidth = 35f;
    public bool flip;

    public override void CalculateLayoutInputHorizontal() {
        base.CalculateLayoutInputHorizontal();
        UpdateLayout(childWidth, false, flip);
    }

    public override void CalculateLayoutInputVertical() { }
    public override void SetLayoutHorizontal() { }
    public override void SetLayoutVertical() { }

}
