using UnityEngine;
using System.Collections;
using TMPro;
public class KhachHangP : MonoBehaviour
{
    public Transform waitingPoint;
    public Transform outPoint;
    public float speed = 1f;
    public int brustTime;
    public bool isWaiting = true;
    public TextMeshProUGUI TextName;

    private void Start()
    {
        StartCoroutine(WaitForOutPoint());
    }

    private void Update()
    {
        if (isWaiting)
        {
            MoveToPoint(waitingPoint);
        }
        else
        {
            MoveToPoint(outPoint);
        }
    }

    private IEnumerator WaitForOutPoint()
    {
        yield return new WaitForSeconds(brustTime);
        isWaiting = false;
    }

    private void MoveToPoint(Transform point)
    {
        transform.position = Vector3.MoveTowards(transform.position, point.transform.position, speed * Time.deltaTime);
    }

}

