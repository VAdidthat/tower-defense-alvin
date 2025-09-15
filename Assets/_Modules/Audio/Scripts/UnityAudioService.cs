using UnityEngine;

namespace Audio
{
    public class UnityAudioService : MonoBehaviour, IAudioService
    {
        public void Initialize()
        {
            Debug.Log("UnityAudioService Initialized");
        }
        
        public void PlaySound(string soundName)
        {
        }
    }
}