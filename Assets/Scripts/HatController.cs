using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{
    private float speed = 15f;

    private void Update()
    {
        Movement();
        
    }
    private void Movement()
    {
        if (InputManager._INPUT_MANAGER.GetTimeHatButton() < 1f)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            
        }else  if (InputManager._INPUT_MANAGER.GetTimeHatButton() > 10f)
        {
            Destroy(this.gameObject);
        }
    }
}
