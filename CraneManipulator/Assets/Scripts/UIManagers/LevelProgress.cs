using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CraneControl;
using InteractableObjects;
using Level;
using UnityEngine;
using YG;

namespace UIManagers
{
    public class LevelProgress : MonoBehaviour
    {
        [SerializeField] private List<TaskPlace> allPlaces = new();

        [SerializeField] private LevelUI ui;

        [SerializeField] private int levelDuration;
        [SerializeField] private LevelStars[] stars = new LevelStars[3];

        private bool _levelResult;
        
        

        [ContextMenu("Set level settings")]
        private void SetSettings()
        {
            allPlaces.Clear();
            allPlaces = GetComponentsInChildren<TaskPlace>().ToList();

            for (var i = 0; i < stars.Length; i++)
            {
                stars[i].starsCount = i + 1;
                stars[i].maxTime =(int)(levelDuration * ((float)(i + 1) / stars.Length));
            }
        }

        private void Start()
        {
            ui = FindObjectOfType<LevelUI>();
            StartCoroutine(WaitRoutine());
            TaskPlace.OnTaskPlaceFilling.AddListener(CheckOnWin);
            CraneHookPhysics.OnJointBroken.AddListener(Lose);
            TaskObject.OnObjectDestroyEvent.AddListener(Lose);
            CheckOnWin();
            YandexGame.FullscreenShow();
        }

        private void OnDestroy()
        {
            TaskPlace.OnTaskPlaceFilling.RemoveListener(CheckOnWin);
            CraneHookPhysics.OnJointBroken.RemoveListener(Lose);
            TaskObject.OnObjectDestroyEvent.RemoveListener(Lose);
        }

        private IEnumerator WaitRoutine()
        {
            while (levelDuration >= 0)
            {
                levelDuration--;
                ui.DrawTime(levelDuration);
                ui.SetStarsPlaymodeCount(GetStarsCount());
                yield return new WaitForSeconds(1f);
            }
            
            Lose(LoseReason.TimeIsOver);
        }

        private void Lose(LoseReason reason)
        {
            if(_levelResult == true) return;
            _levelResult = true;
            ui.ActivateLoseUI(reason);
        }

        private void CheckOnWin()
        {
            var correctCount = allPlaces.Count(place => place.IsEmpty == false);
            ui.SetDoneTasksCount(correctCount, allPlaces.Count);
            if(correctCount >= allPlaces.Count) Win();
        }

        private void Win()
        {
            if(_levelResult == true) return;
            _levelResult = true;
            StopAllCoroutines();
            ui.ActivateWinUI(GetStarsCount());
            SaveSystem.SetOpenedLevelsCount(MenuUI.ActiveLevel + 1);
            SaveSystem.SetLevelStarsCount(MenuUI.ActiveLevel, GetStarsCount());
        }

        private int GetStarsCount()
        {
            foreach (var star in stars)
            {
                if(levelDuration > star.maxTime) continue;
                
                return star.starsCount;
            }

            return 0;
        }
    }

    [Serializable] public class LevelStars
    {
        public int starsCount;
        public int maxTime;
    }
}
