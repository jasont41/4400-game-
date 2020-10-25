using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab; 
    public GameObject enemyPrefab;

    public Transform playerSpawn;
    public Transform enemySpawn;

    Unit playerUnit;
    Unit enemyUnit;
    public int heal_val; //JE added. DONT HARDCODE VALUES
    public Text dialogueText;
    public BattleState state;

    private bool attackSuccess;
    public int hitSuccessRate; //going to be the high number in random chance, a 20 here would give the attack a 95% hit rate
    int min_num; 


    void Start()
    {
        
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        
    }

    private void Update()
    {
        set_equal_to_player_instance();
    }


    IEnumerator SetupBattle()
    { 
        GameObject playerGo = Instantiate(playerPrefab, playerSpawn);
        playerUnit = playerGo.GetComponent<Unit>();
        playerUnit.currentHP = PlayerMovement.Instance.current_health; // JE added. Gets players health before battle 
        GameObject enemyGo = Instantiate(enemyPrefab, enemySpawn);
        enemyUnit = enemyGo.GetComponent<Unit>();

        dialogueText.text = "A " + enemyUnit.unitName + " wants to fight";

        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {

        doesAttackHit();

        if (attackSuccess) {
            int player_current_damage_after_randomization = attack_value_variance(PlayerMovement.Instance.attack_damage);
            bool isDeAD = enemyUnit.TakeDamage(player_current_damage_after_randomization); //grabbing this from the player singleton and that function so it can be changed once a level and experience system is implemented 
            dialogueText.text = "You dealt " + player_current_damage_after_randomization + " demage";
            yield return new WaitForSeconds(1f);
        }

        else if (!attackSuccess)
        {
            dialogueText.text = "Your attack missed!";
            yield return new WaitForSeconds(1f);
        }
        
        if(enemyUnit.currentHP <= 0) //changed from isDead to playerUnit.currentHealth =< 0 because isDead is now out of scope 
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(heal_val);
        PlayerMovement.Instance.addHealth(5); // JE added 


        dialogueText.text = "You cast heal on yourself";
        yield return new WaitForSeconds(1f);

        dialogueText.text = "You healed for" + heal_val + " health";
        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);
        doesAttackHit();
        if (attackSuccess)
        {
            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

            dialogueText.text = enemyUnit.unitName + " dealt " + enemyUnit.damage + " damage";
            yield return new WaitForSeconds(1f);
        }
        else if (!attackSuccess)
        {
            dialogueText.text = enemyUnit.unitName + "'s Attack Missed!";
            yield return new WaitForSeconds(1f);
        }

        if (playerUnit.currentHP <= 0) //changed from isDead to playerUnit.currentHealth =< 0 because isDead is now out of scope 
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action: ";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerHeal());
    }
    public void set_equal_to_player_instance()
    {
        PlayerMovement.Instance.current_health =  playerUnit.currentHP;
    }


    private void doesAttackHit()
    {
        int random_num = UnityEngine.Random.Range(min_num, hitSuccessRate);
        if (random_num == min_num)
        {
            attackSuccess = false;
        }
        else
        {
            attackSuccess = true;
        }
    }


    public int attack_value_variance(int unit_damage_val)
    {
        int new_attack_damage_val;
        int orig_atttack_val = unit_damage_val; 
        int half_of_attack_damage = orig_atttack_val / 2;
        int attack_high = orig_atttack_val + half_of_attack_damage;
        int attack_low = orig_atttack_val - half_of_attack_damage;

        new_attack_damage_val = UnityEngine.Random.Range(attack_low, attack_high);

        return new_attack_damage_val; 
    }

}
