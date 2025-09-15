using Alvin.TowerDefense.Games;

namespace Audio
{
    public interface IAudioService : IService
    {
        void PlaySound(string soundName);
    }
}