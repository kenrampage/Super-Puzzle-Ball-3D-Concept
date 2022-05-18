using UnityEngine;

public class FollowTargetWithOffset : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool followX;
    [SerializeField] private bool followY;
    [SerializeField] private bool followZ;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(followX ? target.transform.position.x + offset.x : offset.x
        ,followY ? target.transform.position.y + offset.y : offset.y
        ,followZ ? target.transform.position.z +offset.z : offset.z);
    }
}
