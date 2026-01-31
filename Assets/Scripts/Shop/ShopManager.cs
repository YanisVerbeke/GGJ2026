using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    float timerBeforeOrder;
    float timer = 0;
    [SerializeField] float timerBeforeLose;
    bool isOrdered;
    TextMeshProUGUI orderText;
    GameObject target;
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
            }
            /*if (Input.GetMouseButtonDown(0))
            {
                target = RaycastDetection();
                if (target != null)
                {
                    if (target.name == orderText.text)
                    {
                        //win
                        Debug.Log("Win !");
                    } else
                    {
                        //lose
                        Debug.Log("Lose !");
                    }
                }
            }*/

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
                } else
                {
                    Debug.Log("Lose !");
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

    GameObject RaycastDetection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 20f))
        {
            if(raycastHit.transform != null && raycastHit.transform.tag == "mask")
            {
                return raycastHit.transform.gameObject;
            }
        }
        return null;
    }
}
