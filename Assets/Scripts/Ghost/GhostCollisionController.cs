using Player;
using Structs;
using UnityEngine;

namespace Ghost
{
    public class GhostCollisionController : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            // Pass through other ghosts and pac dots
            if (other.gameObject.CompareTag($"PacDot") || other.gameObject.CompareTag($"Ghost")) 
            {
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(),
                    other.gameObject.GetComponentInChildren<Collider>());
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                var playerData = other.gameObject.GetComponent<PlayerDataController>().PlayerData;
                var initPos = playerData.InitialPosition;
                var score = playerData.Score;
                other.gameObject.GetComponent<PlayerDataController>().PlayerData = new PlayerData(initPos, score, true);
                other.gameObject.transform.position = initPos;
            }
        }
    }
}