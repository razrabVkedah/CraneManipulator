using System;
using UIManagers;
using UnityEngine;

namespace Level
{
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] levelPrefabs;
        
        [SerializeField] private bool changeCranePosition;
        [SerializeField] private Transform crane;
        [SerializeField] private Vector3[] levelsCranePositions;

        private void Awake()
        {
            if(changeCranePosition == false) return;

            crane.position = levelsCranePositions[MenuUI.ActiveLevel - 1];
        }

        private void Start()
        {
            var selectedLevel = MenuUI.ActiveLevel;
            Instantiate(levelPrefabs[selectedLevel - 1], new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
    }
}
