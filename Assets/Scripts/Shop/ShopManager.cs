using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    float timerBeforeOrder;
    float timer = 0;
    [SerializeField] float timerBeforeLose;
    bool isOrdered;
    TextMeshProUGUI orderText;
    string keyPressed;
    int keyOrdered;
    private Image _timerImage;
    private bool _isEndAnimLaunched = false;
    [SerializeField] private List<Sprite> _maskList;
    private Image _orderImage;
    [SerializeField] private Sprite _sushi;
    [SerializeField] private Sprite _sushiContent;
    [SerializeField] private Sprite _sushiPoContent;
    private SpriteRenderer _sushiSprite;
    [SerializeField] private List<Transform> _masksForSushi;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerBeforeOrder = Random.Range(1.3f, 3f);
        orderText = GameObject.Find("OrderText").GetComponent<TextMeshProUGUI>();
        _timerImage = GameObject.Find("timerImage").GetComponent<Image>();
        _orderImage = GameObject.Find("OrderImage").GetComponent<Image>();
        _orderImage.enabled = false;
        _sushiSprite = GameObject.Find("SushiSprite").GetComponent<SpriteRenderer>();

        SfxManager.Instance.PlayThinking();
    }

    private void OnEnable()
    {
        TransitionCanva.Instance.EndTransition();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOrdered)
        {
            timer += Time.deltaTime;
            _timerImage.fillAmount = 1 - (timer / timerBeforeLose);
            if (timer >= timerBeforeLose)
            {
                if (!_isEndAnimLaunched)
                {
                    StartCoroutine(EndAnim(false));
                    _isEndAnimLaunched = true;
                }
            }

            if (Input.anyKeyDown)
            {
                keyPressed = Input.inputString;
                if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) { keyPressed = "1"; }
                if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) { keyPressed = "2"; }
                if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) { keyPressed = "3"; }
                if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)) { keyPressed = "4"; }
                if (keyPressed == keyOrdered.ToString())
                {
                    if (!_isEndAnimLaunched)
                    {
                        StartCoroutine(EndAnim(true));
                        _isEndAnimLaunched = true;
                    }
                }
                else
                {
                    if (!_isEndAnimLaunched)
                    {
                        StartCoroutine(EndAnim(false));
                        _isEndAnimLaunched = true;
                    }
                }
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= timerBeforeOrder)
            {
                keyOrdered = Random.Range(1, 5);
                orderText.text = "Press " + keyOrdered.ToString() + " !";
                isOrdered = true;
                timer = 0f;
                orderText.enabled = false;
                _orderImage.sprite = _maskList[keyOrdered - 1];
                _orderImage.enabled = true;
            }
        }
    }

    private IEnumerator EndAnim(bool won)
    {
        yield return new WaitForSeconds(0.2f);

        GameObject.Find("Bubble").SetActive(false);
        orderText.enabled = false;
        _orderImage.enabled = false;

        if (won)
        {
            // Sushi content
            _sushiSprite.sprite = _sushiContent;
            _masksForSushi[keyOrdered - 1].gameObject.SetActive(true);
            SfxManager.Instance.PlayYippee();
        }
        else
        {
            // Sushi po content
            SfxManager.Instance.PlayAngry();
            _sushiSprite.sprite = _sushiPoContent;
            SfxManager.Instance.PlayHonk();
        }


        yield return new WaitForSeconds(2f);

        TransitionCanva.Instance.StartTransition();

        yield return new WaitForSeconds(1.5f);

        GameMaster.Instance.EndMiniGame(won);

        yield return null;
    }
}
