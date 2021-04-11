using Player;
using Structs;
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
            else if (other.gameObject.CompareTag("Player"))
            {
                var initPos = other.gameObject.GetComponent<PlayerDataController>().PlayerData.InitialPosition;
                other.gameObject.transform.position = initPos;
            }
        }
    }
}