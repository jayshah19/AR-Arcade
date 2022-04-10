using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;



namespace BeerPong
{

    /// <summary>
    /// This class contains the variable and method to spawn BeerCup GameObject on a detected AR Plane for the BeerPong game in ARArcade Project.
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class PlaceCup : MonoBehaviour
    {
        /// <summary>This is the object of the class GameObject. It holds the prefab of the BeerCup gameObject that has to be spawned.</summary>
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]

        private GameObject BeerCup;

        /// <summary>
        /// The prefab that is instantiated on touch.
        /// </summary>
        public GameObject placedBeerCup
        {
            get { return BeerCup; }
            set { BeerCup = value; }
        }

        /// <summary>
        /// The object that is instantiated as a result of a successful raycast intersection with a the detected plane.
        /// </summary>
        public GameObject spawnedBeerCup { get; private set; }

        /// <summary>This is an object of the class GameObject. This GameObject holds the prefab of the throwable ball that is to be instantiated after the successful placement of BeerCup GameObject.</summary>
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject Ball;

        /// <summary>
        /// The prefab that is instantiated automatically if the cup is placed.
        /// </summary>
        public GameObject placedBall
        {
            get { return placedBall; }
            set { placedBall = value; }
        }

        /// <summary>
        /// The spawned ball.
        /// </summary>
        public GameObject spawnedBall { get; private set; }


        /// <summary>
        /// Invoked whenever an object is placed in on a plane.
        /// </summary>
        public static event Action onPlacedObject;

        /// <summary>
        /// Object of the ARRaycastManager from the ARFoundation package. 
        /// </summary>
        ARRaycastManager m_RaycastManager;
        /// <summary>
        /// List of ARRaycasthits to detect the Raycast Hits from Camera to Detected Planes.
        /// </summary>
        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();


        /// <summary>Boolean that checks wether the BeerCup game object is placed on the detected plane.</summary>
        private bool isBeerCupPlaced = false;

        /// <summary>
        /// This is a unity method that is called when the scene is loaded.
        /// </summary>

        public GameObject niceshot;
        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
            niceshot.gameObject.SetActive(false);
        }

        /// <summary>
        /// This is a unity method that is called every frame.
        /// </summary>
        void Update()
        {

            ///<remarks>Checks if the beer cup is placed. If it is not placed then the code below will be executed, otherwise the function will return at this step.</remarks>
            if (isBeerCupPlaced)
                return;

            ///<remarks>Checks wether the user has touched the screen where the plane is detected. If true then the BeerCup GameObject will be instantiated and the first throwable ball will be spawned.</remarks>
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                    {
                        Pose hitPose = s_Hits[0].pose;

                        spawnedBeerCup = Instantiate(placedBeerCup, hitPose.position, Quaternion.AngleAxis(100,Vector3.up));
                        spawnedBeerCup.transform.parent = transform.parent;

                        isBeerCupPlaced = true;
                        spawnedBall = Instantiate(Ball);
                        spawnedBall.transform.parent = m_RaycastManager.gameObject.transform.Find("AR Camera").gameObject.transform;

                        if (onPlacedObject != null)
                        {
                            onPlacedObject();
                        }
                    }
                }
            }
        }

        public void startNiceshotDisplayCouritine()
        {
            StartCoroutine("niceShotDisplay");
        }
        IEnumerator niceShotDisplay()
        {
            niceshot.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            niceshot.gameObject.SetActive(false);
        }
    }
}