namespace VantasticKick.Core.Audio
{
    public interface IAudioManager
    {
        void PlayClip(AudioClipType clipType);
    }

    public enum AudioClipType
    {
        Kick,
        Target,
        Whistle
    }
}
