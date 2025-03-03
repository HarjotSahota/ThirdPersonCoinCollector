using UnityEngine;
using TMPro;

public class CoinCollection : MonoBehaviour
{
    private int Coin = 0;
    public TextMeshProUGUI  coinText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if(other.transform.tag == "Coin"){
            Coin++;
            coinText.text = "coins: "+ Coin.ToString();
            Destroy(other.gameObject);
        } 
    }
}