using Ghost;
using Photon.Pun;
using Player;
using UnityEngine;

namespace Photon
{
    [RequireComponent(typeof(GameController))]
    public class ServerSetup : MonoBehaviourPunCallbacks
    {
        private bool _isFirstPlayer;
        public void Connect()
        {
            Debug.Log("--> <color=green>CONNECTED to the name server</color> <--");
            PhotonNetwork.ConnectUsingSettings();
        }

        public void CreateRoom()
        {
            Debug.Log("--> <color=yellow>Attempting to JOIN a room</color> <--");
            _isFirstPlayer = true;
            PhotonNetwork.CreateRoom("Roomba");
        }

        public void JoinRandomRoom()
        {
            Debug.Log("--> <color=yellow>Attempting to CREATE a room</color> <--");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnConnected()
        {
            Debug.Log("--> <color=green>CONNECTED to the name server</color> <--");
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("<color=green>CONNECTED TO MASTER</color>");
            JoinRandomRoom();
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("--> <color=green>Successfully CREATED a room</color> <--");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("--> <color=green>Successfully JOINED a room</color> <--");
            if (!_isFirstPlayer)
            {
                gameObject.GetComponent<GameController>().InstantiatePlayers(PlayerStartPoints.TopRight);
            }
            else
            {
                gameObject.GetComponent<GameController>().InstantiatePlayers(PlayerStartPoints.TopLeft);   
                gameObject.GetComponent<GameController>().InstantiateGhosts();   
            }
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log($"Return code: {returnCode}, message: {message}");
            Debug.Log("--> <color=orange>Could NOT join a random room. Attempting to create a room instead</color> <--");
            CreateRoom();
        }

        private void Start()
        {
            Connect();
        }
    }
}
