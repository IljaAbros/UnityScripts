using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("Layout/Dynamic Grid Layout Group", 152)]
public class DynamicGridLayoutGroup : LayoutGroup {
    #region enums
    public enum Corner {
        UpperLeft, UpperRight, LowerLeft, LowerRight
    }

    public enum Axis {
        Horizontal, Vertical
    }
    #endregion
    public Corner startCorner;
    public Axis startAxis;
    public Vector2 spacing;
    public GridAxisInfo horizontal, vertical;
    public bool adjustSize = false;
    public bool useRatio = false;
    [Range(0.01f, 3f)]
    public float ratioSize = 1;

    public bool MainHorizontal { get { return startAxis == Axis.Horizontal; } }
    public GridAxisInfo Main { get { return MainHorizontal ? horizontal : vertical; } }
    public GridAxisInfo Secondary { get { return MainHorizontal ? vertical : horizontal; } }

    [System.Serializable]
    public class GridAxisInfo {
        public enum Mode { Fixed, Dynamic, Absolute }
        public Mode mode;
        public int desiredCells = 8;
        public float desiredSize = 100;

        int _cells;
        float _cellSize;

        public int Cells {
            get { return _cells; }
            set { _cells = value; }
        }
        public float CellSize {
            get { return _cellSize; }
            set { _cellSize = value; }
        }

        public void Update(DynamicGridLayoutGroup layoutGroup, float field, float spacing) {
            if (layoutGroup.useRatio && this == layoutGroup.Secondary) {
                Cells = 1;
                CellSize = layoutGroup.Main.CellSize * layoutGroup.ratioSize;
                return;
            }

            if (mode == Mode.Dynamic) {
                Cells = Mathf.RoundToInt(field / desiredSize);
                float size = field - (spacing * (Cells - 1));
                CellSize = size / Cells;
            } else if(mode == Mode.Fixed) {
                Cells = desiredCells;
                float size = field - (spacing * (desiredCells - 1));
                CellSize = size / Cells;
            } else {
                Cells = 1;
                CellSize = desiredSize;
            }

        }
    }


    public override void CalculateLayoutInputHorizontal() {
        base.CalculateLayoutInputHorizontal();

        horizontal.Update(this, rectTransform.rect.size.x - padding.horizontal, spacing.x);
        vertical.Update(this, rectTransform.rect.size.y - padding.vertical, spacing.y);

        Vector2 cellSize = new Vector2(horizontal.CellSize, vertical.CellSize);
        Vector2 sizedelta = this.rectTransform.sizeDelta;
        Vector2 startOffset = new Vector2(GetStartOffset(0, horizontal.CellSize), GetStartOffset(1, vertical.CellSize));

        int capacity = horizontal.Cells * vertical.Cells;
        float sizeCapacity = Mathf.RoundToInt(MainHorizontal ? this.rectTransform.sizeDelta.x - padding.horizontal : this.rectTransform.sizeDelta.y - padding.vertical);
        float mainCellsizeWithSpacing = MainHorizontal ? cellSize.x + spacing.x : cellSize.y + spacing.y;

        int rows = 0;
        for (int i = 0, columns = 0; i < rectChildren.Count; i++, columns++) {
            if (Main.mode == GridAxisInfo.Mode.Absolute) {
                if (columns >= 1 && mainCellsizeWithSpacing * (columns + 1) >= sizeCapacity) {
                    columns = 0;
                    rows++;
                }
            } else {
                if (columns >= Main.Cells) {
                    columns = 0;
                    rows++;
                }
            }

            Vector2 pos = new Vector2(startOffset.x, startOffset.y);
            pos.x = pos.x + (cellSize.x + spacing.x) * (MainHorizontal ? columns : rows);
            pos.y = pos.y + (cellSize.y + spacing.y) * (MainHorizontal ? rows : columns);

            SetChildAlongAxis(rectChildren[i], 0, pos.x, cellSize.x);
            SetChildAlongAxis(rectChildren[i], 1, pos.y, cellSize.y);
        }

        if (adjustSize) {
            if (MainHorizontal) {
                sizedelta.y = padding.vertical + (cellSize.y + spacing.y) * (rows + 1);
            } else {
                sizedelta.x = padding.horizontal + (cellSize.x + spacing.x) * (rows + 1);
            }

            this.rectTransform.sizeDelta = sizedelta;
        }

        SetLayoutInputForAxis(sizedelta.x, sizedelta.x, -1, 0);
        SetLayoutInputForAxis(sizedelta.y, sizedelta.y, -1, 1);
    }

    public override void CalculateLayoutInputVertical() {}
    public override void SetLayoutHorizontal() {}
    public override void SetLayoutVertical() {}

}
