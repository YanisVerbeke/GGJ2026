using System.Threading;
using UnityEngine;

public class BoingBoing : MonoBehaviour
{
    [SerializeField] float smolValue;
    [SerializeField] float bigValue;
    [SerializeField] float lerpTimer;
    float _timer;
    bool _isReverse = false;
    Vector3 baseScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isReverse)
        {
            _timer -= Time.deltaTime;
        } else
        {
            _timer += Time.deltaTime;
        }
        if (_timer >= lerpTimer)
        {
            _isReverse = true;
        }
        if (_timer <= 0)
        {
            _isReverse = false;
        }
        transform.localScale = baseScale * Mathf.Lerp(smolValue, bigValue, _timer/lerpTimer);
    }
}
