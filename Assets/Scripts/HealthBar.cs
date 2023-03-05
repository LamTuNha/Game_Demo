using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public TMP_Text healthBarText;
    public Slider healthSlider;
    Damageable playerDamageable;
    private void Awake() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<Damageable>();

        if(player == null){
            Debug.Log("No player found in the scene. Make sure it has tag 'Player'");
        }
    }

    void Start()
    {       
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "HP " + playerDamageable.Health+" / "+ playerDamageable.MaxHealth;
    }

    private void OnEnable() {
        playerDamageable.healthChanged.AddListener(OnPlayHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayHealthChanged);
    }

    private float CalculateSliderPercentage(float  currentHealth, float maxHealth){
        return currentHealth / maxHealth;
    }
    // Update is called once per frame
    private void OnPlayHealthChanged(int newHealth,int maxHealth){
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP " + newHealth+" / "+ maxHealth;
    }
}
