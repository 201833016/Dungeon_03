using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class BlessCardController : MonoBehaviour
{
    public BlessCardPage bCardUI;   // 축복 카드 메뉴

    [SerializeField] private BlessCardInven cardData;   // 축복 카드 인벤토리 정보
    [SerializeField] private InventoryController inventoryController;   // 메뉴에서 아이템 인벤을 닫기 위한 
    StatController statController;
    private TestLevel testLevel;    // 스테이지 레벨
    private void Awake()
    {
        statController = GetComponent<StatController>();
        testLevel = GameObject.Find("TestLevel").GetComponent<TestLevel>();
        if (testLevel.level == 1)   // 게임 시작시 축복 상태 초기화
        {
            cardData.InitializeCard();
        }
    }

    private void Start()
    {
        PrepareCardUI();
        //cardData.InitializeCard();
    }

    private void PrepareCardUI()    // 시작시 카드 인벤 칸 생성
    {
        bCardUI.InitializeBlessCardUI(cardData.invenCards.Count);
    }



    public void OnOffCardMenu() // 축복 카드 인벤토리 열기, 닫기
    {
        if (bCardUI.isActiveAndEnabled == false)
        {
            bCardUI.Show();
            inventoryController.invenUI.Hide(); // 아이템 인벤토리 안보이게
            statController.statUI.Hide();
            foreach (var card in cardData.GetCurrentBlessCardState())
            {
                bCardUI.UpdateCardData(card.Key, card.Value.card.cardImage, card.Value.card.cardName, card.Value.card.cardDescription);

            }
        }
        else
        {
            bCardUI.Hide();
        }
    }
}
