using UnityEngine;

public class Quest
{
    public QuestInfo info;
    public QuestState state;

    private int currentQuestStepIndex;

    public Quest(QuestInfo questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            Object.Instantiate<GameObject>(questStepPrefab, parentTransform);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = info.questPrefabs[currentQuestStepIndex];
        if (CurrentStepExists())
        {
            questStepPrefab = info.questPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("stepIndex is out of range! ID: " + info.id);
        }

        return questStepPrefab;
    }
}

