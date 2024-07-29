using UnityEngine;

namespace Data
{
    /// <summary>
    /// Scriptable component, to create items
    /// </summary>
    [CreateAssetMenu(menuName = "My/Item", fileName = "ItemName")]
    public class ItemInfo : ScriptableObject
    {
        public string ItemName;
    }
}
