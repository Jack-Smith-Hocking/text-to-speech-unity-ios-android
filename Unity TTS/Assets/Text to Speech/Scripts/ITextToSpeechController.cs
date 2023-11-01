using System;

namespace TextToSpeech
{
	public enum TextToSpeechComplete
	{
		Normal,
		Stopped,
		Error
	}

	public interface ITextToSpeechController
	{
		bool IsSpeaking { get; }

		string Locale { get; }
		float Pitch { get; }
		float Rate { get; }

		event System.Action<string> OnStartSpeak;
		event System.Action OnStopSpeak;
		public event System.Action OnCompleteSpeak;

		void Setup(string locale, float pitch, float rate);

		void Speak(string text, Action<TextToSpeechComplete> onComplete = null);
		void Stop();
	}
}