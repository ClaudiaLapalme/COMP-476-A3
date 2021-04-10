using UnityEngine;

namespace Structs
{
    public struct PlayerData
    {
        public Vector3 InitialPosition { get;}
        public int Score { get; set; }

        public PlayerData(Vector3 initPos)
        {
            InitialPosition = initPos;
            Score = 0;
        }

        public PlayerData(Vector3 pos, int score)
        {
            InitialPosition = pos;
            Score = score;
        }
    }
}
