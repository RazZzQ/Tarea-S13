 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnImperio : MonoBehaviour
{
    public GameObject ImperioPrefab;
    public float tiempo;
    public GameManagerControl gm;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tiempo = tiempo + Time.deltaTime;
        if (tiempo >= 5)
        {
            CrearNaveImperial();
            tiempo = 0;
        }
    }

    void CrearNaveImperial()
    {
        int x = Random.Range(-8, 8);
        int y = Random.Range(2, 4);
        Vector2 PosicionRandom = new Vector2(x, y);
        GameObject tmp = Instantiate(ImperioPrefab, PosicionRandom, transform.rotation);
        tmp.GetComponent<Destruccion>().SetGameManager(gm);
    }
}

