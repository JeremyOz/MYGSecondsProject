using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIControllerLetterTried : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI letterTriedActive;

    private Dictionary<int, string> dictCharacterByPosition = new Dictionary<int, string>() {
        { 0, "A"}, { 2, "B"}, { 4, "C"}, { 6, "D"}, { 8, "E"}, 
        {10, "F"}, {12, "G"}, {14, "H"}, {16, "I"}, {18, "J"}, 
        {20, "K"}, {22, "L"}, {24, "M"}, {26, "N"}, {28, "O"},
        {30, "P"}, {32, "Q"}, {34, "R"}, {37, "S"}, {39, "T"}, 
        {41, "U"}, {43, "V"}, {45, "W"}, {47, "X"}, {49, "Y"}, 
        {51, "Z"}, 
    };

    private void Start()
    {
        letterTriedActive.text = letterTriedActive.text.Replace("\\n", "\n");
    }

    /* @desc Affiche la lettre essayé par et pour le joueur.*/
    public void AddLetterInLetterTried(string letterTried)
    {
        int position = dictCharacterByPosition.FirstOrDefault(x => x.Value.Contains(letterTried)).Key;

        letterTriedActive.text = letterTriedActive.text.Remove(position, 1);
        letterTriedActive.text = letterTriedActive.text.Insert(position, letterTried.ToUpper());
        
    }

    public bool CheckIsLetterTried(string letter)
    {
        if(letterTriedActive.text.Contains(letter))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Reset()
    {
        letterTriedActive.text = "                 \n                 \n                ";
    }
}
