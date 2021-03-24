using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    public GameObject ship;

    public float speed;

    public ConstraintSource shipSource;

    public AimConstraint aimC;

    void Awake()
    {
        ship = GameObject.Find("Ship");

        aimC = GetComponent<AimConstraint>();

        shipSource.sourceTransform = ship.transform;
        shipSource.weight = 1;
        aimC.AddSource(shipSource);

        speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, ship.transform.position, step);
    }
}
