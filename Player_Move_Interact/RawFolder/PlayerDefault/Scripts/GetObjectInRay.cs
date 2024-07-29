using UnityEngine;

namespace PlayerDefault.Scripts
{
    public class GetObjectInRay : MonoBehaviour
    {
        public GameObject _Get(Vector3 position, Vector3 direction, float distance)
        {
            return Physics.Raycast(position, 
                direction, out var hitInfo, distance) ? hitInfo.collider.gameObject : null;
        }
    }
}