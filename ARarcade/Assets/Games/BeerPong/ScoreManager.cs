using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BeerPong;

namespace BeerPong
{
    /// <summary>
    /// This class tracks the player score and handles UI events related to scoring.
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        /// <summary>Tracks Player score.</summary>
        public int Score = 0;

        /// <summary>This is the UI element of type text which handles the display text for score.</summary>
        public Text scoreUI;

        /// <summary>Stored a reference to the object with PlaceCup script attached.</summary>
        public PlaceCup placeCupRef;
        /// <summary>
        /// This method will be called at the start of the scene. We initialize the scoreUI and placeCupRef variables here.
        /// </summary>
        void Start()
        {
            //cupCollider = this.GetComponent<BoxCollider>();
            scoreUI = GameObject.Find("ScoreUI").GetComponent<Text>();
            scoreUI.text = "Score: " + Score;
            placeCupRef = GameObject.Find("AR Session Origin").GetComponent<PlaceCup>();
        }

        /// <summary>
        /// This method is called when the trigger is activated upon collision. When colliding with objects that have tag "Ball", we increase the score by one and update the score UI with current player score.
        /// </summary>
        /// <param name="other">Any other object with collider attached except the one this script is attached to.</param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Ball")
            {
                Score += 1;
                scoreUI.text = "Score: " + Score;
                placeCupRef.startNiceshotDisplayCouritine();
            }
        }


    }
}