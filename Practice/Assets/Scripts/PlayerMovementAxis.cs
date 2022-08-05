using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAxis : MonoBehaviour
{
    public float movSpeed = 2f;
    Vector3 movDir = new Vector3(0f, 0f, 0f);
    float ejeHorizontal, ejeVertical;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveAxis();
    }

    void MoveAxis()
    {
        ejeHorizontal = Input.GetAxisRaw("Horizontal");
        ejeVertical = Input.GetAxisRaw("Vertical");
        movDir = new Vector3(ejeHorizontal, 0, ejeVertical);
        transform.Translate(movDir * movSpeed * Time.deltaTime);
    }
}
