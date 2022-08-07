using UnityEngine;

namespace Sounds
{
    public class SceneSoundsManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] defaultSounds;
        [SerializeField] private AudioClip[] metalSounds;
        [SerializeField] private AudioClip[] woodSounds;
        [SerializeField] private AudioClip[] dirtSounds;
        [SerializeField] private AudioClip[] glassSounds;
        [SerializeField] private AudioClip[] stoneSounds;


        public AudioClip[] GetSounds(CollisionMaterial material)
        {
            return material switch
            {
                CollisionMaterial.Default => defaultSounds,
                CollisionMaterial.Metal => metalSounds,
                CollisionMaterial.Wood => woodSounds,
                CollisionMaterial.Dirt => dirtSounds,
                CollisionMaterial.Glass => glassSounds,
                CollisionMaterial.Stone => stoneSounds,
                _ => defaultSounds
            };
        }
    }

    public enum CollisionMaterial : byte
    {
        Default,
        Metal,
        Wood, 
        Dirt,
        Glass,
        Stone,
    }
}
