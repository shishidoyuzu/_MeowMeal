using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    [Header("各ステージデータ")]
    // ステージデータ
    [SerializeField] private StageData StageData;
    // 選択しているステージ
    public static int SelectStageNum;
    // 全StageDataをセットする場所
    public List<StageData> AllStageData;

    [Header("UI")]
    // 「〇日目」のテキスト
    public TextMeshProUGUI Date_Text;
    // ステージ説明テキスト
    public TextMeshProUGUI StageDescription_Text;

    // Start is called before the first frame update
    void Start()
    {
        // UIの用意
        SetStageUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStageUI()
    {
        Date_Text.text = ($"{SelectStageNum}日目");
        StageDescription_Text.text = StageData.StageExplanation;
        StageData = AllStageData[SelectStageNum];
    }

    //----------------------ボタン専用-----------------------------------
    // 右にある選択UI(黄色いの)を押したとき
    void PushNEXT_UI()
    {
        if(SelectStageNum < AllStageData.Count - 1)
        {
            SelectStageNum++;
            SetStageUI();
        }

        /*
        // 選択しているステージ番号が５より下＆０より上なら
        if (SelectStageNum < 5 && SelectStageNum > 0)
        {
            SelectStageNum++;
            SetStageUI();
        }
        */
    }
    // 左にある選択UI(黄色いの)を押したとき
    void PushPREV_UI()
    {
        if(SelectStageNum > 0)
        {
            SelectStageNum--;
            SetStageUI();
        }

        /*
        // 選択しているステージ番号が０より上＆６より下なら
        if (SelectStageNum > 0 && SelectStageNum < 6)
        {
            SelectStageNum--;
            SetStageUI();
        }
        */
    }
    //-------------------------------------------------------------------
}
