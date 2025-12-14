using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{

    [SerializeField]
    private int lives;
    private int currentWave = 0;
    [SerializeField]
    private int coin;
    [SerializeField]
    private TMP_Text coinText;
    public int Lives { get => lives; set => lives = value; }
    public int CurrentWave { get => currentWave; set => currentWave = value; }

    public int Coin
    {
        get => coin; set
        {
            coin = value;
            coinText.text = "$ " + coin.ToString();

        }
    }

    private void Start()
    {
        Coin = coin;
    }
}
