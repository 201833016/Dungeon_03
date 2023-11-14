using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessCardPage : MonoBehaviour
{
    [SerializeField] private UIBlessCard bCardPrefab;    // 아이템UI 프리팹 
    [SerializeField] private RectTransform contentPanel;    // 아이템 UI가 놓일 공간
    List<UIBlessCard> listofUICards = new List<UIBlessCard>();  // 마우스 포인터

    public void InitializeBlessCardUI(int bCardInvenSize)    // 인벤토리의 아이템 칸 생성
    {
        for (int i = 0; i < bCardInvenSize; i++)
        {
            UIBlessCard uiCard = Instantiate(bCardPrefab, Vector3.zero, Quaternion.identity) as UIBlessCard;
            uiCard.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            uiCard.transform.SetParent(contentPanel);
            listofUICards.Add(uiCard);
        }
    }

    public void UpdateCardData(int cardIndex, Sprite cardImage, string cardName, string cardDes)   // 인벤토리 아이템 데이터 상시 업데이트
    {
        if (listofUICards.Count > cardIndex)
        {
            listofUICards[cardIndex].SetData(cardImage, cardName, cardDes);  // itemUI에 아이템 데이터 가져오
        }
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
