using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    /*
    ～ステージアイコンの動き～
    ・選択UIの真ん中（x座標：230程度）に選択状態のアイコンを配置
    ・選択状態になればふわふわするアニメーションをつける
    ・選択状態になっていないアイコンはScaleを少し小さく（0.7くらい）し
    ・まだプレイしたことない（解放されていない）ステージは「鍵」を表示し、暗くする

    ～選択UIの動き～
    ・クリックするとStageDataのStageNumを参照して、SetStageUI()を呼び出し
    ・

    ～ステージパネルの動き～
    ・選択されているアイコンによってパネルの情報が切り替わる（StageDataに連動）
    ・変わるのは「ｘ日目」テキスト・ステージ説明テキスト・出てくるねこの種類テキストの３つ
    ・選択状態かつ解放済みなら「プレイ！」ボタンが光る。
    
    */


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
    // やって来るねこ
    public TextMeshProUGUI CatList_Text;
    // 「プレイ！」ボタン
    public Button PlayButton;

    [Header("ステージアイコン")]
    public List<StageIcon> StageIcons;

    [Header("アンロック情報（解放フラグ）")]
    public List<bool> UnlockFlags = new List<bool>() { true, false, false, false, false };


    // Start is called before the first frame update
    void Start()
    {
        SetStageUI();
        UpdateStageIcons();
    }

    public void UpdateStageIcons()
    {
        for (int i = 0; i < StageIcons.Count; i++)
        {
            var icon = StageIcons[i];

            bool isSelected = (i == SelectStageNum);
            bool isUnlocked = UnlockFlags[i];

            StageIcons[i].Setup(isSelected, isUnlocked);
        }
    }

    public void SetStageUI()
    {
        // SelectStageNumの更新
        var data = AllStageData[SelectStageNum];

        Debug.Log($"{SelectStageNum + 1}");

        // UIの更新
        // ｘ日目のテキスト
        Date_Text.text = ($"{SelectStageNum + 1}日目");
        // ステージ説明テキスト
        StageDescription_Text.text = data.StageExplanation;
        // 出現するネコ一覧
        CatList_Text.text = string.Join("\n", data.CatName);
        // 解放されてるなら PLAY ボタン可
        PlayButton.interactable = UnlockFlags[SelectStageNum];
    }
}