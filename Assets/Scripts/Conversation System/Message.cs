using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Message
{
    public Character speaker;
    public Character receiver;
    public Choice choice;
    public Topic topic;

    public string parseResultText()
    {
        return parseText(choice.resultText);
    }

    public string parseDescription()
    {
        return parseText(choice.description);
    }

    public string parseName()
    {
        return parseText(choice.name);
    }

    string parseText(string text)
    {
        return text

            // todo: capitalize by doing '. [s.him]' first
            .Replace("[s.him]", parseHim(speaker.gender))
            .Replace("[s.he]", parseHe(speaker.gender))
            .Replace("[s.his]", parseHis(speaker.gender))
            .Replace("[s.name]", speaker.firstName)

            .Replace("[r.him]", parseHim(receiver.gender))
            .Replace("[r.he]", parseHe(receiver.gender))
            .Replace("[r.his]", parseHis(receiver.gender))
            .Replace("[r.name]", receiver.firstName)

            .Replace("[topic]", topic.name)

            ;
    }

    string parseHim(Gender gender)
    {
        switch (gender)
        {
            case(Gender.Male):
                return "him";
            case(Gender.Female):
                return "her";
            case (Gender.Other):
                return "them";
            default:
                return "them";
        }
    }

    string parseHis(Gender gender)
    {
        switch (gender)
        {
            case (Gender.Male):
                return "his";
            case (Gender.Female):
                return "hers";
            case (Gender.Other):
                return "theirs";
            default:
                return "theirs";
        }
    }

    string parseHe(Gender gender)
    {
        switch (gender)
        {
            case (Gender.Male):
                return "he";
            case (Gender.Female):
                return "she";
            case (Gender.Other):
                return "they";
            default:
                return "they";
        }
    }
}
