using UnityEngine.UI;
using UnityEngine;

public class AbilityHUDManager : MonoBehaviour {
    [SerializeField] private int abilityIndex;
    private PlayerStats stats;
    private PlayerAbilities playerAbilities;
    private Slider cooldown;
    private Text cooldownText;

    void Start() {
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
        playerAbilities = GameObject.Find("Player").GetComponent<PlayerAbilities>();
        cooldown = GetComponentInChildren<Slider>();
        cooldownText = GetComponentInChildren<Text>();
    }

    void Update() {
        float timeRemaining = playerAbilities.abilities[abilityIndex].lastUseTime +
                              playerAbilities.abilities[abilityIndex].cooldown - Time.time;
        if (timeRemaining > 0) {
            cooldownText.gameObject.SetActive(true);
            cooldownText.text = Mathf.Ceil(timeRemaining).ToString();
            cooldown.value = timeRemaining / playerAbilities.abilities[abilityIndex].cooldown;
        }
        else {
            cooldownText.gameObject.SetActive(false);
        }
    }
}
