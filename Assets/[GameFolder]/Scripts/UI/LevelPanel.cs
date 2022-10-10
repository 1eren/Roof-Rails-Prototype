using System.Collections;
using System.Collections.Generic;
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
		GameManager.Instance.WinEvent.RemoveListener(ShowWinPanel);
		GameManager.Instance.FallEvent.RemoveListener(ShowFailPanel);
	}
	private void ShowFailPanel()
	{
		Run.After(1, () =>
		{
			inGamePanel.SetActive(false);
			losePanel.SetActive(true);
		});
	}
	private void ShowWinPanel()
	{
		Run.After(1, () =>
		{
			inGamePanel.SetActive(false);
			winPanel.SetActive(true);
		});
	}
}
