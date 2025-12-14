using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject upgradeTurrentPanel;
    [SerializeField]
    private Player playerInfo;
    [SerializeField]
    private UIManager uiManager;

    private Turrent currentTurrent;

    private Transform speed_Upgrade_Panel;

    TextMeshProUGUI Upgrade_Speed_Number_Text;
    public void UpgradeSpeedTurrent()
    {
        Debug.Log("Upgrade Speed Turrent");
        if (playerInfo.Coin >= currentTurrent.SpeedUpgradeCost)
        {
            Debug.Log("Upgrade Speed Turrent Success");
            playerInfo.Coin -= currentTurrent.SpeedUpgradeCost;

            currentTurrent.FireRate += 0.2f;

            currentTurrent.SellValue += currentTurrent.SellValue / 2;

            currentTurrent.SpeedUpgrades += 1;

            UpdateText();
        }
        else
        {
            Debug.Log("Not enough coins to upgrade!");
            StartCoroutine(uiManager.DisplayError("Not enough coins to upgrade!"));
        }
    }

    private void Awake()
    {
       
        speed_Upgrade_Panel = upgradeTurrentPanel.transform.Find("Speed_Upgrade_Panel");

        Upgrade_Speed_Number_Text = speed_Upgrade_Panel.Find("Upgrade_Number_Text").GetComponent<TextMeshProUGUI>();
        Upgrade_Speed_Number_Text.text = "0";

        upgradeTurrentPanel.SetActive(false);
    }

    public void OpenTurrentUI(Turrent turrent)
    {
        upgradeTurrentPanel.SetActive(true);
        currentTurrent = turrent;
        currentTurrent.RangeGO.SetActive(true); // Show range indicator when UI is open
    }

    public void CloseTurrentUI()
    {
        upgradeTurrentPanel.SetActive(false);
        currentTurrent.RangeGO.SetActive(false); // Show range indicator when UI is open
        currentTurrent = null;
       
    }

    private void UpdateText()
    {
        Upgrade_Speed_Number_Text.text = currentTurrent.SpeedUpgrades.ToString();
    }
}
