using UnityEngine;
using UnityEngine.UI;

public class SettingSoundUI : MonoBehaviour
{
    [Tooltip("BGMボリュームバー")]
    [SerializeField] private Slider bgmSlider;
    [Tooltip("SEボリュームバー")]
    [SerializeField] private Slider seSlider;

    // Start is called before the first frame update
    void Start()
    {
        float bgmVol = PlayerPrefs.GetFloat("BGM_VOLUME", 0f);
        float seVol  = PlayerPrefs.GetFloat("SE_VOLUME" , 0f);

        bgmSlider.value = bgmVol;
        seSlider.value = seVol;

        bgmSlider.onValueChanged.AddListener(SoundManager.instance.SetBGMVol);
        seSlider.onValueChanged.AddListener(SoundManager.instance.SetSEVol);
    }
}
