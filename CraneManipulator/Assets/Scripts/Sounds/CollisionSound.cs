using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sounds
{
    public class CollisionSound : MonoBehaviour
    {
        [SerializeField] private AudioSource soundPlayer;
        [SerializeField] private CollisionMaterial material;
        [SerializeField] private float maxVelocity = 10f;
        [SerializeField] private float delaySoundPlay = 0.5f;
        private SceneSoundsManager _manager;
        private AudioClip[] _collisionSounds;
        private bool _canPlaySound;

        private void Reset()
        {
            soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer == null) soundPlayer = gameObject.AddComponent<AudioSource>();
            soundPlayer.playOnAwake = false;
            soundPlayer.spatialBlend = 0.8f;
            soundPlayer.minDistance = 5f;
            soundPlayer.spread = 50f;
        }

        private void Start()
        {
            _manager = FindObjectOfType<SceneSoundsManager>();
            if (_manager == null)
            {
                Debug.LogError(gameObject.name + " can NOT find SceneSoundManager component on scene");
                _canPlaySound = false;
                return;
            }

            _collisionSounds = _manager.GetSounds(material);
            _canPlaySound = true;
        }

        private IEnumerator DelaySoundPlayRoutine()
        {
            _canPlaySound = false;
            yield return new WaitForSeconds(delaySoundPlay);
            _canPlaySound = true;
        }

        private void PlaySound()
        {
            if(_canPlaySound == false) return;
            StartCoroutine(DelaySoundPlayRoutine());
            soundPlayer.clip = _collisionSounds[Random.Range(0, _collisionSounds.Length)];
            soundPlayer.Play();
        }

        private void OnCollisionEnter(Collision collision)
        {
            soundPlayer.volume = Mathf.InverseLerp(0f, maxVelocity, collision.relativeVelocity.magnitude);
            PlaySound();
        }
    }
}
