using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI GoldCountText;
    public TextMeshProUGUI DiamondCountText;
    public TextMeshProUGUI BuyHealthText;

    public int BuyHealthCost = 300;

    private void Start()
    {
        Refresh();

        CurrencyManager.Instance.OnDataChanged += Refresh;
    }

    private void Refresh()
    {
        var gold = CurrencyManager.Instance.Get(ECurrencyType.Gold);
        var diamond = CurrencyManager.Instance.Get(ECurrencyType.Diamond);

        GoldCountText.text = $"Gold : {gold.Value}";
        DiamondCountText.text = $"Diamond : {diamond.Value}";

        BuyHealthText.color = gold.HaveEnough(BuyHealthCost) ? Color.black : Color.red;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            BuyHealth();
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void BuyHealth()
    {
        if(CurrencyManager.Instance.TryBuy(ECurrencyType.Gold, BuyHealthCost))
        {
            var player = GameObject.FindFirstObjectByType<PlayerCharacterController>();
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.Heal(100);
        }
        else
        {
            Debug.Log("구매에 실패했습니다.");
        }
    }
}
