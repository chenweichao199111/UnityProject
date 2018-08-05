using DotNetSpeech;
using UnityEngine;

public class TextToSpeech : MonoBehaviour
{
    void Start()
    {
        SpVoice voice = new SpVoice();
        voice.Speak("真心换真情");
    }
}
