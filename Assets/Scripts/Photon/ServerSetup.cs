using Photon.Pun;
using UnityEngine;

namespace Photon
{
    public class ServerSetup : MonoBehaviourPunCallbacks
    {
        public void Connect()
        {
            Debug.Log("--> <color=green>CONNECTED to the name server</color> <--");
            PhotonNetwork.ConnectUsingSettings();
        }

        public void CreateRoom()
        {
            Debug.Log("--> <color=yellow>Attempting to JOIN a room</color> <--");
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
