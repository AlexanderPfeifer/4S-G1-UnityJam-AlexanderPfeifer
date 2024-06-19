using System.Collections;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;

public class NumberManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leftNumberText;
    [SerializeField] private TextMeshProUGUI rightNumberText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI deniedText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI rulesText;
    [SerializeField] private GameObject addingResults;
    [SerializeField] private Animator endlessBlueAnimation;
    [SerializeField] private GameObject winResult;
    
    private int randomNumberRight;
    private int randomNumberLeft;
    private int resultNumber;
    private bool canGenerateNewNumber = true;

    private void Start()
    {
        GenerateRandomNumber();
    }

    public void SetActiveRules(bool setActive)
    {
        rulesText.gameObject.SetActive(setActive);
    }

    private void GenerateRandomNumber()
    {
        randomNumberLeft = Random.Range(0, 10);
        randomNumberRight = Random.Range(0, 10);
        leftNumberText.text = randomNumberLeft.ToString();
        rightNumberText.text = randomNumberRight.ToString();
    }
    
    public void EndlessBlueCheck(string calculationSymbol)
    {
        inputField.text = "";
        
        switch (calculationSymbol)
        {
            case "":
                canGenerateNewNumber = false;
                break;
            case "+":
                endlessBlueAnimation.SetTrigger("endlessBlue");
                addingResults.SetActive(true);
                resultText.text = "Current result:" + "" + AddNumbers();
                break;
            case "-":
                endlessBlueAnimation.SetTrigger("endlessBlue");
                addingResults.SetActive(true);
                resultText.text =  "Current result:" + "" + SubtractNumber();
                break;
            case "*":
                endlessBlueAnimation.SetTrigger("endlessBlue");
                addingResults.SetActive(true);
                resultText.text = "Current result:" + "" +  MultiplyNumbers();
                break;
            case "/":
                endlessBlueAnimation.SetTrigger("endlessBlue");
                addingResults.SetActive(true);
                resultText.text = "Current result:" + "" + DivideNumbers();
                break;
            default:
                if (!deniedText.gameObject.activeSelf)
                {
                    StartCoroutine(CalculationDeniedCoroutine());
                }
                canGenerateNewNumber = false;
                break;
        }

        if (resultText.text == "42")
        {
            Win();
        }
        
        if(canGenerateNewNumber)
        {
            GenerateRandomNumber();
        }

        canGenerateNewNumber = true;
    }
    
    
    
    private int AddNumbers()
    {
        return resultNumber += randomNumberLeft + randomNumberRight;
    }
    
    private int SubtractNumber()
    {
        return resultNumber += randomNumberLeft - randomNumberRight;
    }
    
    private int MultiplyNumbers()
    {
        return resultNumber += randomNumberLeft * randomNumberRight;
    }
    
    private int DivideNumbers()
    {
        if (randomNumberLeft % randomNumberRight == 0)
        {
            return resultNumber += randomNumberLeft / randomNumberRight;
        }
        else
        {
            return resultNumber += 10;
        }
    }
    
    private IEnumerator CalculationDeniedCoroutine()
    {
        deniedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        deniedText.gameObject.SetActive(false);
    }

    private void Win()
    {
        winResult.SetActive(true);
    }
}
