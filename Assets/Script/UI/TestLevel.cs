using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI leveltext;
    public int level;

    private void Awake() {
        var obj = FindObjectsOfType<TestLevel>();   // 레벨 오브젝트 2개 방지
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
