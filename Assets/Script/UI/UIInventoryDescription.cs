using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Inventory.UI
{
    public class UIInventoryDescription : MonoBehaviour
    {
        [SerializeField] private Image itemImage;    // 아이템 이미지
        [SerializeField] private TMP_Text title;    // 아이템 이름
        [SerializeField] private TMP_Text description;  // 아아템 설명
        [SerializeField] private Image buttonImage; // 버리기, 사용하기 버튼 패널

        private void Awake()
        {
            ResetDescription(); // 시작시 설명창 비활성화
        }

        public void ResetDescription()  // 설명창 비활성화
        {
            this.itemImage.gameObject.SetActive(false);
            this.title.text = "";
            this.description.text = "";
            this.buttonImage.gameObject.SetActive(false);
        }

        public void SetDescription(Sprite sprite, string itemName, string itemDescription) // 설명창 활성화
        {
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;

            this.title.text = itemName;
            this.description.text = itemDescription;
            this.buttonImage.gameObject.SetActive(true);
        }
    }

}
