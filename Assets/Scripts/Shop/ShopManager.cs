using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    float timerBeforeOrder;
    float timer = 0;
    [SerializeField] float timerBeforeLose;
    bool isOrdered;
    TextMeshProUGUI orderText;
    string keyPressed;
    int keyOrdered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerBeforeOrder = Random.Range(0.3f, 3f);
        orderText = GameObject.Find("OrderText").GetComponent<TextMeshProUGUI>();
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
            if (timer >= timerBeforeLose)
            {
                GameMaster.Instance.EndMiniGame(false);
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
                    GameMaster.Instance.EndMiniGame(true);
                }
                else
                {
                    GameMaster.Instance.EndMiniGame(false);

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
            }
        }
    }
}
