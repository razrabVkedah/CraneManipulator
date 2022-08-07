using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIManagers
{
    public class LevelUI : MonoBehaviour
    {
        [Header("Win")]
        [SerializeField] private GameObject winUI;
        [SerializeField] private GameObject[] stars;

        [Space] [Header("Lose")]
        [SerializeField] private GameObject loseUI;
        [SerializeField] private Text loseText;
        [SerializeField] private string timeIsOver, ropeIsBroken, objectIsBroken;
        
        [Space] [Header("Time")]
        [SerializeField] private Text timer;
        [SerializeField] private GameObject[] playmodeStars;

        [Space] [Header("Tasks")]
        [SerializeField] private Text doneTasksCount;
        [SerializeField] private string textBetween = "/";

        [SerializeField] private GameObject menu;
        public void OnClickEscapeButton()
        {
            menu.SetActive(menu.activeSelf == false);
        }


        public void DrawTime(int seconds)
        {
            seconds = Mathf.Clamp(seconds, 0, 3600);
            var minutes = seconds / 60;
            seconds -= minutes * 60;

            var time =
                (minutes <= 9 ? "0" + minutes : minutes.ToString())
                + ":" +
                (seconds <= 9 ? "0" + seconds : seconds.ToString());
            timer.text = time;
        }

        public void SetStarsPlaymodeCount(int count)
        {
            for (var i = 0; i < playmodeStars.Length; i++)
            {
                playmodeStars[i].SetActive(count > i);
            }
        }

        public void ActivateWinUI(int starsCount)
        {
            winUI.SetActive(true);

            for (var i = 0; i < stars.Length; i++)
            {
                stars[i].SetActive(starsCount > i);
            }
        }

        public void SetDoneTasksCount(int done, int allTasks)
        {
            doneTasksCount.text = done + textBetween + allTasks;
        }

        public void ActivateLoseUI(LoseReason reason)
        {
            loseUI.SetActive(true);
            loseText.text = reason switch
            {
                LoseReason.TimeIsOver => timeIsOver,
                LoseReason.RopeIsBroken => ropeIsBroken,
                LoseReason.ObjectIsBroken => objectIsBroken,
                _ => ""
            };
        }

        public void OnClickLeaveButton() => SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);

        public void OnClickRestartButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        public void OnClickNextLevelButton()
        {
            MenuUI.NextLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public enum LoseReason : byte
    {
        TimeIsOver,
        RopeIsBroken,
        ObjectIsBroken
    }
}
