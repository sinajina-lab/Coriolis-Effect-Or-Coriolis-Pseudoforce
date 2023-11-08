using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBasketToStartGame : MonoBehaviour
{
    public void PlayGame()
    {
        //SceneManager.LoadSceneAsync("SampleScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cannonball"))
        {
            StartCoroutine(StartGameAfterDelay(3f)); // Start the coroutine to wait for 5 seconds
        }
    }

    IEnumerator StartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync("SampleScene");
    }
}
