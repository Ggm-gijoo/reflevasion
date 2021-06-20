using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerMove playerMove = null;

    private void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
    }

    public void Refl()
    {
        playerMove.Ref();
    }
}
