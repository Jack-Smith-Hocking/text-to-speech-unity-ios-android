using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using TextToSpeech;

public class SampleTextToSpeech : MonoBehaviour
{
    public GameObject loading;
    public InputField inputLocale;
    public InputField inputText;
    public float pitch;
    public float rate;

    public Text txtLocale;
    public Text txtPitch;
    public Text txtRate;

    void Start()
    {
        Setting("en-US");
        loading.SetActive(false);
    }

    public void OnClickSpeak()
    {
        TextToSpeechController.Instance.Speak(inputText.text, null);
    }

    /// <summary>
    /// </summary>
    public void  OnClickStopSpeak()
    {
		TextToSpeechController.Instance.Stop();
    }

    /// <summary>
    /// </summary>
    /// <param name="code"></param>
    public void Setting(string code)
    {
        txtLocale.text = "Locale: " + code;
        txtPitch.text = "Pitch: " + pitch;
        txtRate.text = "Rate: " + rate;
		TextToSpeechController.Instance.Setup(code, pitch, rate);
    }

    /// <summary>
    /// Button Click
    /// </summary>
    public void OnClickApply()
    {
        Setting(inputLocale.text);
    }
}
