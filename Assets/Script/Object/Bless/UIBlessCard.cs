using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBlessCard : MonoBehaviour
{
    [SerializeField] private GameObject cardObj;   // 아이템 
    [SerializeField] private Image cardImage;   // 아이템 이미지
    [SerializeField] private TMP_Text cardName;  // 개수 텍스트
    [SerializeField] private TMP_Text cardDescription;  // 개수 텍스트

    //private bool empty = true;

    private void Awake()
    {
        ResetData();    // 인벤토리 정보 초기화
    }

    public void ResetData() // 인벤토리 정보 초기화
    {
        this.cardImage.gameObject.SetActive(false); // 인벤토리의 item데이터가 없어지면 비활성화
        //empty = true;
        this.cardObj.SetActive(false);  // 카드 정보가 없을때
    }


    public void SetData(Sprite sprite, string name, string desc)    // itemUI에 아이템 데이터 가져오기
    {
        this.cardImage.gameObject.SetActive(true);
        this.cardImage.sprite = sprite;

        this.cardName.text = name.ToString() + "";
        this.cardDescription.text = desc.ToString() + "";
        //empty = false;
        this.cardObj.SetActive(true);   // 카드 정보가 있을때
    }

 
}

