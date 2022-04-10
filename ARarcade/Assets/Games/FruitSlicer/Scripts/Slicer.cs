using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FruitSlicer;

namespace FruitSlicer
{

    /// <summary>
    /// This class contain methods to handle the slicing mechanism.
    /// </summary>
    public class Slicer : MonoBehaviour
    {
        /// <summary>Object to store reference to the AR Camera.</summary>
        public GameObject arCam;
        /// <summary>Particles that will be spawned when a fruit is sucessfully sliced.</summary>
        public GameObject particles;

        /// <summary>Variable of type RaycastHit.</summary>
        RaycastHit hit;
        /// <summary>Stores a reference to the ScoreMgr class.</summary>
        public ScoreMgr sm;

        private void Start()
        {
            sm = this.GetComponent<ScoreMgr>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            Slice();
        }

        /// <summary>
        /// Launches a RayCast from the center of the camera and checks if it hits any game object. If the raycast hits a gameobject with tag fruit, this means we have sucessfully sliced the fruit and we spawn the particles at the place where raycast hit the fruit and updates the player score by calling updateScore() method from class ScoreMgr.
        /// </summary>
        public void Slice()
        {

            if (Physics.Raycast(arCam.transform.position, arCam.transform.forward, out hit))
            {
                if (hit.transform.tag == "fruit")
                {
                    Destroy(hit.transform.gameObject);
                    Instantiate(particles, hit.point, Quaternion.LookRotation(hit.normal));
                    sm.updateScore();
                }
            }

        }

    }
}