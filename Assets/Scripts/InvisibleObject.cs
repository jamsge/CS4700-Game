using UnityEngine;
using UnityEngine.SceneManagement;

public class InvisibleObject : MonoBehaviour
{
    public Animator transitionAnimator;
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Play transition animation if you have one
            if (transitionAnimator != null)
            {
                transitionAnimator.SetTrigger("Start");
            }

            // Load the desired scene after a short delay
            Invoke("LoadSceneAfterDelay", 1f);
        }
    }

    private void LoadSceneAfterDelay()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
