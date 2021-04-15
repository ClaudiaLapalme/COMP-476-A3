using Ghost;
using Photon.Pun;
using UnityEngine;

namespace Photon
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject ghostPrefab;

        public void InstantiatePlayers(Vector3 pos)
        {
            if (playerPrefab == null)
            {
                Debug.LogError(
                    "<color=Red>Missing playerPrefab Reference. Set it up in GameObject 'GameController'</color>",
                    this);
            }
            else
            {
                Debug.Log("<color=green>Instantiating prefabs!</color>");
                var player = PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
                player.name = "PacPerson" + PhotonNetwork.CurrentRoom.PlayerCount;
            }
        }

        public void InstantiateGhosts()
        {
            if (playerPrefab == null)
            {
                Debug.LogError(
                    "<color=Red>Missing playerPrefab Reference. Set it up in GameObject 'GameController'</color>",
                    this);
            }
            else
            {
                Debug.Log("<color=green>Instantiating prefabs!</color>");
                var ghost1 = PhotonNetwork.Instantiate(ghostPrefab.name, GhostStartPoints.Pos1, Quaternion.identity);
                ghost1.name = "Ghost1";
                
                var ghost2 = PhotonNetwork.Instantiate(ghostPrefab.name, GhostStartPoints.Pos2, Quaternion.identity);
                ghost2.name = "Ghost2";
            }
        }
    }
}