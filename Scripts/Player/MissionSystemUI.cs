using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionSystemUI : MonoBehaviour
{
    private MissionSystem missionSystem;

    [SerializeField] private TextMeshProUGUI missionNameText;
    [SerializeField] private TextMeshProUGUI missionTaskText;

    [SerializeField] private Button declineMissionBtn;

    private Mission currentMission;

    private void Start()
    {
        missionSystem = GetComponent<MissionSystem>();
        declineMissionBtn.onClick.AddListener(missionSystem.DeclineCurrentMission);
        declineMissionBtn.onClick.AddListener(DeclineCurrentMissionUI);
        missionSystem.OnNewMissionPickedUpHandler += OnNewMissionPickedUp;
        missionSystem.OnMissionCompleted += OnMissionCompleted;
    }

    private void OnNewMissionPickedUp(Mission mission)
    {
        currentMission = mission;
        missionNameText.text = mission.GetName();
        missionTaskText.text = mission.GetTask();
        EnableDeclineButton();
    }

    private void OnMissionCompleted()
    {
        missionNameText.text = currentMission.GetName();
        missionTaskText.text = "Completed";
        DisableDeclineButton();
    }

    private void DeclineCurrentMissionUI()
    {
        missionNameText.text = "";
        missionTaskText.text = "";
        DisableDeclineButton();
    }

    private void EnableDeclineButton()
    {
        declineMissionBtn.gameObject.SetActive(true);
    }

    private void DisableDeclineButton()
    {
        declineMissionBtn.gameObject.SetActive(false);
    }
}
