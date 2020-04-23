using System;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject GatherSouls;
    [SerializeField] private GameObject RessurectSouls;
    [SerializeField] private GameObject Upgrade;
    [SerializeField] private GameObject Quest;
    [Space]
    [SerializeField] private GameObject Controls;

    private void OnEnable()
    {
        EventBroker.VisitShoreTrigger += EventBroker_VisitShoreTrigger;
        EventBroker.VisitUpgradeTrigger += EventBroker_VisitUpgradeTrigger;
        EventBroker.VisitQuestTrigger += EventBroker_VisitQuestTrigger;
    }

    private void OnDisable()
    {
        EventBroker.VisitShoreTrigger -= EventBroker_VisitShoreTrigger;
        EventBroker.VisitUpgradeTrigger -= EventBroker_VisitUpgradeTrigger;
        EventBroker.VisitQuestTrigger -= EventBroker_VisitQuestTrigger;
    }

    private void EventBroker_VisitShoreTrigger(SoulsContainer container)
    {
        var type = container.Type;
        if ((type == ContainerType.Living || type == ContainerType.Aid) && GatherSouls.activeSelf == true)
        {
            HideControls();

            GatherSouls.SetActive(false);
            RessurectSouls.SetActive(true);
        }

        if (type == ContainerType.Resurrection && RessurectSouls.activeSelf == true)
        {
            RessurectSouls.SetActive(false);

            Upgrade.SetActive(true);
            Quest.SetActive(true);
        }
    }

    private void EventBroker_VisitUpgradeTrigger()
    {
        HideControls();

        if (Upgrade.activeSelf == true)
        {
            HideControls();
            Upgrade.SetActive(false);

            HideIfTutorialOver();
        }
    }

    private void EventBroker_VisitQuestTrigger()
    {
        HideControls();

        if (Quest.activeSelf == true)
        {
            Quest.SetActive(false);

            HideIfTutorialOver();
        }
    }

    public void StartShow()
    {
        GatherSouls.SetActive(true);

        Controls.SetActive(true);
    }

    private void HideControls()
    {
        if (Controls.activeSelf)
            Controls.SetActive(false);
    }

    private void HideIfTutorialOver()
    {
        if (GatherSouls.activeSelf == false &&             
            RessurectSouls.activeSelf == false &&                
            Upgrade.activeSelf == false &&                
            Quest.activeSelf == false)
        {
            gameObject.SetActive(false);
        }
    }
}
