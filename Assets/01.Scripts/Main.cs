using UnityEngine;

public class Main : MonoBehaviour
{
    // 도메인(콘텐츠) : 해결하고자 하는 문제 영역, 지식 자체를 의미한다.
    // 도메인 모델(모델링) : 도메인과 그 규칙을 추상화한 것
    // 화폐는 중요한 도메인이기 때문에 추상화 해줘야 한다.
    private void Start()
    {
        Currency gold = new Currency(ECurrencyType.Gold, 100);
        Currency diamond = new Currency(ECurrencyType.Diamond, 34);

        
    }
}
