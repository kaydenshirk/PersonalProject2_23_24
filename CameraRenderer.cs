using UnityEngine;

public class OffScreenActivation : MonoBehaviour
{
    private Renderer _renderer;
    private bool _isActive = false;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        gameObject.SetActive(false); // Start deactivated
    }

    void Update()
    {
        if (!_isActive && _renderer.isVisible)
        {
            _isActive = true;
            gameObject.SetActive(true); // Activate the object when it's on-screen
        }
        else if (_isActive && !_renderer.isVisible)
        {
            _isActive = false;
            gameObject.SetActive(false); // Deactivate the object when it's off-screen
        }
    }
}
