using UnityEngine.UI;
using UnityEngine;

public class InteractableTip : MonoBehaviour
{
    public static GameObject interactableTip;
    public static Text tipText;

    public static void Initialize() {
        interactableTip = GameObject.Find("InteractableTip");
        tipText = interactableTip.GetComponentInChildren<Text>();
        interactableTip.SetActive(false);
    }
    
    public static void DrawTip(string text, Vector3 position) {
        interactableTip.SetActive(true);
        interactableTip.transform.position = position;
        tipText.text = text;
    }
    
    public static void HideTip() {
        if (interactableTip != null)
            interactableTip.SetActive(false);
    }
}
