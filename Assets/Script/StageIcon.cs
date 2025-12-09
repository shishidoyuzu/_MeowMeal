using System;
using UnityEngine;
using UnityEngine.UI;

public class StageIcon : MonoBehaviour
{
    public Image iconImage;
    public GameObject lockIcon;
    // 未開放確認パネル
    public GameObject LockedPopup_Panal;

    // 選択状態でフワフワさせる
    public bool animate = false;
    float animTime = 0f;

    public void Setup(bool isSelected, bool isUnlocked)
    {
        // --- 選択されている？ ---
        animate = isSelected;
        transform.localScale = isSelected ? Vector3.one * 1.1f : Vector3.one * 0.7f;

        // --- アンロックされてる？ ---
        iconImage.color = isUnlocked ? Color.white : new Color(0.6f, 0.6f, 0.6f);
        lockIcon.SetActive(!isUnlocked);

        if (!isUnlocked)
        {
            //LockedPopup_Panal.SetActive(true);
            return;
        }

        // 開放済みならステージ変更
        //StageSelectManager.SelectStageNum = myIndex;
        //StageSelectManager.instance.SetStageUI();
    }

    void Update()
    {
        if (!animate) return;

        animTime += Time.deltaTime * 2f;
        float scale = 1.1f + Mathf.Sin(animTime) * 0.05f;
        transform.localScale = new Vector3(scale, scale, 1);
    }
}