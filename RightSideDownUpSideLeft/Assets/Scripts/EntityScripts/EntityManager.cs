using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityManager : MonoBehaviour
{
    public static EntityManager entityManager;

    private void Awake()
    {
        entityManager = this;
    }

    //Update and fixedUpdate events

    public event Action entityUpdate;
    public void Update()
    {
        entityUpdate?.Invoke();
    }

    public event Action entityFixedUpdate;
    private void FixedUpdate()
    {
        entityFixedUpdate?.Invoke();
    }

}
