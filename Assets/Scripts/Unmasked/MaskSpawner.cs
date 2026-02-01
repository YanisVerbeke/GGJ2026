using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class MaskSpawner : MonoBehaviour
{
    [SerializeField] int numberOfMasks;
    [SerializeField] GameObject maskPrefab;
    [SerializeField] List<GameObject> masks;
    [SerializeField] Sprite alienFace;
    [SerializeField] float maxTimer;
    private bool _endAnimLaunched = false;
    Vector3 tempPos;
    GameObject target;
    Vector3 targetPos;
    Vector3 parentPos;
    Vector3 moveVector = Vector3.up;
    Image _timerImage;
    private TextMeshProUGUI _endText;
    float _timer;
    [SerializeField] List<Sprite> randomMasks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _timerImage = GameObject.Find("timerImage").GetComponent<Image>();
        _timer = maxTimer;
        _endText = GameObject.Find("endText").GetComponent<TextMeshProUGUI>();
        _endText.enabled = false;
    }
    
    void Start()
    {
        tempPos = RandomPos();
        masks.Add(Instantiate(maskPrefab, tempPos, transform.rotation, transform));
        masks[0].tag = "alien";
        GameObject.Find("headSprite").GetComponent<SpriteRenderer>().sprite = alienFace;
        for (int i = 0; i < numberOfMasks - 1; i++)
        {
            tempPos = RandomPos();
            foreach (GameObject mask in masks)
            {
                while (
                    Mathf.RoundToInt(tempPos.x) == Mathf.RoundToInt(mask.transform.position.x) ||
                    Mathf.RoundToInt(tempPos.y) == Mathf.RoundToInt(mask.transform.position.y)
                    )
                {
                    tempPos = RandomPos();
                }

            }
            masks.Add(Instantiate(maskPrefab, tempPos, transform.rotation, transform));
            foreach(GameObject mask in GameObject.FindGameObjectsWithTag("mask"))
            {
                mask.GetComponent<SpriteRenderer>().sprite = randomMasks[UnityEngine.Random.Range(0, randomMasks.Count)];
            }
        }
        TransitionCanva.Instance.EndTransition();
    }

    Vector3 RandomPos()
    {
        float xPos;
        float yPos;
        xPos = UnityEngine.Random.Range(-7f, 7f);
        yPos = UnityEngine.Random.Range(-2.5f, 2.5f);
        return new Vector3(xPos, yPos, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
            _timerImage.fillAmount = (_timer / maxTimer);
        } else
        {
            if (!_endAnimLaunched)
            {
                // Loose Mini Game
                StartCoroutine(EndAnim(false));
                _endAnimLaunched = true;
            }
        }
        if (Input.GetMouseButton(0))
        {
            target = RaycastDetection();
            if (target != null)
            {
                if (target.tag == "mask")
                {
                    //Debug.Log("Mask hit");
                    targetPos = new Vector3(
                        Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                        Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -0.1f);
                    parentPos = new Vector3(
                        target.transform.parent.position.x,
                        target.transform.parent.position.y, 0);
                    moveVector = targetPos - parentPos;
                    moveVector = Vector3.ClampMagnitude(moveVector, 2f);
                    target.transform.localPosition = moveVector;
                    if (target.transform.localPosition.magnitude >= 1f)
                    {
                        if (target.transform.parent.tag == "alien")
                        {
                            StartCoroutine(EndAnim(true));
                            _endAnimLaunched = true;
                        }
                        target.SetActive(false);
                    }
                }
            }
        }
    }

    GameObject RaycastDetection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 200f))
        {
            if (raycastHit.transform != null)
            {
                //Debug.Log("Ray hit");
                return raycastHit.transform.gameObject;
            }
        }
        return null;
    }
    private IEnumerator EndAnim(bool won)
    {
        yield return new WaitForSeconds(0.4f);

        if (won)
        {
            _endText.text = "Bravo !";
            SfxManager.Instance.PlayYippee();
        }
        else
        {
            _endText.text = "Dommage...";
            SfxManager.Instance.PlayHonk();
        }
        _endText.enabled = true;

        yield return new WaitForSeconds(2f);

        TransitionCanva.Instance.StartTransition();

        yield return new WaitForSeconds(1.5f);

        GameMaster.Instance.EndMiniGame(won);

        yield return null;
    }
}
