using UnityEngine;

namespace Data
{
    /// <summary>
    /// !WARN!
    /// This component must ONLY be on ONE SCENE!
    /// For example in start scene.
    /// </summary>
    public class GameSessionSingleton : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;
        
        public static GameSessionSingleton Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void _SaveHealth(int hp)
        {
            _data.Health = hp;
        }

        // You can get info from methods, or from Data variable
        public int _GetHealth()
        {
            return Data.Health;
        }
    }
}
