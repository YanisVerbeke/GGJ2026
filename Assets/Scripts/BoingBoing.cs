using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoingBoing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float smolValue;
    [SerializeField] float bigValue;
    [SerializeField] float lerpTimer;
    [SerializeField] bool _isOnHover = false;
    private bool _hover = false;
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
        if (_isOnHover)
        {
            if (!_hover)
            {
                transform.localScale = baseScale;
                return;
            }
        }
        if (_isReverse)
        {
            _timer -= Time.deltaTime;
        }
        else
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
        transform.localScale = baseScale * Mathf.Lerp(smolValue, bigValue, _timer / lerpTimer);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hover = false;
    }
}
