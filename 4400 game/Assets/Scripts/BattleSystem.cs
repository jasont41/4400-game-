using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject enemyPrefab = PlayerMovement.Instance.enemyPrefab;

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

    public string NewLevel = "SampleScene";

    public enemyHealthBar enemyHealthBar; 
    //public Slider enemyHealthBar;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
      //  enemyPrefab = PlayerMovement.Instance.enemyPrefab; 
        enemyHealthBar.setMaxHealth(enemyUnit.currentHP); 
    }

    private void Update()
    {
        set_equal_to_player_instance();
        enemyHealthBar.SetHealth(enemyUnit.currentHP); 
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
            int player_current_damage_after_randomization = val_variance(PlayerMovement.Instance.attack_damage);
            bool isDeAD = enemyUnit.TakeDamage(player_current_damage_after_randomization); //grabbing this from the player singleton and that function so it can be changed once a level and experience system is implemented 
            dialogueText.text = "You dealt " + player_current_damage_after_randomization + " damage";
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
        int player_current_heal_after_randomization = val_variance(PlayerMovement.Instance.heal_value);
        playerUnit.Heal(player_current_heal_after_randomization);
        PlayerMovement.Instance.addHealth(player_current_heal_after_randomization); // JE added 


        dialogueText.text = "You cast heal on yourself";
        yield return new WaitForSeconds(1f);

        dialogueText.text = "You healed for" + player_current_heal_after_randomization + " health";
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

            int enemeyRandomizedDamage = val_variance(enemyUnit.damage); 
            bool isDead = playerUnit.TakeDamage(enemeyRandomizedDamage);

            dialogueText.text = enemyUnit.unitName + " dealt " + enemeyRandomizedDamage + " damage";
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
            //dialogueText.text = "You won the battle!";
            dialogueText.text = "You won the battle!";
            PlayerMovement.Instance.add_experience(enemyUnit.exp_given);
            dialogueText.text = "You gained " + enemyUnit.exp_given + " experience!";
            PlayerMovement.Instance.checkLevelUP();
            //Need to print a message aboout level up and the new base damage stat and heal stat
            PlayerMovement.Instance.transform.position = PlayerMovement.Instance.player_pos_before_encounter; 
            SceneManager.LoadScene(NewLevel);

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


    public int val_variance(int unit_damage_val)
    {
        int new_val;
        int orig_val = unit_damage_val; 
        int half_orig_val = orig_val / 2;
        int high_val = orig_val + half_orig_val;
        int low_val = orig_val - half_orig_val;

        new_val = UnityEngine.Random.Range(low_val, high_val);

        return new_val; 
    }

}
