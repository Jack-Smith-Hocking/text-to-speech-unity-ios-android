using UnityEngine;
using System.Collections;
using System;

namespace TextToSpeech
{
	public class AndroidTextToSpeechController : MonoBehaviour, ITextToSpeechController
    {
        public static AndroidTextToSpeechController Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Create if it doesn't exist
                    _instance = new GameObject("TextToSpeech")
						.AddComponent<AndroidTextToSpeechController>();
                }

                return _instance;
            }
        }
		private static AndroidTextToSpeechController _instance;

		public bool IsSpeaking { get; private set; }

		public string Locale { get; private set; }

        public float Pitch { get; private set; } = 1;
        public float Rate { get; private set; } = 1;

		public event Action<string> OnSpeak;
		public event Action OnStop;

		private System.Action _callback;

		public void Setup(string locale, float pitch, float rate)
        {
            Pitch = Mathf.Clamp(pitch, 0.5f, 2);
			Rate = Mathf.Clamp(rate, 0.5f, 2);

#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.starseed.texttospeech.Bridge");
        javaUnityClass.CallStatic("SettingTextToSpeech", locale, pitch, rate);
#endif
		}

		public void OnSettingResult(string args)
		{
			// Denotes the language is available for the language by the locale, but not the country and variant.
			const int LANG_AVAILABLE = 0;
			// Denotes the language data is missing. 
			const int LANG_MISSING_DATA = -1;
			// Denotes the language is not supported.
			const int LANG_NOT_SUPPORTED = -2;

			int code = int.Parse(args);

			if (code == LANG_MISSING_DATA || code == LANG_NOT_SUPPORTED)
			{
				Debug.LogWarning("This Language is not supported");
			}
			else
			{
				Debug.Log("This Language valid");
			}
		}

		public void Speak(string text, Action onComplete = null)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.starseed.texttospeech.Bridge");
        javaUnityClass.CallStatic("OpenTextToSpeech", text);
#endif
			IsSpeaking = true;

			_callback = OnComplete;
			OnSpeak?.Invoke(text);
		}

		public void Stop()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.starseed.texttospeech.Bridge");
        javaUnityClass.CallStatic("StopTextToSpeech");
#endif
			OnComplete();
		}

		public void OnStart(string text)
        {
        }
        public void OnDone(string text)
        {
			OnComplete();
        }

        public void OnError(string text)
        {
			OnComplete();
		}
		public void OnMessage(string text)
        {
        }

		private void OnComplete()
		{
			IsSpeaking = false;

			_callback?.Invoke();
			_callback = null;

			OnStop?.Invoke();
		}
	}
}