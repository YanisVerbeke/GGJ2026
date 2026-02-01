using System;
using System.Collections.Generic;
using UnityEngine;

public class MaskSpawner : MonoBehaviour
{
    [SerializeField] int numberOfMasks;
    [SerializeField] GameObject maskPrefab;
    [SerializeField] List<GameObject> masks;
    Vector3 tempPos;
    GameObject target;
    Vector3 targetPos;
    Vector3 parentPos;
    Vector3 moveVector = Vector3.up;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tempPos = RandomPos();
        masks.Add(Instantiate(maskPrefab, tempPos, transform.rotation, transform));
        masks[0].tag = "alien";
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
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("Click registered");
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
                    if (target.transform.localPosition.magnitude >= 1.5f)
                    {
                        if (target.transform.parent.tag == "alien")
                        {
                            SfxManager.Instance.PlayYippee();
                            GameMaster.Instance.EndMiniGame(true);
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
}
