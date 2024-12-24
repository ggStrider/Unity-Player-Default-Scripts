using UnityEngine;

namespace PlayerDefault.Scripts.Interact
{
    public class InteractDisableGameObject : MonoBehaviour, IInteract
    {
        public void OnInteract()
        {
            gameObject.SetActive(false);
        }
    }
}