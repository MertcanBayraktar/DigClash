using UnityEngine;

public class FriendlyTarget : MonoBehaviour
{
    bool collidedWithPlayer = false;
    Transform target;
    void Start()
    {
        
    }
    void Update()
    {
        if(collidedWithPlayer)
            this.transform.position = Vector3.MoveTowards(this.transform.position, GetBehind(target.transform, 2), 3 * Time.deltaTime);

    }
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
            this.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            target = other.transform;
            collidedWithPlayer = true;
            if(!Player.instance.friendlyList.Contains(this.gameObject))
                Player.instance.friendlyList.Add(this.gameObject);
        }
	}
    Vector3 GetBehind(Transform target,float distanceBehind)
	{
        return target.position - (target.forward * distanceBehind);
	}
}
