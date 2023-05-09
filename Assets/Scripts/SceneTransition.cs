using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SceneTransition : MonoBehaviour
{
    public Animator transitionAnimator;
    public float transitionDelay = 1f;

    public void LoadLevel1()
    {
        StartCoroutine(TransitionToScene("Level1"));
    }

    public void LoadLevel2()
    {
        StartCoroutine(TransitionToScene("Level2"));
    }

    public void LoadBossFight()
    {
        StartCoroutine(TransitionToScene("BossFight"));
    }

    IEnumerator TransitionToScene(string sceneName)
    {
        // Trigger the transition animation
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("Start");
        }

        // Wait for a short delay before loading the new scene
        yield return new WaitForSeconds(transitionDelay);

        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }
}
