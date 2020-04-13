using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGroup : MonoBehaviour  {

    public List<GameObject> panels = new List<GameObject>();

    public void SetPanelIndex(int index) {
        for (int i = 0; i < panels.Count; i++) {
            if (panels[i] == null) { continue; }
            panels[i].SetActive(i == index);
        }
    }

}
