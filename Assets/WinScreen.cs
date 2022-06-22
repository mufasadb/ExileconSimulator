using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WinScreen : MonoBehaviour
{
    public float wonAt;
    public TextMeshProUGUI winText;
    public GameObject standardWinText1;
    public GameObject standardWinText2;
    public GameObject lostText;
    // Start is called before the first frame update
    void Awake()
    {
        wonAt = PlayerPrefs.GetFloat("WonAt");
        if (wonAt == 0) gameObject.SetActive(false);
        else if (wonAt == -1)
        {
            gameObject.SetActive(true);
            standardWinText2.SetActive(false);
            standardWinText1.SetActive(false);
            lostText.SetActive(true);
        }
        else
        {
            gameObject.SetActive(true);
            standardWinText2.SetActive(true);
            standardWinText1.SetActive(true);
            lostText.SetActive(false);
            winText.text = wonAt.ToString() + '\n' + " minutes before the end of Exilecon";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
