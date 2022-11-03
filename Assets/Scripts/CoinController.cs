using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null && collision.gameObject == Player)
        {
            SceneManager._SCENE_MANAGER.coinCollected();

            Destroy(this);
        }
    }

}
