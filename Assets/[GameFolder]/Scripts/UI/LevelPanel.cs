using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelPanel : UIPanelBase
{
	[SerializeField] private GameObject inGamePanel, winPanel, losePanel;
	[SerializeField] private List<TextMeshProUGUI> moneyTexts = new List<TextMeshProUGUI>();
	[SerializeField] private TextMeshProUGUI poleCounterText;

	int poleCounter = 0;
	private void OnEnable()
	{
		GameManager.Instance.GameWinEvent.AddListener(ShowWinPanel);
		GameManager.Instance.PlayerFalled.AddListener(ShowFailPanel);
		GameManager.Instance.PlayerDied.AddListener(ShowFailPanel);

		GameManager.Instance.PlayerPrefsUptated.AddListener(UpdateMoneyTexts);
		EventManager.StickCollected.AddListener(UpdatePoleText);

		UpdateMoneyTexts();
	}
	private void OnDisable()
	{
		if (LevelManager.Instance == null) return;
		GameManager.Instance.GameWinEvent.RemoveListener(ShowWinPanel);
		GameManager.Instance.PlayerFalled.RemoveListener(ShowFailPanel);
		GameManager.Instance.PlayerDied.RemoveListener(ShowFailPanel);

		GameManager.Instance.PlayerPrefsUptated.RemoveListener(UpdateMoneyTexts);
		EventManager.StickCollected.RemoveListener(UpdatePoleText);

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
	private void UpdatePoleText(CollectableStick stick)
	{
		poleCounter++;
		poleCounterText.SetText(poleCounter.ToString());
	}
}
