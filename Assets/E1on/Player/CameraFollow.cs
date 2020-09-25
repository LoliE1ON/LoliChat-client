using E1on.Player;
using UnityEngine;

namespace E1on {
    public class CameraFollow : MonoBehaviour {

        private GameObject camera;
        private bool frontCamera = false;
        
        // Front
        private Transform frontViewPoint;
        public float horizontalSpeed = 1f;
        public float verticalSpeed = 1f;
        private float xRotation = 0.0f;
        private float yRotation = 0.0f;
        
        // Around
        private float xRot = 0f;
        private float yRot = 0f;
        public float distance = 5f;
        
        public float sensitivity = 100f;
        public float offsetTop = 1f;
        public float scrollSpeed = 10f;

        private void Start() {
            camera = Camera.main.transform.gameObject;
            frontViewPoint = GetComponentInChildren<Descriptor>().viewPoint;
        }

        private void Update() {
            
            // Toggle view camera
            if (Input.GetKeyDown(KeyCode.V)) this.frontCamera = !this.frontCamera;

            if (this.frontCamera) this.Front();
            else this.Around();
            
            // Rotate player
            Vector3 rotation = this.transform.eulerAngles;            
            rotation.y = this.camera.transform.eulerAngles.y;            
            this.transform.eulerAngles = rotation;

        }

        private void Front() {
            this.camera.transform.position = this.frontViewPoint.position;
            float mouseX = Input.GetAxis("Mouse X") * this.horizontalSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * this.verticalSpeed;
 
            this.yRotation += mouseX;
            this.xRotation -= mouseY;
            if (xRotation > 75) xRotation = 75;
            
            this.xRotation = Mathf.Clamp(xRotation, -90, 90);
 
            this.camera.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
        }
        
        private void Around() {
            xRot += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            yRot += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            
            // Limit view
            if(xRot > -10f) xRot = -10f;
            else if(xRot < -50f) xRot = -50f;
            
            // Scroll
            if (Input.mouseScrollDelta.y > 0) this.distance -= this.scrollSpeed * Time.deltaTime;
            else if(Input.mouseScrollDelta.y < 0) this.distance += this.scrollSpeed * Time.deltaTime;
            if (this.distance > 10) this.distance = 10;
            if (this.distance < 0.1f) this.distance = 0.1f;
            
            camera.transform.position = this.transform.position + Quaternion.Euler(xRot, yRot, 0) * (distance * -Vector3.back);
            camera.transform.LookAt(this.transform.position+new Vector3(0, this.offsetTop, 0), Vector3.up);
        }
    }
}