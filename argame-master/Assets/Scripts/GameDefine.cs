using UnityEngine;
using System.Collections;

public class GameDefine  {

    public const string startScene = "Start";
    public const string CreateCharacterScene = "CreateCharacter";
    public const string MainScene = "Scene_Forest";

    public const string playerName = "playerName";
    public const string playerRole = "playerRole";

    public const string animIdle = "Idle";
    public const string animRun = "Run";
    public const string animDeath = "Death";
    public const string animHit = "Hit";
    public const string animAttack1 = "Attack1";
    public const string animAttack2 = "Attack2";

    //conveint to change when channgeing path of  file to load  
    public const string warriorPath = "Prefabs/Character/Warrior";
    public const string archerPath = "Prefabs/Character/Archer";
    public const string enemyPath = "Prefabs/Character/Enemy1";
    //public const string enemyPath = "Prefabs/Character/Enemy2";

    public const string warriorSkill1 = "Prefabs/FX/WarriorSkill1";
    public const string warriorSkill2 = "Prefabs/FX/WarriorSkill2";
    public const string archerSkill1= "Prefabs/FX/ArcherSkill1";
    public const string archerSkill2 = "Prefabs/FX/ArcherSkill2";
    public const string archerSkillHit = "Prefabs/FX/ArrowHit";

    public const string UI_BloodSlider= "Prefabs/UI/BloodSlider";
    public const string UI_HarmNumber= "Prefabs/UI/HarmNumberUI";
    public const string UI_GameOverPanel = "Prefabs/UI/GameOverPanel";

    public const int skillFXForeverLife = -100;

}

public enum CharacterType //不属于gamedefine class 注意括号位置
{
    warrior,
    archer,
}
