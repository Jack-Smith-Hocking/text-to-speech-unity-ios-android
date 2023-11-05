# Text to Speech in Unity iOS and Unity Android
I have provided all Java and object C source. You can learn how it works, optimize, or add any features.

## Native Text to Speech
* TextToSpeech Android: https://developer.android.com/reference/android/speech/tts/TextToSpeech.html
* TextToSpeech iOS: https://developer.apple.com/reference/avfoundation

## Android
* ClassNotFoundException:
  * Caused by minification of the java code, either turn "Player/Publisher Settings/Minify" off or;
  * Add a [keep declaration](https://developer.android.com/build/shrink-code#keep-code) to your proguard file: `-keep class com.starseed.texttospeech.** { *; }`

## Xcode
* Requires Xcode8 or higher. Target iOS 10.0
* Add framework

      - speech.farmwork
      - AVFoundation.framework
