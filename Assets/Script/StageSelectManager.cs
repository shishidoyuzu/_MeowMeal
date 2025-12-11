using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    // 未開放確認パネル
    public GameObject LockedPopup_Panel;

    [Header("ステージアイコン")]
    public List<StageIcon> StageIcons;
    // スクロールビュー（ステージアイコン）
    private Scrollbar scrollbar;

    [Header("開放パネルの表示時間")]
    // 開放パネルの表示時間
    public float DisplayTime = 1.5f;
    // 計測時間
    private float timer;


    IEnumerator Start()
    {
        yield return null;  // 1 フレーム待つ

        int stage = SelectStageNum;

        // スクロールバーの捜索＆取得
        scrollbar = GameObject.Find("Scrollbar Horizontal").GetComponent<Scrollbar>();

        MoveScrollview();

        // UIのセット＆更新
        SetStageUI(SelectStageNum);
        UpdateStageIcons(SelectStageNum);

        // タイマーの初期化
        timer = 0.0f;
    }
    void Update()
    {
        if (LockedPopup_Panel.activeSelf)
        {
            timer += Time.deltaTime; // 計測開始
            if (timer >= DisplayTime)
            {
                // 開放パネルの非表示
                LockedPopup_Panel.SetActive(false);
                // タイマーを初期化
                timer = 0.0f;
            }
        }
    }
    

    public void UpdateStageIcons(int sNum)
    {
        for (int i = 0; i < StageIcons.Count; i++)
        {
            var icon = StageIcons[i];
            
            // 選択されているステージと値が同じとき、「選択」されている
            bool isSelected = (i == sNum);
            // tureがあれば「開放」されている
            bool isUnlocked = ProgressManager.instance.IsUnlocked(i);
            StageIcons[i].Setup(isSelected, isUnlocked);
        }
    }

    public void SetStageUI(int sNum)
    {
        // SelectStageNumの更新
        var data = AllStageData[sNum];

        //Debug.Log($"{sNum + 1}");

        // UIの更新
        // ｘ日目のテキスト
        Date_Text.text = ($"{sNum + 1}日目");
        // ステージ説明テキスト
        StageDescription_Text.text = data.StageExplanation;
        // 出現するネコ一覧
        CatList_Text.text = string.Join("\n", data.CatName);
        // 解放されてるなら PLAY ボタン可
        PlayButton.interactable = ProgressManager.instance.IsUnlocked(sNum);
    }

    public void TryMoveStage(int direction)
    {
        int next = SelectStageNum + direction;

        // 範囲外なら無視
        if (next < 0 || next >= AllStageData.Count)
            return;

        // 次のステージがロックされてるならポップアップ
        if (!ProgressManager.instance.IsUnlocked(next))
        {
            LockedPopup_Panel.SetActive(true);
            return;
        }

        // ロックされていないなら移動
        SelectStageNum = next;
        SetStageUI(SelectStageNum);
        UpdateStageIcons(SelectStageNum);
        MoveScrollview();
    }

    public void MoveScrollview()
    {
        switch ((SelectStageNum + 1))
        {   
            case 1:
                Debug.Log("ステージ１を選択中");
                scrollbar.value = 0.0f;
                break;
            case 2:
                Debug.Log("ステージ２を選択中");
                scrollbar.value = 0.25f;
                break;
            case 3:
                Debug.Log("ステージ３を選択中");
                scrollbar.value = 0.5f;
                break;
            case 4:
                Debug.Log("ステージ４を選択中");
                scrollbar.value = 0.75f;
                break;
            case 5:
                Debug.Log("ステージ５を選択中");
                scrollbar.value = 1.0f;
                break;
        }
    }
}