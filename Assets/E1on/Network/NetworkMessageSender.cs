using DarkRift;
using UnityEngine;

namespace E1on.Network {
    public class NetworkMessageSender : MonoBehaviour {
        
        public void Authorization(string login, string password, ushort avatar) {
            
            using (DarkRiftWriter writer = DarkRiftWriter.Create()) {
                writer.Write(login);
                writer.Write(password);
                writer.Write(avatar);
                
                using (Message message = Message.Create(Tags.Authorization, writer))
                    GameController.getInstance.unityClient.SendMessage(message, SendMode.Reliable);
            }
            
        }
        
        public void ConnectionToInstance(string session) {
            
            using (DarkRiftWriter writer = DarkRiftWriter.Create()) {
                writer.Write(session);
                
                using (Message message = Message.Create(Tags.ConnectionToInstance, writer))
                    GameController.getInstance.unityClient.SendMessage(message, SendMode.Reliable);
            }
        }
        
    }
}