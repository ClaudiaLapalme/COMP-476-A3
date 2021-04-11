using UnityEngine;

namespace Ghost
{
    public class GhostCollisionController : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag($"PacDot"))
            {
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(),
                    other.gameObject.GetComponentInChildren<Collider>());
            }
        }
    }
}