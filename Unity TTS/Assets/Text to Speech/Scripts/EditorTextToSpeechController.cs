using UnityEngine;
using System;

namespace TextToSpeech
{
	public class EditorTextToSpeechController : ITextToSpeechController
	{
		public static EditorTextToSpeechController Instance { get; } = new();

		public bool IsSpeaking { get; private set; }

		public string Locale { get; private set; }
		public float Pitch { get; private set; }
		public float Rate { get; private set; }

		public event Action<string> OnStartSpeak;
		public event Action OnStopSpeak;

		private System.Action _callback;

		private EditorTextToSpeechController()
		{
		}

		public void Setup(string locale, float pitch, float rate)
		{
			Locale = locale;
			Pitch = pitch;
			Rate = rate;
		}

		public void Speak(string text, Action onComplete = null)
		{
			IsSpeaking = true;

			_callback = onComplete;
			OnStartSpeak?.Invoke(text);

			Stop();
		}

		public void Stop()
		{
			IsSpeaking = false;

			_callback?.Invoke();
			_callback = null;
			OnStopSpeak?.Invoke();
		}
	}
}