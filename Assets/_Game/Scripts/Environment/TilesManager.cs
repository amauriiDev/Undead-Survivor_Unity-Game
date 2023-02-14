using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    public static TilesManager Instance;

    //* variavel atribuida no inspector
    [SerializeField]private MoveTile[] moveTiles;

    //constantes
    private const int maxDistance = 15;

    // variavel da classe
    //[SerializeField]
    private bool isMoving;


    private void Start()
    {
        Instance = this;
        isMoving= false;
    }
    
    // acho que nao ocorre mais bug, um método normal já da conta
    public IEnumerator TranslateTiles(Vector3 newCenter)
    {
        if (!isMoving)
        {
            isMoving = true;
            foreach (var tile in moveTiles)
            {
                tile.transform.position+= newCenter;
            }
        } 
        yield return new WaitForSeconds(0.09f);
        isMoving = false;
    }
}
