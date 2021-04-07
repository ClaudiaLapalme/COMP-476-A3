using UnityEngine;

namespace Structs
{
    public struct PlayerData
    {
        public Vector3 InitialPosition { get; set; }
        public int Score { get; set; }

        public PlayerData(Vector3 initPos)
        {
            InitialPosition = initPos;
            Score = 0;
        }
    }
}
