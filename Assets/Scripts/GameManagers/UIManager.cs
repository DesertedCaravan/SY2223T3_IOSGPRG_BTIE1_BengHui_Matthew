using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Screens")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject gameElements;

    [Header("Game Screen Display")]
    [SerializeField] private TextMeshProUGUI highScoreDisplay;
    [SerializeField] private TextMeshProUGUI survivorsDisplay;

    [SerializeField] private TextMeshProUGUI medKitDisplay;

    [SerializeField] private TextMeshProUGUI currentClipDisplay;
    [SerializeField] private TextMeshProUGUI excessAmmoDisplay;

    [SerializeField] private TextMeshProUGUI pistolAmmoDisplay;
    [SerializeField] private TextMeshProUGUI automaticRifleAmmoDisplay;
    [SerializeField] private TextMeshProUGUI shotgunAmmoDisplay;

    [SerializeField] private GameObject primaryWeaponDisplay;
    [SerializeField] private GameObject secondaryWeaponDisplay;

    [SerializeField] private Sprite pistolSprite;
    [SerializeField] private Sprite automaticRifleSprite;
    [SerializeField] private Sprite shotgunSprite;

    [SerializeField] private TextMeshProUGUI winStateScreen;

    [SerializeField] private Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        gameScreen.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(true);
        gameElements.gameObject.SetActive(false);
        GameManager.Instance.ReloadingComplete();

        // No need to change inspector
        GameManager.Instance.OnHighScoreChange.AddListener(UpdateHighScoreDisplay);
        UpdateHighScoreDisplay();

        GameManager.Instance.OnSurvivorsChange.AddListener(UpdateSurvivorsDisplay);
        UpdateSurvivorsDisplay();

        GameManager.Instance.OnMedKitChange.AddListener(UpdateMedKitDisplay);
        UpdateMedKitDisplay();

        GameManager.Instance.OnClipChange.AddListener(UpdateClipDisplay);
        UpdateClipDisplay();

        GameManager.Instance.OnExcessChange.AddListener(UpdateExcessDisplay);
        UpdateExcessDisplay();

        GameManager.Instance.OnAmmoChange.AddListener(UpdateAmmoDisplay);
        UpdateAmmoDisplay();
        
        GameManager.Instance.OnWinStateChange.AddListener(WinStateDisplay);
        WinStateDisplay();
    }

    public void UpdateHighScoreDisplay()
    {
        highScoreDisplay.SetText(string.Format("Score: {0}", GameManager.Instance._highScore));
    }

    public void UpdateSurvivorsDisplay()
    {
        survivorsDisplay.SetText(string.Format("Remain: {0}", GameManager.Instance._survivors));
    }

    public void UpdateMedKitDisplay()
    {
        medKitDisplay.SetText(string.Format("{0}", GameManager.Instance._medKit));
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

    public void WinStateDisplay()
    {
        winStateScreen.SetText(string.Format("{0}", GameManager.Instance._winState));
    }

    public void SetMaxHealth(int health)
    {
        hpSlider.maxValue = health;
        hpSlider.value = health;
    }

    public void SetHealth(int health)
    {
        hpSlider.value = health;
    }

    public void StartGame()
    {
        mainMenu.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        gameScreen.gameObject.SetActive(true);
        gameElements.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        quitButton.gameObject.SetActive(true);
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

    public void ActivateGunSprite(int mode)
    {
        Image primaryImage = primaryWeaponDisplay.gameObject.GetComponent<Image>();
        Image secondaryImage = secondaryWeaponDisplay.gameObject.GetComponent<Image>();

        if (mode == 1)
        {
            secondaryImage.sprite = pistolSprite;
        }
        else if (mode == 2)
        {
            primaryImage.sprite = automaticRifleSprite;
        }
        else if (mode == 3)
        {
            primaryImage.sprite = shotgunSprite;
        }
    }
}
