using UnityEngine;

namespace Structs
{
    public struct PlayerData
    {
        public Vector3 InitialPosition { get;}
        public int Score { get; set; }
        
        public bool WasTeleported { get; set; }

        public PlayerData(Vector3 initPos)
        {
            InitialPosition = initPos;
            Score = 0;
            WasTeleported = false;
        }

        public PlayerData(Vector3 pos, int score, bool wasTeleported)
        {
            InitialPosition = pos;
            Score = score;
            WasTeleported = wasTeleported;
        }
    }
}
