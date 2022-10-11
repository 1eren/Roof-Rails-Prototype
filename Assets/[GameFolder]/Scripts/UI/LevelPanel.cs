using DG.Tweening;
using UnityEngine;

public class LevelPanel : UIPanelBase
{
	[SerializeField] private GameObject inGamePanel, winPanel, losePanel;

	private void OnEnable()
	{
		GameManager.Instance.WinEvent.AddListener(ShowWinPanel);
		GameManager.Instance.FallEvent.AddListener(ShowFailPanel);
	}
	private void OnDisable()
	{
		if (LevelManager.Instance == null) return;
		GameManager.Instance.WinEvent.RemoveListener(ShowWinPanel);
		GameManager.Instance.FallEvent.RemoveListener(ShowFailPanel);
	}
	private void ShowFailPanel()
	{
		Run.After(1, () =>
		{
			inGamePanel.SetActive(false);
			OpenPanel(losePanel);
		});
	}
	private void ShowWinPanel()
	{
		Run.After(1, () =>
		{
			inGamePanel.SetActive(false);
			OpenPanel(winPanel);
		});
	}
	private void OpenPanel(GameObject panel)
	{
		panel.SetActive(true);
		panel.transform.localScale = Vector3.zero;
		panel.transform.DOScale(Vector3.one, 1f);
	}
}
