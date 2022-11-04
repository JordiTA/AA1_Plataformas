using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject TextGameObject;

    private bool canEnter = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player && canEnter)
        {
            canEnter = false;

            SceneLoadingManager._SCENE_MANAGER.coinCollected(TextGameObject);

            Destroy(this.gameObject);
        }
    }
    
}
