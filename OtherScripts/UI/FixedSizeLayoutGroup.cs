using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class FixedSizeLayoutGroup : LayoutGroup {
    public float spacing = 0;

    public void UpdateLayout(float step, bool vertical, bool flip) {
        float total = vertical ? padding.vertical : padding.horizontal;

        for (int i = 0; i < rectChildren.Count; i++) {
            RectTransform child = rectChildren[i];
            total += step + (i > 0 ? spacing : 0);
        }

        Vector2 size = this.rectTransform.sizeDelta;
        size.x = vertical ? size.x : total;
        size.y = vertical ? total : size.y;
        this.rectTransform.sizeDelta = size;

        SetLayoutInputForAxis(total, 0, 0, 0);
        SetLayoutInputForAxis(total, 0, 0, 1);

        float width = rectTransform.rect.size[0];
        float height = rectTransform.rect.size[1];

        for (int i = 0; i < rectChildren.Count; i++) {
            RectTransform child = rectChildren[i];

            float next = (step + spacing) * i;
            next = flip ? next + step : next;

            float sizeX = vertical ? width - padding.horizontal : step;
            float sizeY = vertical ? step : height - padding.vertical;

            float x = padding.left;
            float y = padding.top;

            if (vertical) {
                y = flip ? height - next - padding.bottom : padding.top + next;
            } else {
                x = flip ? width - next - padding.right : padding.left + next;
            }


            SetChildAlongAxisWithScale(child, 0, x, sizeX, 1);
            SetChildAlongAxisWithScale(child, 1, y, sizeY, 1);
        }

    }

    protected void CalculateGroupSize(float step, int axis) {
        float combinedPadding = axis==0? padding.horizontal:padding.vertical;
        float total = combinedPadding;

        for (int i = 0; i < rectChildren.Count; i++) {
            RectTransform child = rectChildren[i];
            total += step + (i > 0 ? spacing : 0);
        }

        Vector2 size = this.rectTransform.sizeDelta;
        size.x = axis == 0 ? total : size.x;
        size.y = axis == 1 ? total : size.y;
        this.rectTransform.sizeDelta = size;
        SetLayoutInputForAxis(total, 0, 0, axis);
    }

    protected void SetChildrenAlongAxis(int axis, float step, bool vertical) {
        float size = rectTransform.rect.size[axis];
        float alignmentOnAxis = GetAlignmentOnAxis(axis);

        bool alongOtherAxis = (vertical ^ (axis == 1));
        if (alongOtherAxis) {
            float pad = axis == 0 ? padding.horizontal : padding.vertical;
            float innerSize = size - pad;
            for (int i = 0; i < rectChildren.Count; i++) {
                RectTransform child = rectChildren[i];
                float scale = 1;// child.localScale[axis];

                float startOffset = GetStartOffset(axis, innerSize * scale);
                SetChildAlongAxisWithScale(child, axis, startOffset, innerSize, scale);
            }
        } else {
            float pos = (axis == 0 ? padding.left : padding.top);
            float pad = axis == 0 ? padding.horizontal : padding.vertical;
            float innerSize = size - pad;

            for (int i = 0; i < rectChildren.Count; i++) {
                RectTransform child = rectChildren[i];
                float scale = 1;// child.localScale[axis];

                SetChildAlongAxisWithScale(child, axis, pos, step, scale);
                pos += step + spacing;
            }
        }

    }


}
