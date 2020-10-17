using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotNPCTest : MonoBehaviour

{
    public Transform head;

    public Transform spine;
    
    public Transform target;

    private float angularDelta = 0;

    private Quaternion rotation;

    public float minMaxAngle = 30;

    public float rotSpeed=0.2f;


    public void StartRotation()
    {

       
        float dotProduct = Vector3.Dot(spine.forward, -target.forward);
        print(dotProduct);
        if(dotProduct>0)
        {
            rotation = Quaternion.LookRotation(-target.forward);
        }
        if (dotProduct <= 0)
        {
            rotation = Quaternion.identity;
        }
        //Buscar resolver por intervalos del valor de DotProduct
        //Quaternion.Identity=Rotacion Nula
        //LookRotation(-target.forward)
        //LookRotation(target.forward)

        //

    }

    public void TurnNPC()
    {
        spine.rotation = Quaternion.Lerp(spine.rotation, rotation, Time.deltaTime*rotSpeed);
    }


    /*public void StartRotation()
    {
        Vector3 targetPos = target.position;
        Vector3 npcPos = spine.position;
        angularDelta = Vector3.Angle(npcPos, targetPos);
        angularDelta = Mathf.Clamp(angularDelta, -minMaxAngle, minMaxAngle);
        Vector3 delta = new Vector3(targetPos.x - npcPos.x, 0.0f, targetPos.z - npcPos.z);;
        rotation = Quaternion.LookRotation(delta);
    }

    public void TurnNPC()

    {

        //rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, Mathf.Clamp(rotation.eulerAngles.y, -60, 60), rotation.eulerAngles.z);

        spine.rotation = Quaternion.Slerp(spine.rotation, rotation, angularDelta * Time.deltaTime);   //Quaternion.RotateTowards(spine.rotation, rotation, 5f); 

        
        
        
        
        
        
        //Vector3 direction= Vector3.Normalize(delta);

        //Vector3 currentPos = npcPos;

        //if(currentPos!=delta)
        //{
          //  currentPos = new Vector3(npcPos.x + 50 * direction.x, 0.0f, npcPos.z + 50 * direction.z);
        //}

        //While(Rot!=delta)
         //{
        //Rot= new Vector3(npcPos.x+Vector1, 0.0f,npcPos.z+Vector1)
        //}
        
        //Vector1=(targetPos.x - npcPos.x)
        

        //This won't work with animated bones for some reason: Quaternion.Lerp(spine.rotation, rotation, 0.2f);

    }*/

    public void TurnNPCHead()

    {
        
        //head.LookAt(target);

    }

}
