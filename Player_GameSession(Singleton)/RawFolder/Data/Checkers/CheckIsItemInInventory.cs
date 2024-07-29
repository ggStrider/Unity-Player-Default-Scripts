using UnityEngine;
using UnityEngine.Events;

namespace Data.Checkers
{
    public class CheckIsItemInInventory : MonoBehaviour
    {
        [SerializeField] private ItemInfo _itemToCheckInInventory;

        [Space] [Header("Events")]
        [SerializeField] private UnityEvent _onContains;
        [SerializeField] private UnityEvent _onDoesntContains;

        private GameSessionSingleton _gameSession;

        private void Start() => _gameSession = GameSessionSingleton.Instance;

        /// <summary>
        /// Checks if GameSession.Data.Items contains ItemInfo, and invokes an event
        /// depending on whether it contains it or not.
        /// </summary>
        [ContextMenu("Check through context menu")]
        public void _Check()
        {
            var contains = _gameSession.Data.Items.Contains(_itemToCheckInInventory);

            (contains ? _onContains : _onDoesntContains)?.Invoke();
        }
    }
}
