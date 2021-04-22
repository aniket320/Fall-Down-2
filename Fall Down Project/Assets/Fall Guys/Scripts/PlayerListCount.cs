using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListCount : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text text;
    Player player;
    public void setup(Player _player)
    {
        player = _player;
        text.text = player.NickName;
        
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
