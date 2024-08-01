using UnityEngine;

namespace PlayerDefault.Scripts
{
    public class GetObjectInRay : MonoBehaviour
    {
        /// <summary>
        /// Takes game object using ray. No check by layer
        /// </summary>
        /// <param name="position">where ray starts casting</param>
        /// <param name="direction">direction where ray casts</param>
        /// <param name="distance">distance of ray</param>
        /// <returns>GameObject which was in ray or null</returns>
        public GameObject _Get(Vector3 position, Vector3 direction, float distance)
        {
            return Physics.Raycast(position, 
                direction, out var hitInfo, distance) ? hitInfo.collider.gameObject : null;
        }
    }
}