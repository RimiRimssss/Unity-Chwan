using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public int requiredCoins = 10;
    public TextMeshProUGUI coinText;
    public GameObject levelCompletePanel;
    private CoinCollection coinCollection;

    void Start()
    {
        coinCollection = FindObjectOfType<CoinCollection>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (coinCollection.Coin < requiredCoins)
            {
                coinText.text = "Need " + (requiredCoins - coinCollection.Coin) + " more coins to proceed";
                coinText.enabled = true;
            }
            else
            {
                levelCompletePanel.SetActive(true);
                coinText.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            coinText.enabled = false;
        }
    }
}