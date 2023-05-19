using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private List<GameObject> playerPrefabs;
    [SerializeField] private List<GameObject> playerPrefabSpawned= new List<GameObject>();
 
    public int numberOfPlayersToSpawn =0;
    [ServerRpc(RequireOwnership = false)] //server owns this object but client can request a spawn
    public void SpawnPlayerServerRpc(ulong clientId, int prefabId)
    {
 
        GameObject newPlayer = Instantiate( playerPrefabs[prefabId]);
        
        var netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId, true);
        playerPrefabSpawned.Add(newPlayer);
    }
    
}