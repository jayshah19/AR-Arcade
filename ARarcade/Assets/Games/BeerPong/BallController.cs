using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


namespace BeerPong 
{
    /// <summary>
    /// This class contains the property of the throwable ball and methods that controls the movement, speed and spawning of the ball.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class BallController : MonoBehaviour
    {
        /// <summary>The base force at which the ball will be thrown.</summary>
        public float throwForce =10f;
        /// <summary>The base throw direction on the X axis.</summary>
        public float throwDirX = 0.17f;
        /// <summary>The base throw direction on the Y axis.</summary>
        public float throwDirY = 0.67f;

        /// <summary>The spawning offset of the ball in relation to the Camera.</summary>
        public Vector3 ballCameraOffset = new Vector3(0f,-0.4f,1f);

        /// <summary>The start Position of the spawned ball.</summary>
        private Vector3 startPosition;
        /// <summary>The throw direction of the ball as per detected by the user swipe.</summary>
        private Vector3 throwDirection;

        /// <summary>The start time of the swipe.</summary>
        private float startTime;
        /// <summary>The end time of the swipe.</summary>
        private float endTime;
        /// <summary>The total duration of the swipe.</summary>
        private float throwDuration;

        /// <summary>Boolean that checks if the throw direction has been chosen.</summary>
        private bool directionChosen = false;
        /// <summary>Boolean that checks if the throw was started.</summary>
        private bool throwStarted = false;
        /// <summary>GameObject that will refer to the ARCamera</summary>
        [SerializeField] GameObject ARCam;

        /// <summary>Object of the class ARSessionOrigin that stores a refrence to the Origin of AR Scene</summary>
        [SerializeField] ARSessionOrigin sessionOrigin;

        /// <summary>Rigidbody of the ball that provides physics to the ball</summary>
        Rigidbody myRigidBody;


        /// <summary>
        /// This method is called at the begining of the unity scene. The initial values of the variables myRigidBody, sessionOrigin and ARCam is assigned here. Furthermore, the parent of the ball object is set here (which is the ARCamera) and lastly the method ResetBall is called here to reset the properties of the ball. 
        /// </summary>
        private void Start()
        {
            myRigidBody = gameObject.GetComponent<Rigidbody>();
            sessionOrigin = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
            ARCam = sessionOrigin.transform.Find("AR Camera").gameObject;
            transform.parent = ARCam.transform;
            ResetBall();
        }
        /// <summary>
        /// <para>
        /// This is a unity method that is called every frame.
        /// </para>
        /// <para>
        /// Initially, in the method, it is checked wether the user has provided a touch input. If yes we record the start time and position of the touch, and set the value of throw started to true.
        /// </para>
        /// <para>
        /// Then we wait till the user has lifted their finger and record the end time and position of the touch, calculate the duration of the touch and the direction of the touch by subtracting the last position of the touch by startPosition. Lastly we set the directionChosen variable to true.
        /// </para>
        /// <para>
        /// After the direction is chosen, the ball is given some mass and gravity for the rigidbody of the ball is turned on. The force that has to be applied to throw the ball is calculated by the formula:  ARCam.transform.forward * throwForce / throwDuration + ARCam.transform.up* throwDirY * throwDirection.y + ARCam.transform.right* throwDirX * throwDirection.x
        /// </para>
        /// <para>
        /// Lastly, all the variables requried to be reset for the next throw are reset and the method ResetBall is called to reset the properties of the ball and spawn a new ball.
        /// </para>
        /// </summary>
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPosition = Input.mousePosition;
                startTime = Time.time;
                throwStarted = true;
                directionChosen = false;
            } 
            else if(Input.GetMouseButtonUp(0))
            {
                endTime = Time.time;
                throwDuration = endTime - startTime;
                throwDirection = Input.mousePosition - startPosition;
                directionChosen = true;
            }

            if(directionChosen)
            {
                myRigidBody.mass = 1;
                myRigidBody.useGravity = true;
                myRigidBody.AddForce(ARCam.transform.forward * throwForce / throwDuration +
                    ARCam.transform.up * throwDirY * throwDirection.y + 
                    ARCam.transform.right * throwDirX * throwDirection.x);

                startTime = 0.0f;
                throwDuration = 0.0f;

                startPosition = new Vector3( 0, 0, 0);
                throwDirection = new Vector3(0, 0, 0);
                throwStarted = false;
                directionChosen = false;
            }

            if (Time.time - endTime >= 3 && Time.time - endTime <= 4)
                ResetBall();
        }

        /// <summary>
        /// This method resets the physics properties of the ball and sets the initial position of the ball before the throw starts.
        /// </summary>
        public void ResetBall()
        {
            myRigidBody.mass = 0;
            myRigidBody.useGravity = false;
            myRigidBody.velocity = Vector3.zero;
            myRigidBody.angularVelocity = Vector3.zero;
            endTime = 0.0f;

            Vector3 ballPos = ARCam.transform.position + ARCam.transform.forward * ballCameraOffset.z + ARCam.transform.up * ballCameraOffset.y;
            transform.position = ballPos;
        }
    }
}