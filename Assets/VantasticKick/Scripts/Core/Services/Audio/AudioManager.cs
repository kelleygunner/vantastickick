using UnityEngine;

namespace VantasticKick.Core.Audio
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        [SerializeField] private AudioSource _kick;
        [SerializeField] private AudioSource _target;
        [SerializeField] private AudioSource _whistle;
        
        public void PlayClip(AudioClipType clipType)
        {
            switch (clipType)
            {
                case AudioClipType.Kick:
                    _kick.Play();
                    break;
                case AudioClipType.Target:
                    _target.Play();
                    break;
                case AudioClipType.Whistle:
                    _whistle.Play();
                    break;
            }
        }
    }
}
