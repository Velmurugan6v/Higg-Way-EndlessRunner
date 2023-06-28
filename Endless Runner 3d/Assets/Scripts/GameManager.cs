using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("----------Score----------")]
    [SerializeField] float _score;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] GameObject _gameOverPanel;

    [Header("----------Gold Coin----------")]
    [SerializeField] int _goldCoins;
    [SerializeField] TextMeshProUGUI _goldCoinText;

    [SerializeField] int kiloMeter;
    [SerializeField] TextMeshProUGUI kiloMeterText;

    //For Save high Score and coins and highCoin collected
    [SerializeField] int currentCoin;
    [SerializeField] int highScoreKM;

    //
    [SerializeField] Animator fadeAnimInGameScene;

    //Add Super 
    [SerializeField] int needCoinsForPower;
    [SerializeField] Button topSpeedBtn;

    [Header("----------Start Counter----------")]
    [SerializeField] int countDown;
    [SerializeField] TextMeshProUGUI _countDownText;
    [SerializeField] RectTransform countDownPanel;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentCoin = PlayerPrefs.GetInt("Coins");
        highScoreKM = PlayerPrefs.GetInt("HighScoreKM", 0);

        //add extra feature GameStart Counter
        StartCoroutine(ICounter(countDown, _countDownText));
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControl.instance.isActiveAndEnabled)
        {
            //ScoreAdd();
            TrackKiloMeter();

            needCoinsForPower = _goldCoins;

            if (needCoinsForPower >= 100)
            {
                topSpeedBtn.enabled = true;
            }

            if (kiloMeter > 2000)
            {
                PlayerControl2.instance.CurrentSpeed = 5000;
            }
            else if (kiloMeter > 1500)
            {
                PlayerControl2.instance.CurrentSpeed = 4000;
            }
            else if (kiloMeter > 1100)
            {
                PlayerControl2.instance.CurrentSpeed = 3000;
            }
            else if (kiloMeter > 900)
            {
                PlayerControl2.instance.CurrentSpeed = 2400;
            }
            else if (kiloMeter > 700)
            {
                PlayerControl2.instance.CurrentSpeed = 2000;
            }
            else if (kiloMeter > 400)
            {
                PlayerControl2.instance.CurrentSpeed = 1800;
            }
            else if (kiloMeter > 250)
            {
                PlayerControl2.instance.CurrentSpeed = 1500;
            }
            else if (kiloMeter > 100)
            {
                PlayerControl2.instance.CurrentSpeed = 1300;
            }
        }
    }

    void ScoreAdd()  // Add Score 
    {
        _score += Time.deltaTime * 10;
        _scoreText.text = "" + Mathf.RoundToInt(_score);

        if (_score > highScoreKM)
        {
            highScoreKM = (int)_score;
            PlayerPrefs.SetInt("HighScoreKM", highScoreKM);
        }
    }

    public void GoldCoinAdd()  //Add Coins
    {
        _goldCoins++;
        _goldCoinText.text = "" + _goldCoins;
        PlayerPrefs.SetInt("Coins", _goldCoins + currentCoin);
    }

    public void TrackKiloMeter()  //Calculate Kilometer
    {
        kiloMeter = (int)PlayerControl.instance.transform.position.z;
        kiloMeterText.text = "" + kiloMeter;

        if (kiloMeter > 1000)
        {
            float km = Mathf.FloorToInt(kiloMeter / 1000);
            float meter = Mathf.FloorToInt(kiloMeter % 1000)/100;

            kiloMeterText.text = string.Format("{0}.{1}", km, meter);
        }

        if (kiloMeter > highScoreKM)
        {
            highScoreKM = kiloMeter;
            PlayerPrefs.SetInt("HighScoreKM", highScoreKM);
        }
    }

    private IEnumerator ICounter(int count, TextMeshProUGUI counterText) //CountDown For GameStart
    {
        counterText.text = "" + count; //3
        count--;      
        yield return new WaitForSeconds(1);
        counterText.text = "" + count; //2
        count--;
        yield return new WaitForSeconds(1);
        counterText.text = "" + count; //1
        count--;
        yield return new WaitForSeconds(1);
        counterText.text = "" + count; //0
        count--;
        yield return new WaitForSeconds(1); 
        countDownPanel.DOMoveY(-1000, 1.2f); //move Downside
        GameObject.FindObjectOfType<PlayerControl>().enabled = true;
        GameObject.FindObjectOfType<PlayerControl2>().enabled = true;
    }

    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
        PlayerControl.instance.gameObject.SetActive(false);
    }

    //Scene Management
    public void GoToMainMenu()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        StartCoroutine(IenumeratorMainMenu());
    }

    private IEnumerator IenumeratorMainMenu()
    {
        fadeAnimInGameScene.Play("FadeOutGameScene");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
    public void RestartGame()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


//Demo For leanr Extention Method
public static class StartCounter
{
    public static void GameStartCounter(this GameManager mainPlayer,int count,TextMeshProUGUI counterText)
    {
        
    }
}
