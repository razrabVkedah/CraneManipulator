using System;
using UnityEngine;
using UnityEngine.UI;

namespace UIManagers
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button levelButton;
        [SerializeField] private Image levelRenderer;
        [SerializeField] private GameObject[] stars;
        [SerializeField] private Color openedLevelColor, closedLevelColor;


        public void SetLevelOpened(int starsCount)
        {
            levelButton.enabled = true;
            levelRenderer.color = openedLevelColor;
            for(var i = 0; i < stars.Length; i++)
                stars[i].SetActive(starsCount > i);
        }
        
        public void SetLevelClosed()
        {
            levelButton.enabled = false;
            foreach (var star in stars)
            {
                star.SetActive(false);
            }
            levelRenderer.color = closedLevelColor;
        }
    }
}
