namespace TextToSpeech
{
	public interface ITextToSpeechController
    {
        bool IsSpeaking { get; }
        
        string Locale { get; }
        float Pitch { get; }
        float Rate { get; }

        event System.Action<string> OnSpeak;
        event System.Action OnStop;

        void Setup(string locale, float pitch, float rate);

        void Speak(string text, System.Action onComplete = null);
        void Stop();
    }
}