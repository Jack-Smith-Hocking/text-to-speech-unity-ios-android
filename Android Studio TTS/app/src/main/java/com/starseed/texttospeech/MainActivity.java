package com.starseed.texttospeech;

import android.os.Bundle;
import android.speech.tts.TextToSpeech;
import android.speech.tts.UtteranceProgressListener;
import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;
import java.util.ArrayList;
import java.util.Locale;


public class MainActivity extends UnityPlayerActivity
{
    private TextToSpeech tts;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        tts = new TextToSpeech(this, initListener);
    }
    @Override
    public void onDestroy() {
        // Don't forget to shutdown tts!
        if (tts != null) {
            tts.stop();
            tts.shutdown();
        }
        super.onDestroy();
    }

    public void OnSettingSpeak(String language, float pitch, float rate) {
        tts.setPitch(pitch);
        tts.setSpeechRate(rate);
        int result = tts.setLanguage(getLocaleFromString(language));
        UnityPlayer.UnitySendMessage("TextToSpeech", "OnSettingResult", "" + result);
    }

    public  void OnStartSpeak(String valueText)
    {
        tts.speak(valueText, TextToSpeech.QUEUE_FLUSH, null, valueText);
    }
    public void OnStopSpeak()
    {
        tts.stop();
    }

    TextToSpeech.OnInitListener initListener = new TextToSpeech.OnInitListener()
    {
        @Override
        public void onInit(int status) {
            if (status == TextToSpeech.SUCCESS)
            {
                OnSettingSpeak(Locale.US.toString(), 1.0f, 1.0f);
                tts.setOnUtteranceProgressListener(utteranceProgressListener);
            }
        }
    };

    UtteranceProgressListener utteranceProgressListener=new UtteranceProgressListener() {
        @Override
        public void onStart(String utteranceId) {
            UnityPlayer.UnitySendMessage("TextToSpeech", "OnStart", utteranceId);
        }
        @Override
        public void onError(String utteranceId) {
            UnityPlayer.UnitySendMessage("TextToSpeech", "OnError", utteranceId);
        }
        @Override
        public void onDone(String utteranceId) {
            UnityPlayer.UnitySendMessage("TextToSpeech", "OnDone", utteranceId);
        }
    };

    /**
     * Convert a string based locale into a Locale Object.
     * Assumes the string has form "{language}_{country}_{variant}".
     * Examples: "en", "de_DE", "_GB", "en_US_WIN", "de__POSIX", "fr_MAC"
     *
     * @param localeString The String
     * @return the Locale
     */
    public static Locale getLocaleFromString(String localeString)
    {
        if (localeString == null)
        {
            return null;
        }
        localeString = localeString.trim();
        if (localeString.equalsIgnoreCase("default"))
        {
            return Locale.getDefault();
        }

        // Extract language
        int languageIndex = localeString.indexOf('_');
        String language;
        if (languageIndex == -1)
        {
            // No further "_" so is "{language}" only
            return new Locale(localeString, "");
        }
        else
        {
            language = localeString.substring(0, languageIndex);
        }

        // Extract country
        int countryIndex = localeString.indexOf('_', languageIndex + 1);
        String country;
        if (countryIndex == -1)
        {
            // No further "_" so is "{language}_{country}"
            country = localeString.substring(languageIndex+1);
            return new Locale(language, country);
        }
        else
        {
            // Assume all remaining is the variant so is "{language}_{country}_{variant}"
            country = localeString.substring(languageIndex+1, countryIndex);
            String variant = localeString.substring(countryIndex+1);
            return new Locale(language, country, variant);
        }
    }
}
