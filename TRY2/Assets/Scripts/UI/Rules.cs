using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rules : MonoBehaviour
{
    public GameObject input;
    public GameObject start;
    public GameObject quit;
    public GameObject rules;
    public GameObject back;

    public Text rulesHeading;
    public Text rulesBody;

    private void Start()
    {
        back.SetActive(false);
        rulesHeading.enabled = false;
        rulesBody.enabled = false;
    }

    public void RulesFunction()
    {
        input.SetActive(false);
        start.SetActive(false);
        quit.SetActive(false);
        rules.SetActive(false);

        rulesHeading.enabled = true;
        rulesBody.enabled = true;
        back.SetActive(true);
    }
}
