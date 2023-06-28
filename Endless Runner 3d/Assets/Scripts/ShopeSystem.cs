using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopeSystem : MonoBehaviour
{
    [Header("----------Shope Items----------")]

    public List<GameObject> CarObjList;
    public int currentCarIndex;

    //Buy items in Shop
    public List<CarBase> Cars;
    public Button buyBtn;
    public Image isLockedImage;

    public void CarSetup()
    {
        currentCarIndex = PlayerPrefs.GetInt("SelectedCar", 0);

        foreach (GameObject car in CarObjList)
        {
            car.SetActive(false);
        }

        CarObjList[currentCarIndex].SetActive(true);
    }

    public void NextCar()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        CarObjList[currentCarIndex].SetActive(false);

        currentCarIndex++;

        //Incase User Swipe nunber increase automatical reset
        if (currentCarIndex > CarObjList.Count - 1)
        {
            currentCarIndex = 0;
        }

        CarObjList[currentCarIndex].SetActive(true);

        CarBase car = Cars[currentCarIndex];

        if (!car.isUnlocked)
        {
            return;
        }

        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
    }


    public void PreviousCar()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        CarObjList[currentCarIndex].SetActive(false);

        currentCarIndex--;

        //Incase User Swipe nunber increase automatical reset
        if (currentCarIndex < 0)
        {
            currentCarIndex = CarObjList.Count - 1;
        }

        CarObjList[currentCarIndex].SetActive(true);

        CarBase car = Cars[currentCarIndex];

        if (!car.isUnlocked)
        {
            return;
        }

        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
    }


    public void CheckCarStatus()
    {
        foreach (var car in Cars)
        {
            if (car.price == 0)
            {
                car.isUnlocked = true;
            }
            else
            {
                car.isUnlocked = PlayerPrefs.GetInt(car.name, 0) == 0 ? false : true;
            }
        }
    }

    public void UpdateUI()
    {
        CarBase car = Cars[currentCarIndex];

        if (car.isUnlocked)
        {
            buyBtn.gameObject.SetActive(false);
            isLockedImage.enabled = false;
        }
        else
        {
            isLockedImage.enabled = true;
            buyBtn.gameObject.SetActive(true);
            buyBtn.GetComponentInChildren<TextMeshProUGUI>().text = "" + car.price;

            if (PlayerPrefs.GetInt("TotalCoins") >= car.price)  //This line take decision, if coins is equal or more than car price
            {
                buyBtn.interactable = true;
            }
            else
            {
                buyBtn.interactable = false;
            }
        }
    }

    public void UnLockCar()
    {
        AudioManager.instance.PlaySFX("CarUnlock");
        CarBase car = Cars[currentCarIndex];
        PlayerPrefs.SetInt(car.name, 1);   //This line mentioned car isUnlocked is true
        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
        car.isUnlocked = true;
        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins", 0) - car.price);   //Reduce coins for purchased car
        FindObjectOfType<MainMenu>().UpdataShopeUI(PlayerPrefs.GetInt("TotalCoins"));
    }
}
