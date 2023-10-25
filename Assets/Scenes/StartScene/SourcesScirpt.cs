using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.IO;

public class SourcesScirpt : MonoBehaviour
{
    public GameObject SourcesMenu;  // 출처 메뉴
    public TextMeshProUGUI sourcesText; // 출처 텍스트
    [SerializeField] private BGMScript bgm;

    private void Awake() {
        Time.timeScale = 1;
        bgm.Start_StandByScreen_BGM_On();
    }

    private void Start()
    {
        DOTween.Init();
        SourcesMenu.transform.localScale = Vector3.one * 0.1f;
        SourcesMenu.SetActive(false);
    }

    public void OnMenu()
    {
        Show(); // 메뉴 판 열기
        TextOn();   // 출처 텍스트 열기
        Debug.Log("출처 열기");
    }

    public void OffMenu()
    {
        OffMenu_Out();
        Debug.Log("출처 닫기");
    }

    private void Show()
    {
        SourcesMenu.SetActive(true);
        var seq = DOTween.Sequence();

        seq.Append(SourcesMenu.transform.DOScale(1.1f, 0.2f));
        seq.Append(SourcesMenu.transform.DOScale(1f, 0.1f));

        seq.Play();
    }

    private void Hide()
    {
        var seq = DOTween.Sequence();

        SourcesMenu.transform.localScale = Vector3.one * 0.2f;

        seq.Append(SourcesMenu.transform.DOScale(1.1f, 0.1f));
        seq.Append(SourcesMenu.transform.DOScale(0.2f, 0.2f));

        // OnComplete 는 seq 에 설정한 애니메이션의 플레이가 완료되면
        // { } 안에 있는 코드가 수행된다는 의미입니다.
        // 여기서는 닫기 애니메이션이 완료된 후 객체를 비활성화 합니다.
        seq.Play().OnComplete(() =>
        {
            SourcesMenu.SetActive(false);
        });
    }

    private void OffMenu_Out()
    {
        var seq = DOTween.Sequence();

        seq.Append(SourcesMenu.transform.DOScale(0.95f, 0.1f));
        seq.Append(SourcesMenu.transform.DOScale(1.05f, 0.1f));
        seq.Append(SourcesMenu.transform.DOScale(1f, 0.1f));

        seq.Play().OnComplete(() =>
        {
            Hide();
        });
    }

    private void TextOn()
    {
        string filePath = "Assets/Scenes/StartScene/Resourcetext.txt";
        ReadTxt(filePath);
    }
    private string ReadTxt(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        string value = "";

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            value = reader.ReadToEnd();
            reader.Close();           
        }

        else
            value = "파일이 없습니다.";
        sourcesText.text = value;
        string aaa = sourcesText.text;
        
        return aaa;
    }
}
