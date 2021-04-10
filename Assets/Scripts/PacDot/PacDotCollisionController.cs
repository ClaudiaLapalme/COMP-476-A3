using Player;
using Structs;
using UnityEngine;

namespace PacDot
{
    public class PacDotCollisionController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var playerData = other.GetComponent<PlayerDataController>().PlayerData;
                playerData.Score++;
                other.GetComponent<PlayerDataController>().PlayerData =
                    new PlayerData(playerData.InitialPosition, playerData.Score);
                Destroy(gameObject);
            }
        }
    }
}