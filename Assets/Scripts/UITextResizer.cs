using UnityEngine.UI;
using UnityEngine;

[ExecuteAlways]
public class UITextResizer : MonoBehaviour {
    [SerializeField] private RectTransform rt;
    [SerializeField] private Text txt;
    [SerializeField] private float smallestWidth = 153;
    [SerializeField] private float offset = 32;
    
    void Update() {
        rt.sizeDelta = new Vector2(Mathf.Max(smallestWidth, txt.preferredWidth + offset), rt.sizeDelta.y);
    }
}
