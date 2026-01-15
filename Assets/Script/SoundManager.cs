using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("BGM")]
    [Tooltip("BGMボリュームバー")]
    [SerializeField] private Slider BGMvol_Bar;
    [Tooltip("BGMオーディオソース")]
    public AudioSource BGM_audioSource;
    // BGM
    public AudioClip BGM;

    [Header("SE")]
    [Tooltip("SEボリュームバー")]
    [SerializeField] private Slider SEvol_Bar;
    [Tooltip("SEオーディオソース")]
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

        // 子要素の取得
        BGMobj = transform.GetChild(0).gameObject;
        SEobj  = transform.GetChild(1).gameObject;

        // AudioSourceの取得
        BGM_audioSource = BGMobj.GetComponent<AudioSource>();
        SE_audioSource  = SEobj.GetComponent<AudioSource>();

        // 保存した音量の反映
        float bgmVol = PlayerPrefs.GetFloat(BGM_VOL_KEY, 0f);
        float seVol = PlayerPrefs.GetFloat(SE_VOL_KEY, 0f);
        audioMixer.SetFloat("BGM", bgmVol);
        audioMixer.SetFloat("SE", seVol);

        // BGMの再生
        BGM_audioSource.clip = BGM;
        BGM_audioSource.Play();
    }

    public void SetBGMVol(float volume)
    {
        // BGMスライダーにある「OnValueChanged」に設定
        audioMixer.SetFloat("BGM",volume);
        PlayerPrefs.SetFloat(BGM_VOL_KEY, volume);
    }

    public void SetSEVol(float volume)
    {
        // SEスライダーにある「OnValueChanged」に設定
        audioMixer.SetFloat("SE", volume);
        PlayerPrefs.SetFloat(SE_VOL_KEY,volume);
    }

}
