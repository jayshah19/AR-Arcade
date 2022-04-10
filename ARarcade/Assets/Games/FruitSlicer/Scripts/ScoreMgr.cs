using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FruitSlicer
{
    /// <summary>
    /// This class tracks the player score and handles UI events related to scoring.
    /// </summary>
    public class ScoreMgr : MonoBehaviour
    {
        /// <summary>Tracks Player score.</summary>
        public int Score = 0;

        /// <summary>This is the UI element of type text which handles the display text for score.</summary>
        public Text scoreUI;
        /// <summary>This method increments the score by one and updates the score UI text./summary>
        public void updateScore()
        {
            Score++;
            scoreUI.text = "Score: " + Score;
        }
    }
}
