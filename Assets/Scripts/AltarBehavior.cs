using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarBehavior : InteractableObject
{
    
    
    public override void Interact() {
        if (GameManager.killsThisFloor >= GameManager.killsRequired) {
            GameManager.instance.NextFloor();
            SoundManager.Play(gameObject, "teleporter");
        }
        else {
            SoundManager.Play(gameObject, "deny");
        }
    }

    public override void DrawTip() {
        Debug.Log(GameManager.killsThisFloor + " " + GameManager.killsRequired);
        if (GameManager.killsThisFloor < GameManager.killsRequired)
            InteractableTip.DrawTip($"Kills: {GameManager.killsThisFloor}/{GameManager.killsRequired}", transform.position + new Vector3(0, 1, 0));
        else
            InteractableTip.DrawTip("Next Floor", transform.position + new Vector3(0, 1, 0));
    }
}
