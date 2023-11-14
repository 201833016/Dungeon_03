using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

public class StatController : MonoBehaviour
{
    
    public UIStatPage statUI;   // 스테이터스 메뉴
    InventoryController inventoryController;
    BlessCardController cardController;
    public int AAA;
    private void Awake() {
        inventoryController = GetComponent<InventoryController>();
        cardController = GetComponent<BlessCardController>();
        AAA = 0;
    }

    public void OnOffStatMenu() // 스텟 메누 열기, 닫기
    {
        if (statUI.isActiveAndEnabled == false)
        {
            inventoryController.invenUI.Hide(); // 아이템 인벤토리 안보이게
            cardController.bCardUI.Hide(); // 축복 메뉴 안보이게;
            statUI.Show();
            UIPlayerStat.instance.StartStat();
        }
        else
        {
            statUI.Hide();
        }
    }
    
}
