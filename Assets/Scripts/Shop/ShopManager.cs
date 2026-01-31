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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerBeforeOrder = Random.Range(0.3f, 3f);
        orderText = GameObject.Find("OrderText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isOrdered)
        {
            timer += Time.deltaTime;
            if(timer >= timerBeforeLose)
            {
                //Loose
                GameMaster.Instance.EndMiniGame(false);
            }

            if (Input.anyKeyDown)
            {
                keyPressed = Input.inputString;
                switch (keyPressed)
                {
                    case "&" :
                        keyPressed = "1";
                        break;
                    case "é" :
                        keyPressed = "2";
                        break;
                    case "\"" :
                        keyPressed = "3";
                        break;
                    case "'" :
                        keyPressed = "4";
                        break;
                }
                if (keyPressed == orderText.text)
                {
                    Debug.Log("Win");
                    GameMaster.Instance.EndMiniGame(true);
                } else
                {
                    Debug.Log("Lose !");
                    GameMaster.Instance.EndMiniGame(false);
                }
            }
        } else
        {
            timer += Time.deltaTime;
            if(timer >= timerBeforeOrder)
            {
                orderText.text = Random.Range(1,5).ToString();
                isOrdered = true;
                timer = 0f;
            }
        }
    }
}
