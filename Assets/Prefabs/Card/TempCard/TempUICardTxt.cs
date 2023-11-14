using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TempUICardTxt : MonoBehaviour
{
    public CardSO cardSO;   // 카드 정보
    public Image tempcardImage;    // 아이템 이미지
    public TMP_Text tempcardtitle;    // 아이템 이름
    public TMP_Text tempcarddescription;  // 아아템 설명


    private void Awake()
    {
        TempCardResetDescription(); // 시작시 설명창 비활성화
    }

    public void TempCardResetDescription()  // 설명창 비활성화
    {
        this.tempcardImage.gameObject.SetActive(false);
        this.tempcardtitle.text = "";
        this.tempcarddescription.text = "";
    }

    public void TempCardSetDescription(CardSO cardSO, Sprite sprite, string itemName, string itemDescription) // 설명창 활성화
    {
        this.cardSO = cardSO;
        this.tempcardImage.gameObject.SetActive(true);
        this.tempcardImage.sprite = sprite;

        this.tempcardtitle.text = itemName;
        this.tempcarddescription.text = itemDescription;
    }
}
