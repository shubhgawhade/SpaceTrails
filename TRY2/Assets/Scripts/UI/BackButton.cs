using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] private GameObject input;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject quit;
    [SerializeField] private GameObject rules;
    [SerializeField] private GameObject back;

    [SerializeField] private Text rulesHeading;
    [SerializeField] private Text rulesBody;

    private void Start()
    {
        back.SetActive(true);
        rulesHeading.enabled = true;
        rulesBody.enabled = true;
    }
    void OnEnable()
    {
        StartCoroutine("EasterEgg", 15);
    }

    public void RulesFunction()
    {
        input.SetActive(true);
        start.SetActive(true);
        quit.SetActive(true);
        rules.SetActive(true);

        rulesHeading.enabled = false;
        rulesBody.enabled = false;
        back.SetActive(false);

        rulesBody.text = "Use W,A,D or arrow keys for movement. You CANNOT go backwards or turn without moving ahead. \nTry to make your enemies crash into each other while avoiding them. \nC'mon its a simple game, didnt need these rules... GLHF";
    }

    IEnumerator EasterEgg(float t)
    {
        yield return new WaitForSeconds(t);
        rulesBody.text = "This is my first Easter Egg. Youv'e shown some stupidity by staying on the rules screen for a long time, but it was for the good XD \n\n If you find this, let me know by texting \"FIRSTEASTEREGG\" \n\n -Shubh Ravishankar Gawhade";
    }
}
