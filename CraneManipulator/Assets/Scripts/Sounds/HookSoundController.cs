using UnityEngine;

namespace Sounds
{
    public class HookSoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource takeAndDropPlayer;
        [SerializeField] private AudioClip takeSound, dropSound;
        
        public void TakeSound()
        {
            takeAndDropPlayer.clip = takeSound;
            takeAndDropPlayer.Play();
        }

        public void DropSound()
        {
            takeAndDropPlayer.clip = dropSound;
            takeAndDropPlayer.Play();
        }
    }
}
