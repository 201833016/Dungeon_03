using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Inventory.Model;

[CreateAssetMenu(fileName = "Drop table", menuName = "Not need/Drop Table")]
public class SOItemDropTable : ScriptableObject // 몬스터 드랍 테이블
{
    [System.Serializable]
    public class Items
    {
        public GameObject item;
        public int weight;
    }

    public List<Items> items = new List<Items>();

    private GameObject PickItem()
    {
        int sum = 0;
        foreach(var item in items)
        {
            sum += item.weight;
        }
        var rnd = Random.Range(0, sum);
        for(int i = 0; i < items.Count; i++)
        {
            var item = items[i];
            if(item.weight > rnd)
            {
                return items[i].item;
            }
            else
            {
                rnd -= item.weight;
            }
        }
        return null;
    }

    public void ItemDrop(Vector3 pos)
    {
        var item = PickItem();
        if(item == null)
        {
            return;
        }
        GameObject itemDrop =  Instantiate(item, pos, Quaternion.identity);     // 위치에 아이템 드랍

    }
}
