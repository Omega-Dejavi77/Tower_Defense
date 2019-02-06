using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour {

    public Text roundsText;

    void OnEnable()
    {
        //roundsText.text = PlayerStats.ROUNDS.ToString();
        //Time.timeScale = 0;
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText ()
    {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(0.5f);

        while (round < PlayerStats.ROUNDS)
        {
            round++;
            roundsText.text = round.ToString();
            yield return new  WaitForSeconds(0.05f);
        }
    }
}
