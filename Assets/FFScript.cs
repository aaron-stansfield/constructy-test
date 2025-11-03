using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FFScript : MonoBehaviour
{
    [SerializeField]
    public GameObject[] IKRoots;

    private void Awake()
    {
        print("Running FF Scene");

    }
}
