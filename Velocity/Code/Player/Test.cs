using UnityEngine;

namespace Code.Player
{
    public class Test : MonoBehaviour
    {
        [ContextMenu("test")]
        public void Teseet()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}