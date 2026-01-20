using UnityEngine;
using UnityEngine.UI;

public class SettingSoundUI : MonoBehaviour
{
    [Tooltip("BGMボリュームバー")]
    [SerializeField] private Slider bgmSlider;
    [Tooltip("SEボリュームバー")]
    [SerializeField] private Slider seSlider;

    void Start()
    {
        // シーンに存在する SoundManager を取得
        SoundManager sm = FindObjectOfType<SoundManager>();
        if (sm == null) return; // SoundManager がない場合、処理しない

        // 登録していたものを削除する
        bgmSlider.onValueChanged.RemoveAllListeners();
        seSlider.onValueChanged.RemoveAllListeners();

        // Listenerを先に登録
        bgmSlider.onValueChanged.AddListener(sm.SetBGMVol);
        seSlider.onValueChanged.AddListener(sm.SetSEVol);

        // MAX表示に揃える
        bgmSlider.SetValueWithoutNotify(1f);
        seSlider.SetValueWithoutNotify(1f);

        /* 不要な要素
        // 保存している値を再度、反映させる
        //float bgmVol = PlayerPrefs.GetFloat("BGM_VOLUME", 0f);
        //float seVol = PlayerPrefs.GetFloat("SE_VOLUME", 0f);
        //bgmSlider.value = bgmVol;
        //seSlider.value = seVol;

        // 発火させずに値を設定
        //bgmSlider.SetValueWithoutNotify(bgmVol);
        //seSlider.SetValueWithoutNotify(seVol);
        */
    }
}
