using Structs;
using UnityEngine;

namespace Player
{
    public class PlayerDataController : MonoBehaviour
    {
        public PlayerData PlayerData { get; set; }

        private void Start()
        {
            PlayerData = new PlayerData(transform.position);
        }
    }
}
