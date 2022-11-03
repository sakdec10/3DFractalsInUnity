using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystemScript : MonoBehaviour
{   
    private bool dragMoveActive;
    private Vector2 lastMousePos;
    private float dragPanSpeed = 0.5f;
    private void Update()
    {    Vector3 inputDir = Vector3.zero;

        //camera movements WASD
       

        if(Input.GetKey(KeyCode.W)) inputDir.z = +1f;
        if(Input.GetKey(KeyCode.S)) inputDir.z = -1f;
        if(Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        if(Input.GetKey(KeyCode.D)) inputDir.x = +1f;

        // //edge scrolling up and down
        // int edgeScrollSize = 20;
        // // if (Input.mousePosition.x < edgeScrollSize) inputDir.x = -1f;
        // if (Input.mousePosition.y < edgeScrollSize) inputDir.y = -1f;
        // // if (Input.mousePosition.x > Screen.width - edgeScrollSize) inputDir.x = +1f;
        // if (Input.mousePosition.y > Screen.height - edgeScrollSize) inputDir.y = +1f;

        //dragMousemovement
        if(Input.GetMouseButtonDown(1)) {
            dragMoveActive = true;
            lastMousePos = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(1)) dragMoveActive = false;

        if (dragMoveActive){
            Vector2 mouseMoveDelta = (Vector2)Input.mousePosition - lastMousePos;

            
            inputDir.x = mouseMoveDelta.x*dragPanSpeed;
            inputDir.y = mouseMoveDelta.y*dragPanSpeed;


            lastMousePos = Input.mousePosition;
        }

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x + transform.up * inputDir.y;
        float moveSpeed = 50f;
        transform.position  += moveDir *moveSpeed * Time.deltaTime;


        //camera rotation QE
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = +1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;

        float rotateSpeed = 100f;
        transform.eulerAngles += new Vector3(0,rotateDir*rotateSpeed*Time.deltaTime,0);

       




    }
}
