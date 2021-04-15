using UnityEngine;

namespace Player
{
    public class CollisionController : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            // players can pass through each other
            if (other.gameObject.CompareTag($"Player"))
            {
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(),
                    other.gameObject.GetComponentInChildren<Collider>());
            }
        }
    }
}