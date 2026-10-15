using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float bodyheight;
    public float moveSpeed;
    public float roundSpeed;
    private Vector3 pos;
    private Quaternion rotate;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        pos = target.position + target.forward * offset.z;
        pos += target.right * offset.x;
        pos+=target.up*offset.y;
        transform.position=Vector3.Lerp(transform.position, pos, Time.deltaTime*moveSpeed);
        rotate=Quaternion.LookRotation(target.position + target.up * bodyheight - transform.position);
        transform.rotation=Quaternion.Lerp(transform.rotation,rotate,Time.deltaTime*roundSpeed);
    }

    public void SetTarget(Transform player)
    {
        target = player;
    }
}
