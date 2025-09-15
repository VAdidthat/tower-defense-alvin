using UnityEngine;

namespace Audio
{
    public class UnrealAudioService : MonoBehaviour, IAudioService
    {
        public void Initialize()
        {
            Debug.Log("UnrealAudioService Initialized");
        }

        public void PlaySound(string soundName)
        {
        }
    }
}