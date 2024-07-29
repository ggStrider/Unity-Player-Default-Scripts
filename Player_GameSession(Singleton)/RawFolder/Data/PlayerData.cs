using System;
using System.Collections.Generic;

namespace Data
{
    /// <summary>
    /// Using by GameSessionSingleton.
    /// Component which saves any data.
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        // Save data that you want to save here.
        // For example, save a List of some ScriptableObject to store items, like below.
        public List<ItemInfo> Items;
        
        public int Health;
    }
}
