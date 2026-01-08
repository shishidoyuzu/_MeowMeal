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
    GameObject BGMobj;

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


    GameObject SEobj;

    [Header("AudioMixer")]
    [SerializeField] AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

        // 子要素の取得
        BGMobj = transform.GetChild(0).gameObject;
        SEobj  = transform.GetChild(1).gameObject;

        // AudioSourceの取得
        BGM_audioSource = BGMobj.GetComponent<AudioSource>();
        SE_audioSource  = SEobj.GetComponent<AudioSource>();

        // BGMの再生
        BGM_audioSource.clip = BGM;
        BGM_audioSource.Play();

        // スライダーの値をBGM・SEの音量に設定
        SetBGMVolume(BGMvol_Bar.value);
        SetSEVolume(SEvol_Bar.value);
        // スライダーの値が変更された時、メソッドを実行する
        BGMvol_Bar.onValueChanged.AddListener(SetBGMVolume);
        SEvol_Bar.onValueChanged.AddListener(SetSEVolume);
    }

    // BGM
    public void SetBGMVolume(float volume)
    {
        // スライダーの値を変更した時に、呼び出されるメソッド
        // ・BGMの音量を変える
        audioMixer.SetFloat("BGM", Mathf.Clamp(Mathf.Log10(volume) * 20f, -80f, 0f));
    }

    // SE
    public void SetSEVolume(float volume)
    {
        // スライダーの値を変更した時に、呼び出されるメソッド
        // ・SEの音量を変える
        audioMixer.SetFloat("SE", Mathf.Clamp(Mathf.Log10(volume) * 20f, -80f, 0f));
    }
}
