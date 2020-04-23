using System;

public class EventBroker 
{
    public static event Action<SoulsContainer> VisitShoreTrigger;
    public static void Call_VisitShoreTrigger(SoulsContainer container)
    {
        VisitShoreTrigger?.Invoke(container);
    }

    public static event Action VisitUpgradeTrigger;
    public static void Call_VisitUpgradeTrigger()
    {
        VisitUpgradeTrigger?.Invoke();
    }

    public static event Action VisitQuestTrigger;
    public static void Call_VisitQuestTrigger()
    {
        VisitQuestTrigger?.Invoke();
    }
}
