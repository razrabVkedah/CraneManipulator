using Level;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIManagers
{
    public class MenuUI : MonoBehaviour
    {
        private void Start()
        {
            SetVolumeSettings();
            SetSensitivitySettings();
            SetLevelsSettings();
        }


        #region Volume
        [Header("Volume")]
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private Slider gameVolumeSlider, musicVolumeSlider, uiVolumeSlider;

        private void SetVolumeSettings()
        {
            gameVolumeSlider.value = GetVolume("GameSounds");
            musicVolumeSlider.value = GetVolume("Music");
            uiVolumeSlider.value = GetVolume("UI");
        }
        public void OnGameVolumeSliderValueChanged()
        {
            mixer.SetFloat("GameSounds", gameVolumeSlider.value);
        }
        
        public void OnMusicVolumeSliderValueChanged()
        {
            mixer.SetFloat("Music", musicVolumeSlider.value);
        }
        
        public void OnUIVolumeSliderValueChanged()
        {
            mixer.SetFloat("UI", uiVolumeSlider.value);
        }
        
        private float GetVolume(string mixerValueName)
        {
            mixer.GetFloat(mixerValueName, out var result);
            return result;
        }
        #endregion
        
        #region Sensitivity
        public static float SensitivityX { get; private set; } = 100f;
        public static float SensitivityY { get; private set; } = 100f;

        [Space] [Space] [Header("Sensitivity")]
        [SerializeField] private Slider sensitivityXSlider;
        [SerializeField] private Slider sensitivityYSlider;

        private void SetSensitivitySettings()
        {
            sensitivityXSlider.value = SensitivityX;
            sensitivityYSlider.value = SensitivityY;
        }
        public void OnSensitivityXSliderValueChanged()
        {
            SensitivityX = sensitivityXSlider.value;
        }
        
        public void OnSensitivityYSliderValueChanged()
        {
            SensitivityY = sensitivityYSlider.value;
        }
        #endregion

        #region Levels

        public static int ActiveLevel { get; private set; } = 6;
        
        [SerializeField] private LevelButton[] allLevels;

        private void SetLevelsSettings()
        {
            var openedLevelsCount = SaveSystem.GetOpenedLevelsCount();
            for (var i = 0; i < allLevels.Length; i++)
            {
                if (openedLevelsCount > i)
                {
                    var starsCount = SaveSystem.GetLevelStarsCount(i + 1);
                    allLevels[i].SetLevelOpened(starsCount);
                    continue;
                }
                
                allLevels[i].SetLevelClosed();
            }
        }

        public static void NextLevel()
        {
            ActiveLevel++;
            if (ActiveLevel > 5) ActiveLevel = 5;
        }

        public void OnClickPlayButton()
        {
            var openedLevelCount = SaveSystem.GetOpenedLevelsCount();
            if (openedLevelCount > 5) openedLevelCount = 5;
            ActiveLevel = openedLevelCount;
            SceneManager.LoadScene("Levels 1-5", LoadSceneMode.Single);
        }
        public void OnClickLoadLevel(int level)
        {
            ActiveLevel = level;
            SceneManager.LoadScene("Levels 1-5", LoadSceneMode.Single);
        }
        #endregion
        
    }
}
