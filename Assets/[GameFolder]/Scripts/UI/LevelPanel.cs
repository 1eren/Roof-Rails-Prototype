using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelPanel : UIPanelBase
{
	[SerializeField] private GameObject inGamePanel, winPanel, losePanel;
	[SerializeField] private List<TextMeshProUGUI> moneyTexts = new List<TextMeshProUGUI>();
	private void OnEnable()
	{
		GameManager.Instance.WinEvent.AddListener(ShowWinPanel);
		GameManager.Instance.FallEvent.AddListener(ShowFailPanel);
		GameManager.Instance.DeathEvent.AddListener(ShowFailPanel);
		GameManager.Instance.UpdatePlayerCoinEvent.AddListener(UpdateMoneyTexts);

		UpdateMoneyTexts();
	}
	private void OnDisable()
	{
		if (LevelManager.Instance == null) return;
		GameManager.Instance.WinEvent.RemoveListener(ShowWinPanel);
		GameManager.Instance.FallEvent.RemoveListener(ShowFailPanel);
		GameManager.Instance.DeathEvent.RemoveListener(ShowFailPanel);
		GameManager.Instance.UpdatePlayerCoinEvent.RemoveListener(UpdateMoneyTexts);
	}
	private void ShowFailPanel()
	{
		Run.After(1, () =>
		{
			inGamePanel.SetActive(false);
			OpenEndPanel(losePanel);
		});
	}
	private void ShowWinPanel()
	{
		Run.After(1, () =>
		{
			inGamePanel.SetActive(false);
			OpenEndPanel(winPanel);
		});
	}
	private void OpenEndPanel(GameObject panel)
	{
		panel.SetActive(true);
		panel.transform.localScale = Vector3.zero;
		panel.transform.DOScale(Vector3.one, 1f);
	}
	public void UpdateMoneyTexts()
	{
		int coin = PlayerPrefs.GetInt(PlayerPrefKeys.COIN);
		foreach (var item in moneyTexts)
		{
			item.SetText(coin.ToString());
		}
	}
}
