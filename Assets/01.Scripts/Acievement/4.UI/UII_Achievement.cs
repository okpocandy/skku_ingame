using System.Collections.Generic;
using UnityEngine;

public class UII_Achievement : MonoBehaviour
{
    [SerializeField]
    private UI_AchievementSlot _slotPrefab;
    [SerializeField]
    private Transform _slotParent;
    
    [SerializeField]
    private List<UI_AchievementSlot> _slotList = new List<UI_AchievementSlot>();

    private Vector3 _slotStartPosition = new Vector3(441.5f, -68.42857f, 0f);
    private float _slotHeight = -136.85713f;

    private void Start()
    {
        Refresh();
        AchievementManager.Instance.OnDataChanged += Refresh;
    }

    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.AchievementList;
        
        // 필요한 만큼 슬롯 생성
        while (_slotList.Count < achievements.Count)
        {
            UI_AchievementSlot slot = Instantiate(_slotPrefab, _slotParent);
            _slotList.Add(slot);
        }

        // 모든 슬롯 업데이트
        for (int i = 0; i < _slotList.Count; i++)
        {
            UI_AchievementSlot slot = _slotList[i];
            
            if (i < achievements.Count)
            {
                // 활성화하고 위치 및 데이터 업데이트
                slot.gameObject.SetActive(true);
                slot.transform.localPosition = _slotStartPosition + new Vector3(0f, _slotHeight * i, 0f);
                slot.Refresh(achievements[i]);
            }
            else
            {
                // 사용하지 않는 슬롯은 비활성화
                slot.gameObject.SetActive(false);
            }
        }

        // Content 크기 조정
        _slotParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, Mathf.Abs(_slotHeight) * achievements.Count);
    }
}
