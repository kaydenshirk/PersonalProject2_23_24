using UnityEngine;

public class TriggerDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>() != null && !other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
