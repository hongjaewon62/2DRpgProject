using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private Slider experienceBarImage;

    [SerializeField]
    private PlayerController playerController;
    private LevelSystem levelSystem;

    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();
        levelSystem.ExperienceIncreaseAmount();
        SetLevelSystem(levelSystem);
        playerController.SetLevelSystem(levelSystem);
    }

    public void AddExperience(int amount)
    {
        levelSystem.AddExperience(amount);
    }

    public void Button1()
    {
        levelSystem.AddExperience(8000000);
    }
    private void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBarImage.value = experienceNormalized;
    }

    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = "LV " + (levelNumber + 1);
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        // 시작 설정
        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
        // 업데이트
        levelSystem.onExperienceChanged += LevelSystemOnExperienceChanged;
        levelSystem.onLevelChanged += LevelSystemOnLevelChanged;
    }
    private void LevelSystemOnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(levelSystem.GetLevelNumber());
    }
    private void LevelSystemOnExperienceChanged(object sender, System.EventArgs e)
    {
        Debug.Log("바");
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }
}
