using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;

public class MouseFollwer : MonoBehaviour
{
    [SerializeField] private Canvas canvas; // canvas 오브젝트
    [SerializeField] private UIInventoryItem item;  // 드래그 될 아이템 

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<UIInventoryItem>();
    }

    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);    // 아이템 이미지와 개수 가져오기
    }

    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(     // 좌표값 변환을 위한 함수
            (RectTransform)canvas.transform,    // 마우스 포인트 출력할 좌표
            Input.mousePosition,    // 마우스 포인터 현재 위치
            canvas.worldCamera,     // 좌표와 연결된 카메라
            out position);          // 변환되는 좌표를 저장

        transform.position = canvas.transform.TransformPoint(position); // 마우스 좌표 = 캔버스 내의 출력 좌표
    }

    public void Toggle(bool val)
    {
        {
            //Debug.Log($"Item toggled {val}");
            gameObject.SetActive(val);  // true면 이미지 활성화, flase면 비활성화
        }
    }
}
