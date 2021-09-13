using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public Player player;

    void Awake()
    {
    }

    void Update()
    {
        text.text = player.jumpPowerRounded.ToString();
    }
}