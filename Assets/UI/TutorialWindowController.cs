using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialWindowController : MonoBehaviour
{
    public TextMeshProUGUI tmPro;
    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
