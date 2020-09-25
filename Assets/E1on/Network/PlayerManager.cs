using System.Collections.Generic;
using UnityEngine;

namespace E1on.Network {

    public class PlayerManager : MonoBehaviour {
        
        public Dictionary<ushort, NetworkPlayer> players = new Dictionary<ushort, NetworkPlayer>();

        public void Add(ushort id, NetworkPlayer player) {
            this.players.Add(id, player);
        }
        
        public void DestroyPlayer(ushort id) {
            if (!players.ContainsKey(id)) return;
            NetworkPlayer player = players[id];
            Destroy(player.gameObject);
            players.Remove(id);
        }
        
    }
}