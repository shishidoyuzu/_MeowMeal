using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingSoundUI : MonoBehaviour
{
    [Tooltip("BGMボリュームバー")]
    [SerializeField] private Slider bgmSlider;
    [Tooltip("SEボリュームバー")]
    [SerializeField] private Slider seSlider;


    // シーンの切り替えを検知したとき、
    // そのシーンに存在する「SoundManager」を取得し、
    // 各スライダーのonValueChangedにSet○○Volを設定する。
    // ようにしたい

    /*
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // シーンに存在する SoundManager を取得
        SoundManager sm = FindObjectOfType<SoundManager>();
        if(sm == null) return; // SoundManager がない場合、処理しない

        // 登録していたものを削除する
        bgmSlider.onValueChanged.RemoveAllListeners();
        seSlider.onValueChanged.RemoveAllListeners();

        // 保存している値を再度、反映させる
        float bgmVol = PlayerPrefs.GetFloat("BGM_VOLUME", 0f);
        float seVol = PlayerPrefs.GetFloat("SE_VOLUME", 0f);
        bgmSlider.value = bgmVol;
        seSlider.value = seVol;

        // もう一度登録し直す
        bgmSlider.onValueChanged.AddListener(sm.SetBGMVol);
        seSlider.onValueChanged.AddListener(sm.SetSEVol);
    }
    */

    void Start()
    {
        // シーンに存在する SoundManager を取得
        SoundManager sm = FindObjectOfType<SoundManager>();
        if (sm == null) return; // SoundManager がない場合、処理しない

        // 登録していたものを削除する
        bgmSlider.onValueChanged.RemoveAllListeners();
        seSlider.onValueChanged.RemoveAllListeners();

        // 保存している値を再度、反映させる
        float bgmVol = PlayerPrefs.GetFloat("BGM_VOLUME", 0f);
        float seVol = PlayerPrefs.GetFloat("SE_VOLUME", 0f);
        bgmSlider.value = bgmVol;
        seSlider.value = seVol;

        // もう一度登録し直す
        bgmSlider.onValueChanged.AddListener(sm.SetBGMVol);
        seSlider.onValueChanged.AddListener(sm.SetSEVol);
    }
}
