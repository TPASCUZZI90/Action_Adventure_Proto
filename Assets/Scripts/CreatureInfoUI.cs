using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatureInfoUI : MonoBehaviour
{
    private LivingBeing creatureInfo;
    private float creatureMaxHealth;
    private TextMeshProUGUI creatureNameUi;
    private Image healthBar;
    private CreatureInfoUIElements currentInfoUI;
    private GameObject currentInfoUIGo;

    [SerializeField] private GameObject InfoUIPrefab;
    [SerializeField] private Transform InfoUITransform;
    [SerializeField] private Transform playerCameraTransform;

    private void Start()
    {
        currentInfoUIGo = Instantiate(InfoUIPrefab, InfoUITransform);
        currentInfoUI = currentInfoUIGo.GetComponent<CreatureInfoUIElements>();
        creatureInfo = GetComponent<LivingBeing>();
        healthBar = currentInfoUI.creatureHealthBar;
        creatureNameUi = currentInfoUI.creatureName;

        creatureMaxHealth = creatureInfo.healthPoint;
        creatureNameUi.text = creatureInfo.name;
    }

    private void Update()
    {
        UpdateHealthBar();
        LookAtCamera();
    }

    void UpdateHealthBar()
    {
        float currentCreatureHp = creatureInfo.healthPoint;
        healthBar.fillAmount = currentCreatureHp / creatureMaxHealth;
    }

    void LookAtCamera()
    {
        if(currentInfoUI != null)
        {
            if (playerCameraTransform != null)
            {
                currentInfoUIGo.transform.LookAt(playerCameraTransform.position);
                currentInfoUIGo.transform.rotation = Quaternion.Euler(0, currentInfoUIGo.transform.rotation.eulerAngles.y + 180f, 0);
            }
        }
    }
}
