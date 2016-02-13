using UnityEngine;

public class Character : MonoBehaviour
{
    public delegate void LevelingUp(int levelChange);
    public event LevelingUp LevelChange;

    [Tooltip("The current level of this Experiential")]
    [Range(1, 100)]
    public int level = 1;

    [Range(1, 100)]
    public float baseExperiencePerLevel = 10.0f;

    [Range(1, 5)]
    public float experiencePerLevelGrowth = 1.1f;

    [Tooltip("The team to which this GameObject belongs")]
    public TeamType teamType = TeamType.Neutral;
    
    private float experience;

    void OnValidate()
    {
        experience = GetExperience(level);
    }

    /// <summary>
    /// Adds experience points to this characters current experience, and updates the current level
    /// </summary>
    public void AddExperience(float experience)
    {
        this.experience += experience;
        int currentLevel = GetLevel(this.experience);
        if (currentLevel != level)
        {
            ChangeLevel(currentLevel - level);
            level = currentLevel;
        }
    }

    /// <summary>
    /// Copies this Character's data over with the data from the passed in Character
    /// </summary>
    public void Copy(Character character)
    {
        level = character.level;
        baseExperiencePerLevel = character.baseExperiencePerLevel;
        experiencePerLevelGrowth = character.experiencePerLevelGrowth;
        teamType = character.teamType;
        GetExperience(level);
    }

    /// <summary>
    /// Finds and returns the nearest Character that is not on the same team, or returns null if there is none
    /// </summary>
    public Character FindNearestNotTeammate()
    {
        var characters = FindObjectsOfType<Character>();
        Character nearestCharacter = null;
        float nearestDistance = Mathf.Infinity;
        foreach (var character in characters)
        {
            if (character.teamType == teamType)
            {
                continue;
            }
            if (!nearestCharacter)
            {
                nearestCharacter = character;
                continue;
            }
            var distance = (transform.position - nearestCharacter.transform.position).sqrMagnitude;
            if (distance < nearestDistance)
            {
                nearestCharacter = character;
                nearestDistance = distance;
            }
        }
        return nearestCharacter;
    }

    /// <summary>
    /// Finds and returns the nearest Character that is on the specified team, or returns null if there is none
    /// </summary>
    public Character FindNearestOnTeam(TeamType teamType)
    {
        var characters = FindObjectsOfType<Character>();
        Character nearestCharacter = null;
        float nearestDistance = Mathf.Infinity;
        foreach (var character in characters)
        {
            if (character.teamType != teamType)
            {
                continue;
            }
            if (!nearestCharacter)
            {
                nearestCharacter = character;
                continue;
            }
            var distance = (transform.position - nearestCharacter.transform.position).sqrMagnitude;
            if (distance < nearestDistance)
            {
                nearestCharacter = character;
                nearestDistance = distance;
            }
        }
        return nearestCharacter;
    }

    private void ChangeLevel(int levelChange)
    {
        if (levelChange == 0)
        {
            return;
        }
        if (LevelChange != null)
        {
            LevelChange(levelChange);
        }
    }

    private int GetLevel(float currentExperience)
    {
        int level = 1;
        float experiencePerLevel = baseExperiencePerLevel;
        while (currentExperience / experiencePerLevel > 0)
        {
            currentExperience -= experiencePerLevel;
            experiencePerLevel *= experiencePerLevelGrowth;
            level++;
        }
        return level;
    }

    private float GetExperience(int level)
    {
        float experience = 0;
        float experiencePerLevel = baseExperiencePerLevel;
        while (level > 1)
        {
            experience += experiencePerLevel;
            experiencePerLevel *= experiencePerLevelGrowth;
            level--;
        }
        return experience;
    }
}