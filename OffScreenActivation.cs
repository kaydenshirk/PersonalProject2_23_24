using UnityEngine;

public class ActivateOnVisible : MonoBehaviour
{
    private void OnBecameVisible()
    {
        gameObject.SetActive(true);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
