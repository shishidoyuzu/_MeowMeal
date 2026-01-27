using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("BGM")]
    public AudioSource BGM_audioSource;
    // BGM
    public AudioClip BGM;

    [Header("SE")]
    public AudioSource SE_audioSource;
    // SE
    [Tooltip("UI（ボタン）クリック")]
    public AudioClip SE_UIclick;
    [Tooltip("UI（閉じる）クリック")]
    public AudioClip SE_UIclose;
    [Tooltip("お皿にごはんが当たる音")]
    public AudioClip SE_fallPlate;
    [Tooltip("ねこリアクション：Lovey")]
    public AudioClip SE_meowLovey;
    [Tooltip("ねこリアクション：Happy")]
    public AudioClip SE_meowHappy;
    [Tooltip("ねこリアクション：UnHappy")]
    public AudioClip SE_meowUnhappy;
    [Tooltip("ねこリアクション：Angry")]
    public AudioClip SE_meowAngry;

    // ゲームシーンに現れた時の鳴き声
    [Header("普通のねこの鳴き声（全４種）")]
    [SerializeField] private List<AudioClip> normalCat_voices = new List<AudioClip>();
    [Header("でぶねこの鳴き声（全４種）")]
    [SerializeField] private List<AudioClip> fatCat_voices = new List<AudioClip>();

    /*
    ねこがゲームシーンに登場するごとに特定の鳴き声SEをランダムで再生させたい。

    でぶねこ以外のねこが登場したときは、nomalCat_voiceに登録されてるSE全４種の中からランダムで再生
    でぶねこが登場したときは、fatCat_voiceに登録されているSE全４種の中からランダムで再生
     */

    [Tooltip("スコア表示")]
    public AudioClip SE_score;

    GameObject BGMobj;
    GameObject SEobj;

    // ボリューム保存用のキー
    //public const string BGM_VOL_KEY = "BGM_VOLUME";
    //public const string SE_VOL_KEY  = "SE_VOLUME";

    // 初回起動判定キー
    //public const string FIRST_RUN_KEY = "FIRST_RUN";
    //public const string FIRST_RUN_EDITOR_KEY = "FIRST_RUN_EDITOR";

    [Header("AudioMixer")]
    [SerializeField] AudioMixer audioMixer;

    /*  BGM/SEの音量を保存する

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        bool isFirstRun;

#if UNITY_EDITOR
        isFirstRun = !PlayerPrefs.HasKey("FIRST_RUN_EDITOR");
        if (isFirstRun)
            PlayerPrefs.SetInt("FIRST_RUN_EDITOR", 1);
#else
    isFirstRun = !PlayerPrefs.HasKey(FIRST_RUN_KEY);
    if (isFirstRun)
        PlayerPrefs.SetInt(FIRST_RUN_KEY, 1);
#endif

        if (isFirstRun)
        {
            PlayerPrefs.SetFloat("BGM_VOLUME", 1f);
            PlayerPrefs.SetFloat("SE_VOLUME", 1f);
            PlayerPrefs.Save();
        }

        float bgmVol = PlayerPrefs.GetFloat("BGM_VOLUME", 1f);
        float seVol = PlayerPrefs.GetFloat("SE_VOLUME", 1f);

        AudioMixer mixer = Resources.Load<AudioMixer>("MainAudioMixer");
        if (mixer == null) return;

        mixer.SetFloat("BGM", bgmVol <= 0f ? -80f : Mathf.Log10(bgmVol) * 20f);
        mixer.SetFloat("SE", seVol <= 0f ? -80f : Mathf.Log10(seVol) * 20f);
    }
    */

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        /* 不要な要素
        // 確認用
        //Debug.Log("EDITOR FIRST RUN = " + PlayerPrefs.HasKey(FIRST_RUN_EDITOR_KEY));
        //Debug.Log("BGM_VOLUME = " + PlayerPrefs.GetFloat(BGM_VOL_KEY, -1f));
        //Debug.Log("SE_VOLUME = "  + PlayerPrefs.GetFloat(SE_VOL_KEY , -1f));
        */

        // 子要素の取得
        BGMobj = transform.GetChild(0).gameObject;
        SEobj = transform.GetChild(1).gameObject;

        // AudioSourceの取得
        BGM_audioSource = BGMobj.GetComponent<AudioSource>();
        SE_audioSource = SEobj.GetComponent<AudioSource>();

        /* 不要な要素
        // 保存した音量の反映
        float bgmVol = PlayerPrefs.GetFloat(BGM_VOL_KEY, 0f);
        float seVol = PlayerPrefs.GetFloat(SE_VOL_KEY, 0f);
        SetBGMVol(bgmVol);
        SetSEVol(seVol);
        */

        // 音量を最大に強制
        SetBGMVol(1f);
        SetSEVol(1f);

        // BGMの再生
        BGM_audioSource.clip = BGM;
        BGM_audioSource.Play();
    }

    public void SetBGMVol(float volume)
    {
        // 音量が 0 or -0～ の時
        if (volume <= 0f)
        {
            // 完全な無音にする
            audioMixer.SetFloat("BGM", -80f);
            //PlayerPrefs.SetFloat(BGM_VOL_KEY, 0f);
            return;
        }

        float VolumedB = Mathf.Log10(volume) * 20f;

        // BGMスライダーにある「OnValueChanged」に設定
        audioMixer.SetFloat("BGM", VolumedB);
        //PlayerPrefs.SetFloat(BGM_VOL_KEY, volume);
    }

    public void SetSEVol(float volume)
    {
        // 音量が 0 or -0～ の時
        if (volume <= 0f)
        {
            // 完全な無音にする
            audioMixer.SetFloat("SE", -80f);
            //PlayerPrefs.SetFloat(SE_VOL_KEY, 0f);
            return;
        }

        float VolumedB = Mathf.Log10(volume) * 20f;

        // SEスライダーにある「OnValueChanged」に設定
        audioMixer.SetFloat("SE", VolumedB);
        //PlayerPrefs.SetFloat(SE_VOL_KEY,volume);
    }

    // 普通のねこ鳴き声リストに登録されているAudioClipをランダムに再生する関数
    public void RandomPlay_VCN()
    {
        // 未登録ならスルー
        if (normalCat_voices.Count == 0) return;

        AudioClip clip = normalCat_voices[Random.Range(0, normalCat_voices.Count)];
        SE_audioSource.PlayOneShot(clip);
    }

    // でぶねこ鳴き声リストに登録されているAudioClipをランダムに再生する関数
    public void RandomPlay_VCF()
    {
        // 未登録ならスルー
        if(fatCat_voices.Count == 0) return;

        AudioClip clip = fatCat_voices[Random.Range(0, fatCat_voices.Count)];
        SE_audioSource.PlayOneShot(clip);
    }
}