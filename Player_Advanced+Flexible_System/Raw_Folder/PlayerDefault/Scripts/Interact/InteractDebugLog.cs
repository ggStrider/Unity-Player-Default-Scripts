using UnityEngine;

namespace PlayerDefault.Scripts.Interact
{
    public class InteractDebugLog : MonoBehaviour, IInteract
    {
        [SerializeField] private string _log;
        
        public void OnInteract()
        {
            Debug.Log($"Interacted! {_log}");
        }
    }
}