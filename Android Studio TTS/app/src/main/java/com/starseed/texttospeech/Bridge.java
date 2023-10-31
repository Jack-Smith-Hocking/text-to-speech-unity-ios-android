package com.starseed.texttospeech;

import android.content.Intent;
import com.unity3d.player.UnityPlayer;

/**
 * Created by J1mmyTo9
 */
public class Bridge {

    protected static String languageSpeech = "en-US";

    // Text To Speech
    public static void SettingTextToSpeech(String language, float pitch, float rate) {
        MainActivity activity = (MainActivity)UnityPlayer.currentActivity;
        activity.OnSettingSpeak(language, pitch, rate);
    }

    public static void OpenTextToSpeech(String text) {
        MainActivity activity = (MainActivity)UnityPlayer.currentActivity;
        activity.OnStartSpeak(text);
    }
    public static void StopTextToSpeech(){
        MainActivity activity = (MainActivity)UnityPlayer.currentActivity;
        activity.OnStopSpeak();
    }
}
