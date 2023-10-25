using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructImpact : MonoBehaviour
{
    public GameObject[] gameObjectImpact;
    GameObject childObj;
    public void DeathImpact()
    {
        StartImpact();
        StartCoroutine(BreakeImpact());
    }

    public void StartImpact()
    {
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            childObj = Instantiate(gameObjectImpact[rand], gameObject.transform.parent.position, Quaternion.identity);	
            childObj.transform.parent = gameObject.transform;
        }
        else if (rand == 1)
        {
            childObj = Instantiate(gameObjectImpact[rand], gameObject.transform.parent.position, Quaternion.identity);	
            childObj.transform.parent = gameObject.transform;
        }
        else if (rand == 2)
        {
            childObj = Instantiate(gameObjectImpact[rand], gameObject.transform.parent.position, Quaternion.identity);	
            childObj.transform.parent = gameObject.transform;
        }
        
    }

    IEnumerator BreakeImpact()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(childObj);
    }
}
