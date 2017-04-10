using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Prototype.NetworkLobby
{
    public class NetworkLobbyHook : LobbyHook
    {
       
        public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
        {
            LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
            SetUpLocalPlayer localPlayer = gamePlayer.GetComponent<SetUpLocalPlayer>();

            localPlayer.pname = lobby.name;
            localPlayer.playerColor = lobby.playerColor;
            Debug.Log("Supposed to be this color: " + lobby.playerColor);
        }

    }
}
