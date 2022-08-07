using UnityEngine;

namespace Level
{
    public static class SaveSystem
    {
        #region Load
        public static int GetOpenedLevelsCount()
        {
            return PlayerPrefs.GetInt("OpenedLevels", 1);
        }

        public static int GetLevelStarsCount(int level)
        {
            return PlayerPrefs.GetInt("Stars" + level, 0);
        }
        #endregion

        #region Save

        public static void SetOpenedLevelsCount(int openedCount)
        {
            if(GetOpenedLevelsCount() >= openedCount) return;
            
            PlayerPrefs.SetInt("OpenedLevels", openedCount);
        }

        public static void SetLevelStarsCount(int level, int starsCount)
        {
            if(GetLevelStarsCount(level) >= starsCount) return;

            PlayerPrefs.SetInt("Stars" + level, starsCount);
        }

        #endregion
        
    }
}
