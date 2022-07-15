using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{   
    public int cheddarAmount = 0;
    private int cheddarMax;
    public GameObject cheddarPopup;
    public GameObject cheddarPopupTextObject;
    private TextMeshProUGUI cheddarPopupText;

    private Animator cheddarPopupAnimator;
    public GameObject cheddarCount;
    private TextMeshProUGUI cheddarCountText;
    private void Start() {
        cheddarPopupAnimator = cheddarPopup.GetComponent<Animator>();
        cheddarPopupText = cheddarPopupTextObject.GetComponent<TextMeshProUGUI>();
        cheddarCountText = cheddarCount.GetComponent<TextMeshProUGUI>();
        cheddarMax = GameObject.FindGameObjectsWithTag("Cheddar").Length;
        cheddarCountText.text = $"{cheddarAmount} / {cheddarMax}";
    }

    public void GetCheddar() {
        cheddarAmount += 1;
        cheddarPopupText.text = $"CHEDDAR GET! \n {cheddarAmount} / {cheddarMax}";
        cheddarCountText.text = $"{cheddarAmount} / {cheddarMax}";
        cheddarPopupAnimator.SetTrigger("ShowCheddarGet");
    }
}
