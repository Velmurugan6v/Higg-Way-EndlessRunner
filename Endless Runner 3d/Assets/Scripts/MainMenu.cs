using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : ShopeSystem
{
    [Header("----------Main Menu UI----------")]
    [SerializeField] RectTransform mainMenuPanel;
    [SerializeField] RectTransform quitGamePanel;
    [SerializeField] RectTransform optionPanel;
    [SerializeField] RectTransform shopPanel;

    //Store Position of RectTrans
    [SerializeField] Vector2 mainMenuStartPos;
    [SerializeField] Vector2 quitPanelStartPos;
    [SerializeField] Vector2 optionPanelStartPos;
    [SerializeField] Vector2 shopPanelStartPos;
    [SerializeField] Vector3 cameraStartPos;
    [SerializeField] Quaternion cameraStartRotation;
    [SerializeField] Transform desiredPos;

    
    //UI likes coins and highscore
    public int totalCoins;
    [SerializeField] int currentCoin;
    [SerializeField] int highScoreKM;
    [SerializeField] TextMeshProUGUI totalCoinsText;
    [SerializeField] TextMeshProUGUI highScoreText;

    //Animator for fade in and out
    [SerializeField] Animator fadeAnim;

    //Music Slier Control
    [SerializeField] Slider musiceSlider;
    [SerializeField] Slider sfxSlider;

    private void Awake()
    {
        mainMenuStartPos = mainMenuPanel.anchoredPosition;
        quitPanelStartPos = quitGamePanel.anchoredPosition;
        optionPanelStartPos = optionPanel.anchoredPosition;
        shopPanelStartPos = shopPanel.anchoredPosition;
        cameraStartPos = Camera.main.transform.position;
        cameraStartRotation = Camera.main.transform.rotation;
    }

    private void Start()
    {
        CarSetup();
        CheckCarStatus();

        //Store
        highScoreKM = PlayerPrefs.GetInt("HighScoreKM");
        totalCoins = PlayerPrefs.GetInt("TotalCoins");
        currentCoin = PlayerPrefs.GetInt("Coins");
        totalCoins += currentCoin;
        currentCoin = 0;
        PlayerPrefs.SetInt("Coins", currentCoin);
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        //Show
        highScoreText.text = highScoreKM.ToString();
        totalCoinsText.text = "" + totalCoins;

    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdataShopeUI(int coins)
    {
        totalCoins = coins;
        totalCoinsText.text = "" + totalCoins;
    }
    //MainMenu UI Animation by DoTween
    public void HideMainMenuPanel()
    {
        mainMenuPanel.DOAnchorPosX(1500, 0.2f);
    }

    public void ShowMainMenuPanel()
    {
        mainMenuPanel.DOAnchorPos(mainMenuStartPos, 0.4f);
    }

    //Options

    public void HideOptionPanel()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        optionPanel.DOAnchorPos(optionPanelStartPos, 0.4f);
        ShowMainMenuPanel();
    }

    public void ShowOprionPanel()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        optionPanel.DOAnchorPos(Vector2.zero, 0.4f);
        HideMainMenuPanel();
    }

    //Music Slide

    public void MusicAdjes()
    {
        AudioManager.instance.MusicVolume(musiceSlider.value);
    }

    public void SFXAdjase()
    {
        AudioManager.instance.SFXVolume(sfxSlider.value);
    }

    //Shope

    public void ShowShopPanel()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        HideMainMenuPanel();

        //Camera move to carPos
        Camera.main.transform.DOMove(desiredPos.position, 0.3f);
        Camera.main.transform.DOLocalRotate(desiredPos.localEulerAngles, 0.1f);
        shopPanel.DOAnchorPos(Vector2.zero, 0.4f);
    }

    public void HideShopPanel()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        Camera.main.transform.DOMove(cameraStartPos, 0.3f);
        Camera.main.transform.DORotateQuaternion(cameraStartRotation, 0.1f);
        shopPanel.DOAnchorPos(shopPanelStartPos, 0.4f);
        ShowMainMenuPanel();
    }

    //Qiut 
    public void ShowQuitPanel()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        HideMainMenuPanel();
        quitGamePanel.DOAnchorPos(Vector2.zero, 0.2f).SetDelay(0.2f);
    }

    public void YesToQuit()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        Application.Quit();
    }

    public void NoToQiut()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        quitGamePanel.DOAnchorPos(quitPanelStartPos, 0.4f);
        ShowMainMenuPanel();
    }

    //Fade in and Out Animation
    IEnumerator GameStartWithFade()
    {
        fadeAnim.Play("Fade Out");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }


    //SceneManager
    public void GameStart()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        StartCoroutine(GameStartWithFade());
    }


}
