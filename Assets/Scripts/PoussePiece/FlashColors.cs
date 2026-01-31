using UnityEngine;

public class FlashColors : MonoBehaviour
{
    private Light _light;
    [SerializeField] private Color _color1;
    [SerializeField] private Color _color2;
    private float _flashTimer = 0f;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    private void Update()
    {
        if (_flashTimer <= 0f)
        {
            if (_light.color == _color1)
            {
                _light.color = _color2;
            }
            else
            {
                _light.color = _color1;
            }
            _flashTimer = 0.6f;
        }

        _flashTimer -= Time.deltaTime;
    }
}
