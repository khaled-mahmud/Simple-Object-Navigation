using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjects : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hitinfo;
    private float rotateSpeed = 10f;
    

    void Update()
    {
        ray = Player.instance.ray;
        hitinfo = Player.instance.hitinfo;


        if (Physics.Raycast(ray, out hitinfo, 100f) && !Player.instance.isRotable)
        {

            if(Input.GetMouseButtonDown(0) && hitinfo.transform.gameObject == this.gameObject)
            {
                SelectDeselectThisObject();
            }
            

            if(Input.GetMouseButton(0) && Player.instance.isTransformable)
            {
                TranslateThisObject();
            }

        }

        if(Player.instance.isRotable && Input.GetMouseButton(0))
        {
            RotateThisObject();
        }

        

    }

    private void SelectDeselectThisObject()
    {
        if(Player.instance.activeGameObject != null && Player.instance.activeGameObject != this.gameObject)
        {
            Player.instance.activeGameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
            Player.instance.isTransformable = false;
        }
                
                
        if(!Player.instance.isTransformable)
        {
            transform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            Player.instance.isTransformable = true;
            Player.instance.activeGameObject = this.gameObject;
        }
    }


    private void TranslateThisObject()
    {
        if(hitinfo.transform.gameObject == this.gameObject)
        {    
            Player.instance.activeGameObject.transform.parent.position = new Vector3(hitinfo.point.x, Player.instance.activeGameObject.transform.parent.position.y, hitinfo.point.z);
        }  
    }


    private void RotateThisObject()
    {
        float XaxisRotation = Input.GetAxis("Mouse X") * rotateSpeed;
        Player.instance.activeGameObject.transform.parent.Rotate(Vector3.down, XaxisRotation);
    }
    
}
