using System;
public class Scripture
{
  private Reference _reference;
  private List<Word> _words;
  public Scripture(Reference reference, string text)
  {
    _reference = reference;
    _words = new List<Word>();
    string[] splittedWords = text.Split(' ');
    foreach (string word in splittedWords)
    {
      Word wordToAdd = new Word(word);
      _words.Add(wordToAdd);
    }
  }
  public void HideRandomWords(int numberToHide)
  {
    // int hiddenCount = 0;
    // for (int i = 0; i < _words.Count; i++)
    // {
    //   if (!_words[i].isHidden())
    //   {
    //     if (hiddenCount < numberToHide)
    //     {
    //       _words[i].Hide();
    //       hiddenCount++;
    //     }
    //   }
    // }
    Random random = new Random();
    List<int> indexesToHide = new List<int>();
    int hiddenCount = 0;
    for (int i = 0; i < _words.Count; i++)
    {
      if (!_words[i].isHidden())
      {
        int randomIndex = random.Next(0, _words.Count);
        while (indexesToHide.Contains(randomIndex) || _words[randomIndex].isHidden())
        {
          randomIndex = random.Next(0, _words.Count);
        }
        indexesToHide.Add(randomIndex);
        hiddenCount++;
        if (hiddenCount == numberToHide)
        {
          break;
        }
      }
    }

    foreach (int index in indexesToHide)
    {
      _words[index].Hide();
    }
  }
  public string GetReference(){
    return _reference.GetDisplayText();
  }
  public string GetDisplayText()
  {
    string joinedStrings = string.Join(" ", _words.Select(word => word.isHidden() ? new string('_', word.GetDisplayText().Length) : word.GetDisplayText()));
    joinedStrings = GetReference() + " -> " + joinedStrings.TrimEnd();
    return joinedStrings;
  }
  public bool IsCompletelyHidden()
  {
    bool completelyHidden = true;
    foreach (Word word in _words)
    {
      if (!word.isHidden())
      {
        completelyHidden = false;
        break;
      }
    }
    return completelyHidden;
  }
}