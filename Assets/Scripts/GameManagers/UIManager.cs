using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI currentClipDisplay;
    [SerializeField] private TextMeshProUGUI excessAmmoDisplay;

    [SerializeField] private TextMeshProUGUI pistolAmmoDisplay;
    [SerializeField] private TextMeshProUGUI automaticRifleAmmoDisplay;
    [SerializeField] private TextMeshProUGUI shotgunAmmoDisplay;

    // Start is called before the first frame update
    void Start()
    {
        // No need to change inspector
        GameManager.Instance.OnClipChange.AddListener(UpdateClipDisplay);
        UpdateClipDisplay();

        GameManager.Instance.OnExcessChange.AddListener(UpdateExcessDisplay);
        UpdateExcessDisplay();

        GameManager.Instance.OnAmmoChange.AddListener(UpdateAmmoDisplay);
        UpdateAmmoDisplay();
    }

    public void UpdateClipDisplay()
    {
        currentClipDisplay.SetText(string.Format("{0}", GameManager.Instance._currentClip));
    }

    public void UpdateExcessDisplay()
    {
        excessAmmoDisplay.SetText(string.Format("{0}", GameManager.Instance._currentExcess));
    }

    public void UpdateAmmoDisplay()
    {
        pistolAmmoDisplay.SetText(string.Format("{0}", Inventory.Instance._pistolAmmoCarry));
        automaticRifleAmmoDisplay.SetText(string.Format("{0}", Inventory.Instance._automaticRifleAmmoCarry));
        shotgunAmmoDisplay.SetText(string.Format("{0}", Inventory.Instance._shotgunAmmoCarry));
    }

    public void ActivateGun(int mode, Gun pistol, Gun automaticRifle, Gun shotgun)
    {
        if (mode == 1)
        {
            pistol.gameObject.SetActive(true);
            automaticRifle.gameObject.SetActive(false);
            shotgun.gameObject.SetActive(false);
        }
        else if (mode == 2)
        {
            pistol.gameObject.SetActive(false);
            automaticRifle.gameObject.SetActive(true);
            shotgun.gameObject.SetActive(false);
        }
        else if (mode == 3)
        {
            pistol.gameObject.SetActive(false);
            automaticRifle.gameObject.SetActive(false);
            shotgun.gameObject.SetActive(true);
        }
    }
}
