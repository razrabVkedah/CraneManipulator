using System.Collections;
using UnityEngine;
namespace Sounds
{
    public class SoundPlayController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioPlayer;
        [SerializeField] private bool loop = true, slowlyStopping = true;
        [SerializeField] private float slowlyStoppingSpeed = 3f;
        private bool _isPlaying;
        private AudioClip _lastSound;
        
        public void Play(AudioClip playSound)
        {
            if(_isPlaying == true && audioPlayer.clip == playSound) return;
            StopAllCoroutines();
            _isPlaying = true;

            audioPlayer.clip = playSound;
            _lastSound = playSound;
            audioPlayer.Play();
            
            if(loop)
                StartCoroutine(DelayCall(audioPlayer.clip.length));
        }

        public void SetVolume(float value)
        {
            if(_isPlaying == false) return;
            
            audioPlayer.volume = value;
        }

        private IEnumerator DelayCall(float delay)
        {
            yield return new WaitForSeconds(delay);
            _isPlaying = false;
            audioPlayer.Stop();
            audioPlayer.clip = null;
            Play(_lastSound);
        }

        public void Stop()
        {
            if(_isPlaying == false) return;
            
            StopAllCoroutines();
            
            _isPlaying = false;
            
            if (slowlyStopping == true)
            {
                StartCoroutine(SlowStopRoutine());
                return;
            }
            audioPlayer.Stop();
        }

        private IEnumerator SlowStopRoutine()
        {
            var currentVolume = audioPlayer.volume;
            
            for (var i = currentVolume; i >= 0f; i -= Time.deltaTime * slowlyStoppingSpeed)
            {
                audioPlayer.volume = i;
                yield return new WaitForEndOfFrame();
            }
            
            audioPlayer.Stop();
        }
    }
}
