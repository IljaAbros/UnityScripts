using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonList : UIComponentList<Button> {
    protected override void PullFromPool(Button obj) {
        base.PullFromPool(obj);
        obj.onClick.RemoveAllListeners();
    }
}
