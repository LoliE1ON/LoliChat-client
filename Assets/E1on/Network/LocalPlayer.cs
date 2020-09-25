using DarkRift;
using DarkRift.Client.Unity;
using UnityEngine;

namespace E1on.Network {
    public class LocalPlayer : MonoBehaviour {

        private UnityClient client;
        private Animator animator;
        private Transform playerTransform;
        
        [SerializeField]
        [Tooltip("The distance we can move before we send a position update.")]
        float moveDistance = 0.05f;
        
        private Vector3 lastPosition;
        private Quaternion lastRotation;
        

        void Start() {

            client = GameController.getInstance.unityClient;
            animator = GetComponent<Animator>();
            playerTransform = GetComponent<Transform>();
            
            lastPosition = playerTransform.position;
            this.lastRotation = playerTransform.rotation;
        }

        void Update() {
            if ((Vector3.Distance(lastPosition, transform.position) > moveDistance) || lastRotation != transform.rotation) {
                
                using (DarkRiftWriter writer = DarkRiftWriter.Create())
                {
                    // Position
                    writer.Write(transform.position.x);
                    writer.Write(transform.position.y);
                    writer.Write(transform.position.z);
                    
                    // Animator params
                    writer.Write(animator.GetBool("isWalking"));
                    writer.Write(animator.GetBool("isRunning"));
                    writer.Write(animator.GetBool("isBackWalking"));
                    writer.Write(animator.GetBool("isRightWalking"));
                    writer.Write(animator.GetBool("isLeftWalking"));
                    writer.Write(animator.GetBool("isDirectRightWalking"));
                    writer.Write(animator.GetBool("isDirectLeftWalking"));
                    writer.Write(animator.GetBool("isJumpRun"));
                    
                    // Rotation
                    writer.Write(transform.rotation.x);
                    writer.Write(transform.rotation.y);
                    writer.Write(transform.rotation.z);
                    writer.Write(transform.rotation.w);
                    
                    using (Message message = Message.Create(Tags.PlayerDetails, writer))
                        client.SendMessage(message, SendMode.Unreliable);
                }

                lastPosition = playerTransform.position;
                lastRotation = playerTransform.rotation;
            }
        }

        public void HideNameplate() {
            GetComponentInChildren<Canvas>().gameObject.SetActive(false);
        }
    }
}