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
        /// <summary>
        /// Data to set (change). Actually data.
        /// </summary>
        [SerializeField] private PlayerData _data;
        
        /// <summary>
        /// Data to get (readonly). This data contains _data
        /// </summary>
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

        // You can get (any) info from methods, or from Data variable
        public int _GetHealth()
        {
            return Data.Health;
        }

        /// <summary>
        /// Adds item to PlayerData.Items
        /// </summary>
        /// <param name="item">item instance to be added</param>
        public void _AddItem(ItemInfo item)
        {
            _data.Items.Add(item);
        }

        /// <summary>
        /// Deletes an item in the PlayerData.Items,
        /// if the PlayerData.Items contains it.
        /// </summary>
        /// <param name="item">item instance to be deleted</param>
        public void _RemoveItem(ItemInfo item)
        {
            var contains = _data.Items.Contains(item);

            if (!contains)
            {
                Debug.Log($"Doesnt contain {item} in inventory. Return from method");
                return;
            }
            
            _data.Items.Remove(item);
        }
    }
}
