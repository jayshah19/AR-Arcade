using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitSlicer
{
    /// <summary>
    /// This class contains variables and methods to spawn fruits in the Game scene.
    /// </summary>
    public class FruitSpawner : MonoBehaviour
    {
        /// <summary>An array of type GameObject that stores the fruits to be spawned.</summary>
        public GameObject[] fruitPrefabs;
        /// <summary>An array of type Transform that stores the spawn points where the fruits will be spawned periodically.</summary>
        public Transform[] spawnPoints;


        /// <summary>
        /// This method is called at the start of the game scene. Calls the coroutine that begins to spawn fruits.
        /// </summary>
        void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;

            StartCoroutine("spawnFruits");
        }


        /// <summary>
        /// This method spawns random fruits from the fruitPrefabs array at random spawn points from spawnPoints array. Furthermore, we apply force to the spawned fruits in the upward direction, wait for a 4 seconds and destroy the fruit object if player was not able to slice the fruit.
        /// </summary>
        /// <returns></returns>
        IEnumerator spawnFruits()
        {
            GameObject gm = Instantiate(fruitPrefabs[Random.Range(0, fruitPrefabs.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)]);
            Vector3 forceDir = new Vector3(0, 1, 0);
            gm.GetComponent<Rigidbody>().AddForce(transform.up * 700);
            yield return new WaitForSeconds(4.0f);
            Destroy(gm);
            StartCoroutine("spawnFruits");
        }
    }
}
