using System.Linq;
using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using E1on.Player;
using UnityEngine;

namespace E1on.Network {
    public class NetworkManager : MonoBehaviour {

        private UnityClient client;
        private PlayerManager playerManager;

        private void Start() {
            client = GameController.getInstance.unityClient;
            playerManager = GameController.getInstance.playerManager;
        }
        
        public void Connect() {
            //client.Connect(System.Net.IPAddress.Parse(GameController.getInstance.ip), GameController.getInstance.port, true);
            client.Connect(System.Net.IPAddress.Parse("127.0.0.1"), GameController.getInstance.port, true);
            client.MessageReceived += MessageReceived;
        }
        
        private void MessageReceived(object sender, MessageReceivedEventArgs e) {
            using (Message message = e.GetMessage() as Message) {
                
                if (message.Tag == Tags.ConnectedPlayer) ConnectedPlayer(e);
                if (message.Tag == Tags.DisconnectedPlayer) DisconnectedPlayer(e);
                
                if (message.Tag == Tags.SpawnNewPlayer) SpawnNewPlayer(e);
                if (message.Tag == Tags.SpawnCurrentPlayer) SpawnCurrentPlayer(e);
                
                if (message.Tag == Tags.PlayerDetails) PlayerDetails(e);
                
            }
        }
        
        // Authentication
        void ConnectedPlayer(MessageReceivedEventArgs e) {
            GameController.getInstance.id = client.ID;
            
            using (DarkRiftWriter writer = DarkRiftWriter.Create()) {
                writer.Write(GameController.getInstance.username);
                writer.Write(GameController.getInstance.password);
                writer.Write(GameController.getInstance.avatar);
                using (Message message = Message.Create(Tags.Authentication, writer)) {
                    client.SendMessage(message, SendMode.Unreliable);
                }
            }
        }

        private void SpawnNewPlayer(MessageReceivedEventArgs e) {
            SpawnPlayer(e);
            using (Message message = e.GetMessage())
            using (DarkRiftReader reader = message.GetReader()) {
                var id = reader.ReadUInt16();
                GameController.getInstance.eventManager.UiListEvent(playerManager.players[id].userName + " joined the server");
            }
        }
        
        private void SpawnCurrentPlayer(MessageReceivedEventArgs e) {
            SpawnPlayer(e);
        }
        
        void SpawnPlayer(MessageReceivedEventArgs e) {
            using (Message message = e.GetMessage())
            using (DarkRiftReader reader = message.GetReader()) {
                
                // Set session
                var session = reader.ReadString();
                if (session.Length > 0) GameController.getInstance.session = session;
                
                while (reader.Position < reader.Length) {
                    var id = reader.ReadUInt16();
                    var username = reader.ReadString();
                    var avatar = reader.ReadUInt16();
                    
                    var playerObject = Instantiate(GameController.getInstance.prefabs.player, Vector3.zero, Quaternion.identity) as GameObject;
                    var avatarPrefab = GameController.getInstance.prefabs.list.ElementAt(avatar);
                    var avatarObject = Instantiate(avatarPrefab.Value, Vector3.zero, Quaternion.identity) as GameObject;
                    avatarObject.transform.parent = playerObject.transform;
                    playerObject.GetComponent<Animator>().avatar = avatarObject.GetComponent<Descriptor>().avatar;
                    
                    var networkPlayer = playerObject.GetComponent<NetworkPlayer>();
                    networkPlayer.userName = username;
                    networkPlayer.avatarType = avatar;
                    playerManager.Add(id, networkPlayer);

                    if (id != client.ID) {
                        Destroy(playerObject.GetComponent<Rigidbody>());
                        Destroy(playerObject.GetComponent<BoxCollider>());
                        Destroy(playerObject.GetComponent<CameraFollow>());
                        Destroy(playerObject.GetComponent<LocalPlayer>());
                        Destroy(playerObject.GetComponent<PlayerMovement>());
                        networkPlayer.SetNameplateName();
                    }
                    else {
                        var localPlayer = playerObject.GetComponent<LocalPlayer>();
                        localPlayer.HideNameplate();

                        Instantiate(GameController.getInstance.prefabs.playerOverlay, Vector3.zero,
                            Quaternion.identity);
                    }
                    
                    Debug.Log("Spawn " + username + " Avatar " + avatar);
                }
            }
        }
        
        void DisconnectedPlayer(MessageReceivedEventArgs e) {
            using (Message message = e.GetMessage()) {
                using (DarkRiftReader reader = message.GetReader()) {
                    var id = reader.ReadUInt16();
                    
                    if (playerManager.players.ContainsKey(id)) {
                        
                        var username = playerManager.players[id].userName;
                        playerManager.DestroyPlayer(id);
                        
                        GameController.getInstance.eventManager.UiListEvent(username + " left the server");
                    }
                }
            }
        }
        
        private void PlayerDetails(MessageReceivedEventArgs e) {
            using (Message message = e.GetMessage())
            using (DarkRiftReader reader = message.GetReader()) {
                
                if (message.Tag == Tags.PlayerDetails) {
                    ushort id = reader.ReadUInt16();
                    
                    if (playerManager.players.ContainsKey(id)) {
                        
                        // Position
                        Vector3 newPosition = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                        // Animators params
                        bool[] animatorParams = new bool[] {
                            reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(),
                            reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean()
                        };

                        // Rotation
                        Quaternion newRotation = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        
                        playerManager.players[id].SetMovePosition(newPosition);
                        playerManager.players[id].SetRotation(newRotation);
                        playerManager.players[id].SetAnimatorParams(animatorParams);
                    }
                }

            }
        }
        
    }
}