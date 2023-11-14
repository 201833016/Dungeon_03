using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestTimeCheck : MonoBehaviour
{
    public static TestTimeCheck instance;
    private void Awake()
    {
        instance = this;
    }

    public TextMeshProUGUI text_A;
    [SerializeField] private BaseBuff basebuff;

}
