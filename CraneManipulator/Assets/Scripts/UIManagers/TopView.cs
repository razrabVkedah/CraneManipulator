using UnityEngine;

namespace UIManagers
{
    public class TopView : MonoBehaviour
    {
        [SerializeField] private GameObject viewRenderer;
        [SerializeField] private GameObject text;
        
        public void OnClickTopView()
        {
            viewRenderer.SetActive(!viewRenderer.activeSelf);
            text.SetActive(viewRenderer.activeSelf == false);
        }
    }
}
