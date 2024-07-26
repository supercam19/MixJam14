using UnityEngine.UI;
using UnityEngine;

public class HealthBarController : MonoBehaviour {
    private Text healthText;
    private Slider healthSlider;

    void Start() {
        healthText = GetComponentInChildren<Text>();
        healthSlider = GetComponentInChildren<Slider>();
    }

    public void SetHealth(float health, float maxHealth) {
        healthText.text = Mathf.RoundToInt(health) + "/" + Mathf.RoundToInt(maxHealth);
        healthSlider.value = health / maxHealth;
    }
}
