using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarBehavior : InteractableObject
{
    
    
    public override void Interact() {
        GameManager.instance.NextFloor();
    }

    public override void DrawTip() {
        if (GameManager.killsThisFloor < GameManager.killsRequired)
            InteractableTip.DrawTip($"Kills: {GameManager.killsThisFloor}/{GameManager.killsRequired}", transform.position + new Vector3(0, 1, 0));
        else
            InteractableTip.DrawTip("Next Floor", transform.position + new Vector3(0, 1, 0));
    }
}
