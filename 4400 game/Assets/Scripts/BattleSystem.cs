using System.Collections;
using System.Collections.Generic;
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
        bool isDeAD = enemyUnit.TakeDamage(playerUnit.damage);
        dialogueText.text = "You dealt " + playerUnit.damage + " demage";

        yield return new WaitForSeconds(2f);
        if(isDeAD)
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

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        dialogueText.text = enemyUnit.unitName + " dealt " + enemyUnit.damage + " damage";
        yield return new WaitForSeconds(1f);

        if (isDead)
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
        dialogueText.text = "Choose an action:";
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






}
