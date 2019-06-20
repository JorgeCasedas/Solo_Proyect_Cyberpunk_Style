using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class DepthSortByYPos : MonoBehaviour
{
    SpriteRenderer spRend;
    TilemapRenderer tlRend;
    public GameObject pivot;
    public GameObject parentObject;
    GameObject tarjet;
    public int offset;
    // Start is called before the first frame update
    void Start()
    {
        spRend = GetComponent<SpriteRenderer>();
        tlRend = GetComponent<TilemapRenderer>();
        pivot = transform.Find("pivot").gameObject;
        
    }

    // Update is called once per frame

    void Update()
    {
        if (!parentObject)
            tarjet = pivot;
        else
            tarjet = parentObject.GetComponent<DepthSortByYPos>().pivot;

        if(spRend)
            spRend.sortingOrder = -(int)(tarjet.transform.position.y * 100) + offset;
        else
            tlRend.sortingOrder = -(int)(tarjet.transform.position.y * 100) + offset;
    }
}
