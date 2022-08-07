using UnityEngine;

namespace HelpScripts
{
    public class FPS : MonoBehaviour
    {
        public float updateInterval = 0.5f;

        private float _acu;
        private int _frames;
        private float _timeLeft;
        private float _fps;

        private readonly GUIStyle _textStyle = new();


        private void Start()
        {
            _timeLeft = updateInterval;
            _textStyle.fontStyle = FontStyle.Bold;
            _textStyle.normal.textColor = Color.white;
        }

        private void Update()
        {
            _timeLeft -= Time.deltaTime;
            _acu += Time.timeScale / Time.deltaTime;
            ++_frames;


            if (_timeLeft > 0.0) return;
           
            _fps = (_acu / _frames);
            _timeLeft = updateInterval;
            _acu = 0.0f;
            _frames = 0;
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(5, 5, 100, 25), _fps.ToString("F2") + "FPS", _textStyle);
        }
    }
}
