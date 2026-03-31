using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    protected void FinishQuest()
    {
        if (!isFinished)
        {
            isFinished = true;
            
            Destroy(this.gameObject);
        }
    }
}
