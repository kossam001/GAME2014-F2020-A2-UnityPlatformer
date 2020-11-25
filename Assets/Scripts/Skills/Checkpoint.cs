/* Checkpoint.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-24
 * 
 * If player dies with a checkpoint active, player will respawn there, with
 * score equalling to the amount spent to create the checkpoint.
 * 
 * 2020-11-24: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Checkpoint", menuName = "Skills/Checkpoint")]
public class Checkpoint : ISkill
{
    public GameObject repawnPointTemplate;
    public Button skillButton;

    public override void Attach(GameObject _owner)
    {
        owner = _owner;
    }

    public override void DeductCost()
    {
        // Cost is 1/2 of score.
        GameManager.Instance.UpdateHealth(-(int)((float)GameManager.Instance.playerScore * 0.5f), 0);
    }

    public override void Initialize()
    {
        GameManager.Instance.onStatUpdated.AddListener(UpdateLabel);
        SetCostLabel();
    }

    private void UpdateLabel(int score, int heart)
    {
        costLabel.text = ((int)((float)score * 0.5f)).ToString();
    }

    public override void SkillUse()
    {
        GameObject respawnPoint;

        if (GameManager.Instance.availableRespawnPoints.Count > 0)
        {
            respawnPoint = GameManager.Instance.availableRespawnPoints.Dequeue();
            respawnPoint.SetActive(true);
        }
        // Making a maximum of 10 respawns, anymore, then the least recent respawn is erased.
        else if (GameManager.Instance.activeRespawnPoints.Count >= 10)
        {
            // Remove oldest checkpoint
            respawnPoint = GameManager.Instance.activeRespawnPoints[0];
            GameManager.Instance.activeRespawnPoints.RemoveAt(0);
        }
        else
        {
            respawnPoint = Instantiate(repawnPointTemplate);
        }

        respawnPoint.GetComponent<RespawnPoint>().cachedPoints = (int)((float)GameManager.Instance.playerScore * 0.5f);
        respawnPoint.GetComponent<RespawnPoint>().gameObject.transform.position = owner.transform.position;

        GameManager.Instance.activeRespawnPoints.Add(respawnPoint);
        DeductCost();
    }

    public override void SetCostLabel()
    {
        costLabel.text = ((int)((float)GameManager.Instance.playerScore * 0.5f)).ToString();
    }

    public override void AttachButton(Button button)
    {
        skillButton = button;
        skillButton.onClick.AddListener(SkillUse);
    }

    public override void Detach()
    {
        skillButton.onClick.RemoveListener(SkillUse);
        GameManager.Instance.onStatUpdated.RemoveListener(UpdateLabel);
    }
}
