using UnityEngine;
using System;

namespace TextToSpeech
{
	public class TextToSpeechController : ITextToSpeechController
	{
        public static TextToSpeechController Instance { get; } = new();

        public bool IsSpeaking => _controller.IsSpeaking;

		public string Locale => _controller.Locale;
		public float Pitch => _controller.Pitch;
		public float Rate => _controller.Rate;

		public event System.Action<string> OnStartSpeak;
		public event System.Action OnStopSpeak;

		private ITextToSpeechController _controller;

        private TextToSpeechController()
        {
#if UNITY_EDITOR
			_controller = EditorTextToSpeechController.Instance;
#elif UNITY_ANDROID
			_controller = AndroidTextToSpeechController.Instance;
#endif

			_controller.OnStartSpeak += OnStartSpeak;
            _controller.OnStopSpeak += OnStopSpeak;
        }
        ~TextToSpeechController()
        {
            if (_controller != null)
            {
                _controller.OnStartSpeak -= OnStartSpeak;
                _controller.OnStopSpeak -= OnStopSpeak;
            }
		}

		public void Setup(string locale, float pitch, float rate)
		{
            _controller.Setup(locale, pitch, rate);
		}

		public void Speak(string text, Action onComplete = null)
		{
			Debug.Log("[TTS] Speaking: " + text);
			_controller.Speak(text, onComplete);
		}

		public void Stop()
		{
			Debug.Log("[TTS] Stopped Speaking");
			_controller.Stop();
		}
	}
}