using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button m_EndTurnButton;
    [SerializeField] private TextMeshProUGUI m_TurnNumberText;
    [SerializeField] private GameObject m_EnemyTurnVisualGameObject;

    private void Start()
    {
        TurnSystem.Instance.OnTurnChange += UpdateTurnNuberText;

        m_EndTurnButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.nextTurn();
        });

        UpdateTurnNuberText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisual();
    }

    private void UpdateTurnNuberText()
    {
        m_TurnNumberText.text = "TURN: " + TurnSystem.Instance.GetTurnNumber();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisual();
    }

    private void UpdateEnemyTurnVisual()
    {
        m_EnemyTurnVisualGameObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }

    private void UpdateEndTurnButtonVisual()
    {
        m_EndTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }
}
