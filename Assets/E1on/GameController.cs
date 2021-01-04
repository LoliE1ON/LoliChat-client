using System;
using DarkRift;
using DarkRift.Client.Unity;
using E1on.Asset;
using E1on.Network;
using E1on.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace E1on {
    public class GameController : MonoBehaviour {
        
        public static GameController getInstance;

        [Header("Current player")]
        public ushort id;
        public string username = "";
        public string password = "";
        public string session = "";
        public ushort avatar = 0;

        [Header("Scripts")] 
        public EventManager eventManager;
        public PlayerManager playerManager;
        public UnityClient unityClient;
        public Prefabs prefabs;
        public NetworkManager networkManager;
        public NetworkMessageSender networkMessageSender;
        public AvatarManager avatarManager;
        
        [Header("Server settings")]
        public string ip = "5.180.138.37";
        public int port = 4296;
        
        private void Awake () {
            if (getInstance == null) {
                DontDestroyOnLoad (gameObject);
                getInstance = this;
            }
        }
    }
}