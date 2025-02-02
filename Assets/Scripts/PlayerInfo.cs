using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : LivingBeing
{
    [SerializeField] private GameObject playerUI;
    private float playerMaxHealth;

    private void Start()
    {
        playerMaxHealth = healthPoint;   
        if(playerUI == null)
        {
            playerUI = GameObject.Find("PlayerUI");
        }
    }

    private void Update()
    {
        UpdateHealthUI();
    }

    protected override IEnumerator Die()
    {
        yield return StartCoroutine(base.Die()); // Appelle Die() de LivingBeing et attend sa fin

        Debug.Log("Le joueur meurt, recharge la scène...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateHealthUI()
    {
        playerUI.GetComponent<PlayerUI>().PlayerHealthBar.fillAmount = healthPoint / playerMaxHealth;
    }
}
