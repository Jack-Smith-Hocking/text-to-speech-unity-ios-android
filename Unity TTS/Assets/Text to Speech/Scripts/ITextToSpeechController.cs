namespace TextToSpeech
{
	public interface ITextToSpeechController
    {
        bool IsSpeaking { get; }
        
        string Locale { get; }
        float Pitch { get; }
        float Rate { get; }

        event System.Action<string> OnStartSpeak;
        event System.Action OnStopSpeak;

        void Setup(string locale, float pitch, float rate);

        void Speak(string text, System.Action onComplete = null);
        void Stop();
    }
}