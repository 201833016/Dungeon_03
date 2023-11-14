using System.Collections;
using System.Collections.Generic;
using Inventory.UI;
using Inventory.Model;
using UnityEngine;
using Inventory;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject minimap_Tab;
    [SerializeField] private InventorySO inventorySO;
    [SerializeField] public GameObject UIInventoryPage;
    [SerializeField] private BossCanvas bossCanvas;
    [SerializeField] private Health health; // 효과 대상이 될 플레이어
    private int AcceptItemNum;  // 아이템UI의 순번 받아오는 변수
    private UIInventoryPage ItemSlotNum;  // 인벤토리에서 클릭한 아이템 순번 변수
    public GameObject description_Button_Panel; // 버프 아이템 사용시 버리기/사용하기 숨기기
    
    [SerializeField] private GameObject inGameMenu;    // 게임 메뉴
    private bool isOnOff;      // 게임메뉴 On Off 확인
    InventoryController invenCon;
    BlessCardController blessCon;
    StatController statCon;
    [SerializeField]private Player player;

    private void Start()
    {
        ItemSlotNum = UIInventoryPage.GetComponent<UIInventoryPage>();
        invenCon = GetComponent<InventoryController>();
        blessCon = GetComponent<BlessCardController>();
        statCon = GetComponent<StatController>();
        
        minimap_Tab.SetActive(false);
    }

    private void Update()
    {
        AcceptItem();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (minimap_Tab.activeSelf)
            {
                minimap_Tab.SetActive(false);
            }
            else
            {
                minimap_Tab.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!bossCanvas.playerin && !player.Overbool)   // 보스 방 애니메이션 실행시 || 게임 오버시 메뉴 창 안열리게
            {
                Debug.Log("인벤토리 메뉴 열기 / 닫기");
                OnOffInGameMenu();  // ESC 게임 메뉴 열기, 닫기
            }
            
        }
    }

    private void OnOffInGameMenu()  // 인게임 메뉴 개폐
    {
        if (isOnOff == false)   // 메뉴 열기
        {
            inGameMenu.SetActive(true); // 메뉴 활성화
            invenCon.invenUI.Hide();
            blessCon.bCardUI.Hide();
            statCon.statUI.Hide();
            isOnOff = true;
            Time.timeScale = 0; // 일시정지
        }
        else    // 메뉴 닫기
        {
            inGameMenu.SetActive(false);    // 메뉴 비활성화
            isOnOff = false;
            Time.timeScale = 1f;    // 일시정지 해제
        }
    }

    public void TestHide()
    {
        inGameMenu.SetActive(false);    // 메뉴 비활성화
        isOnOff = false;
        Time.timeScale = 1f;    // 일시정지 해제
    }


    private void AcceptItem()
    {
        if (ItemSlotNum != null)
        {
            AcceptItemNum = ItemSlotNum.selectItemSlotNum;  // 클릭한 아이템 순번 가져옴
        }
    }

    public void Dropitem(int index)     // 인벤토리에서 아이템 버리기 요청
    {
        index = AcceptItemNum;
        InventoryItem inventoryItem = inventorySO.GetItemAt(index); // index부분을 아이템 번호가 올수 있도록
        if (inventoryItem.IsEmpty)
        {
            return;
        }

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;  // 음식 아이템 사용시
        if (destroyableItem != null)
        {
            inventorySO.DumpItem(index, 1);   // 해당 번호 아이템갯수 1개 감소
            Debug.Log("임시 버리기");
        }
    }

    public void UseItem(int index)  // 인벤토리에서 아이템 사용 요청
    {
        index = AcceptItemNum;
        InventoryItem inventoryItem = inventorySO.GetItemAt(index);
        if (inventoryItem.IsEmpty)
        {
            return;
        }

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;  // 아이템 사용시
        if (destroyableItem != null)
        {
            inventorySO.RemoveItem(index, 1);   // 해당 번호 아이템갯수 1개 감소
            //Debug.Log("01: 아이템사용");
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            switch (inventoryItem.item.item_type)
            {
                case "ATK":     // ATK 아이템 사용시
                    //basebuff.onATK = true;
                    description_Button_Panel.SetActive(false);
                    BuffUseYN.instance.DEFEnabled(false);
                    BuffUseYN.instance.ATKEnabled(true);

                    BuffManager.instance.onATK = true;
                    break;
                case "DEF":     // DEF 아이템 사용시
                    //basebuff.onATK = true;
                    description_Button_Panel.SetActive(false);
                    BuffUseYN.instance.ATKEnabled(false);
                    BuffUseYN.instance.DEFEnabled(true);
                    BuffManager.instance.onDEF = true;
                    break;
                default:
                    description_Button_Panel.SetActive(true);
                    BuffUseYN.instance.ATKEnabled(false);
                    BuffUseYN.instance.DEFEnabled(false);

                    break;
            }
            itemAction.PerformAction(health, inventoryItem.itemState);  // 해당 값만큼 사용
            Debug.Log($"5. 종류 : {inventoryItem.item.item_type}");
        }
    }
}
