using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    /* Sbyte variables */
    private static sbyte _previousNum = 0;
    private static sbyte _randomNum;

    /* Skins Array */
    private static sbyte[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8 }; 
 
    /* GameObjects to set from inspector */
    [HideInInspector]public GameObject ParentOBJ;
    [HideInInspector] public Button UnlockButton;

    /* Colors */
    private Color Green = new Color(0.1619794f, 0.5283019f, 0.177457f, 1f);
    private Color Gray = new Color(0.5019608f, 0.5019608f, 0.5019608f, 1f);
   
    /* Unlock button event */
    public void onClickUnlock()
    {
        UnlockButton.interactable = false;
        StartCoroutine(RandomSelector());
    }
    /* Random skin selector via iterator. */
    private IEnumerator RandomSelector()
    {
        for (int i = 0; i < numbers.Length + 5; i++)
        {       
            _randomNum = (sbyte)Random.Range(0, numbers.Length); // Select random number which we use for array index.
            if(_previousNum != numbers[_randomNum])
            {
                ParentOBJ.transform.GetChild(numbers[_randomNum]).GetComponent<Image>().color = Green; // New number frame is green.
                ParentOBJ.transform.GetChild(_previousNum).GetComponent<Image>().color = Gray; // Old number frame is gray.
                _previousNum = numbers[_randomNum]; // Update previous number.
                yield return new WaitForSeconds(.45f); // Wait 0.45 second. Animation for users. 
            }   
        }
        numbers = numbers.Where(val => val != numbers[_randomNum]).ToArray(); // Remove selected skin number from skin array.
        ParentOBJ.transform.GetChild(_previousNum).transform.GetChild(1).gameObject.SetActive(false); // Make inactive this gameObject to see unlocked skin.
        if(numbers.Length > 0)
        {
            UnlockButton.interactable = true;  // If there is a skin, we can click again to get another skin.
        }
        else
        {
            Destroy(UnlockButton.gameObject); // If there is no skin, Destroy button.
        }
        
    }
}
