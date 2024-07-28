using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuAbilityDescription : MonoBehaviour, IPointerEnterHandler {
    [SerializeField] private Text abilityTitle;
    [SerializeField] private Text abilityDescription;
    [SerializeField] private string title;
    [SerializeField] private string description;

    public void OnPointerEnter(PointerEventData data) {
        abilityTitle.text = title;
        abilityDescription.text = description;
    }
}
