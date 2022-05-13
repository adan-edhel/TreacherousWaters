using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public float timeLeft = 300; // 5 minutes

    private void Update()
    {
        timeLeft -= Time.deltaTime;
    }
}
