using DotNetSpeech;
using UnityEngine;

public class TextToSpeech : MonoBehaviour
{
    void Start()
    {
        //SpeechVoiceSpeakFlags flags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
        SpVoice sp = new SpVoice();
        sp.Voice = sp.GetVoices("name=Microsoft Lili", "").Item(0);
        sp.Rate = -3;
        sp.Speak("是你的真心换你的真情");
    }
}
