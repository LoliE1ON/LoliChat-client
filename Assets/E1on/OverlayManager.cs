using System;
using System.Collections.Generic;
using System.Linq;
using E1on.Network;
using UnityEngine;
using UnityEngine.UI;
using NetworkPlayer = E1on.Network.NetworkPlayer;

namespace E1on {
    public class OverlayManager : MonoBehaviour {
        
        // Model
        private PlayerManager playerManager;
        
        // UI
        public Text totalPlayersText;
        public Text eventsListText;
        public Text listPlayers;
        
        private int totalPlayers = 0;
        
        // Events
        private Dictionary<string, long> events = new Dictionary<string, long>();
        public int eventActiveTime = 10;
        
        private void Start() {
            playerManager = GameController.getInstance.playerManager;
            totalPlayers = playerManager.players.Count;
            
            EventManager.ConnectionEvent+= SetEvent;
        }

        private void Update() {
            ComputedTotalPlayers();
            HandleEvents();
        }

        private void ComputedTotalPlayers() {
            int total = playerManager.players.Count;
            if (totalPlayers != total) {
                totalPlayersText.text = total.ToString();
            }
        }

        private void HandleEvents() {
            
            if (events.Count == 0 && eventsListText.text.Length == 0) return;
            
            string eventsString = "";
            foreach (KeyValuePair<string, long> item in events) eventsString = eventsString + item.Key + "\n";
            eventsListText.text = eventsString;
            
            // Remove events
            long time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            for (int i = 0; i < events.Count; i++) {
                if (time > (events.ElementAt(i).Value + eventActiveTime)) events.Remove(events.ElementAt(i).Key);
            }

        }

        public void SetEvent(string message) {
            if (events.ContainsKey(message)) return;
            
            long time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            events.Add(message, time);

            UpdatePlayersList();
        }
        
        public void UpdatePlayersList() {
            listPlayers.text = "";

            if (playerManager.players.Count < 2) listPlayers.text = "No players";
            else {
                foreach (KeyValuePair<ushort, NetworkPlayer> player in playerManager.players) 
                    if( player.Key != GameController.getInstance.id) listPlayers.text += player.Value.userName + "\n";
            }

        }
    }
}