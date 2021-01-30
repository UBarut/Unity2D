using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [Header("Lists")]
    public List<GameObject> checkPointList;
    public List<GameObject> players;

    [Header("GameObject Position")]
    public Vector2 newPos;

    [Header("GameObject")]
    public GameObject gObject;
    public GameObject newPosName;
    GameObject lastCheckpoint;

    string cPTag;
    string lastCPTag="CheckPoints-1";

    private void Start()
    {
        gObject = gameObject;
        gObject.name = gObject.name.Replace("(Clone)", "");
    }
    private void OnTriggerEnter2D(Collider2D checkpoints)
    {
        CheckPoint(checkpoints);
    }

    //It processes one of the control points in the other maps equal to the control point we are in to the newPos coordinate. And if there is an old checkpoint point, it deactivates it.
    private void CheckPoint(Collider2D checkpoints)
    {
        cPTag = checkpoints.gameObject.tag;
        if (checkpoints.gameObject.name.Contains("CheckPoint"))
        {
            if (cPTag != lastCPTag)
            {
                foreach (GameObject i in GameObject.FindGameObjectsWithTag(lastCPTag))
                {
                    i.SetActive(false);
                }
                lastCPTag = checkpoints.gameObject.tag;
            }
            if (checkpoints.gameObject != lastCheckpoint)
            {
                checkPointList.Clear();
                foreach (GameObject i in GameObject.FindGameObjectsWithTag(checkpoints.gameObject.tag))
                {
                    checkPointList.Add(i);
                }
                for (int i = 0; i < checkPointList.Count; i++)
                {
                    if (checkPointList[i] == checkpoints.gameObject)
                    {
                        checkPointList.Remove(checkpoints.gameObject);
                    }
                }
                int index = Random.Range(0, checkPointList.Count);
                newPos = checkPointList[index].transform.position;
                newPosName = checkPointList[index];

                lastCheckpoint = checkpoints.gameObject;
            }
        }
    }

    // In this function, when the character dies, it performs the exchange of the character with the character in the newPos coordinate.
    public void ChangePlayer(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (newPosName.name.Contains("CheckPoint-1"))
            {
                Instantiate(players[0], newPos, Quaternion.identity);
                Destroy(collision.gameObject);
            }
            else if (newPosName.name.Contains("CheckPoint-2"))
            {
                Instantiate(players[1], newPos, Quaternion.identity);
                Destroy(collision.gameObject);
            }
            else if (newPosName.name.Contains("CheckPoint-3"))
            {
                Instantiate(players[2], newPos, Quaternion.identity);
                Destroy(collision.gameObject);
            }
            else if (newPosName.name.Contains("CheckPoint-4"))
            {
                Instantiate(players[3], newPos, Quaternion.identity);
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.LogError("An unspecified checkpoint");
            }
        }
    }
}
