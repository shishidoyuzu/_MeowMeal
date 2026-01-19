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
    [Tooltip("ねこの鳴き声")]
    public AudioClip SE_meowVer1;
    [Tooltip("ねこの鳴き声")]
    public AudioClip SE_meowVer2;
    [Tooltip("ねこの鳴き声")]
    public AudioClip SE_meowVer3;
    [Tooltip("ねこの鳴き声")]
    public AudioClip SE_meowVer4;

    [Tooltip("スコア表示")]
    public AudioClip SE_score;

    GameObject BGMobj;
    GameObject SEobj;

    // ボリューム保存用のキー
    public const string BGM_VOL_KEY = "BGM_VOLUME";
    public const string SE_VOL_KEY  = "SE_VOLUME";

    // 初回起動判定キー
    public const string FIRST_RUN_KEY = "FIRST_RUN";
    public const string FIRST_RUN_EDITOR_KEY = "FIRST_RUN_EDITOR";

    [Header("AudioMixer")]
    [SerializeField] AudioMixer audioMixer;

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
        }


        bool isFirstRun = false;

        // エディター起動時
#if UNITY_EDITOR
        if (!PlayerPrefs.HasKey(FIRST_RUN_EDITOR_KEY))
        {
            isFirstRun = true;
            PlayerPrefs.SetInt(FIRST_RUN_EDITOR_KEY, 1);
        }
#else
    if (!PlayerPrefs.HasKey(FIRST_RUN_KEY))
    {
        isFirstRun = true;
        PlayerPrefs.SetInt(FIRST_RUN_KEY, 1);
    }
#endif

        // 初回起動なら
        if (isFirstRun)
        {
            // 音量を最大に
            PlayerPrefs.SetFloat(BGM_VOL_KEY, 1f);
            PlayerPrefs.SetFloat(SE_VOL_KEY, 1f);
            // 値を保存
            PlayerPrefs.Save();
        }


        // 子要素の取得
        BGMobj = transform.GetChild(0).gameObject;
        SEobj  = transform.GetChild(1).gameObject;

        // AudioSourceの取得
        BGM_audioSource = BGMobj.GetComponent<AudioSource>();
        SE_audioSource  = SEobj.GetComponent<AudioSource>();

        // 保存した音量の反映
        float bgmVol = PlayerPrefs.GetFloat(BGM_VOL_KEY, 0f);
        float seVol = PlayerPrefs.GetFloat(SE_VOL_KEY, 0f);
        SetBGMVol(bgmVol);
        SetSEVol(seVol);

        // BGMの再生
        BGM_audioSource.clip = BGM;
        BGM_audioSource.Play();
    }

    public void SetBGMVol(float volume)
    {
        // 音量が 0 or -0～ の時
        if(volume <= 0f)
        {
            // 完全な無音にする
            audioMixer.SetFloat("BGM", -80f);
            PlayerPrefs.SetFloat(BGM_VOL_KEY, 0f);
            return;
        }

        float VolumedB = Mathf.Log10(volume) * 20f;

        // BGMスライダーにある「OnValueChanged」に設定
        audioMixer.SetFloat("BGM", VolumedB);
        PlayerPrefs.SetFloat(BGM_VOL_KEY, volume);
    }

    public void SetSEVol(float volume)
    {
        // 音量が 0 or -0～ の時
        if (volume <= 0f)
        {
            // 完全な無音にする
            audioMixer.SetFloat("SE", -80f);
            PlayerPrefs.SetFloat(SE_VOL_KEY, 0f);
            return;
        }

        float VolumedB = Mathf.Log10(volume) * 20f;

        // SEスライダーにある「OnValueChanged」に設定
        audioMixer.SetFloat("SE", VolumedB);
        PlayerPrefs.SetFloat(SE_VOL_KEY,volume);
    }

}
