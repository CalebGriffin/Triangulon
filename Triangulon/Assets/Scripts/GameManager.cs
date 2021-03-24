using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject ship;
    public GameObject bullet;

    public Vector3 pos;
    public Quaternion rot;

    public bool waiting = false;

    public void Start()
    {

    }

    public void Update()
    {
        pos = new Vector3(ship.transform.position.x, (ship.transform.position.y + 1f), ship.transform.position.z);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ship.transform.Rotate(0, 0, 3);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            ship.transform.Rotate(0, 0, -3);
        }

        if (Input.GetKey("space") && waiting == false)
        {
            rot.Set(0, 0, ship.transform.rotation.z, 1);
            Instantiate(bullet, ship.transform.position + (transform.up * 1), ship.transform.rotation * Quaternion.Euler (0f, 0f, 0f));
            StartCoroutine("wait");
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            ship.transform.position += transform.up * 0.06f;
        }
    }

    IEnumerator wait()
    {
        waiting = true;
        yield return new WaitForSeconds(0.5f);
        waiting = false;
    }

    //Change the Quaternion values depending on the values of the ship
    private static Quaternion Change(float x, float y, float z)
    {
        Quaternion rot = new Quaternion();
        rot.Set(x, y, z, 1);
        //Return the new Quaternion
        return rot;
    }
}
