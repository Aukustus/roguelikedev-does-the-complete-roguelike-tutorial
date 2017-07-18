using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRogueTutorial
{
    [Serializable]
    class MessageLog
    {
        public List<string> Messages = new List<string>();

        public MessageLog()
        {

        }

        public void AddMessage(string message)
        {
            Messages.AddRange(WordWrap(message));

            while (Messages.Count() > Constants.MessageLogLength)
            {
                Messages.RemoveAt(0);
            }
        }

        private static List<string> WordWrap(string text)
        {
            int maxLineLength = 60;

            List<string> list = new List<string>();

            int currentIndex;

            int lastWrap = 0;

            char[] whitespace = { ' ', '\r', '\n', '\t' };

            do
            {
                currentIndex = lastWrap + maxLineLength > text.Length ? text.Length : (text.LastIndexOfAny(new[] { ' ', ',', '.', '?', '!', ':', ';', '-', '\n', '\r', '\t' }, Math.Min(text.Length - 1, lastWrap + maxLineLength)) + 1);
                if (currentIndex <= lastWrap)
                    currentIndex = Math.Min(lastWrap + maxLineLength, text.Length);
                list.Add(text.Substring(lastWrap, currentIndex - lastWrap).Trim(whitespace));
                lastWrap = currentIndex;
            } while (currentIndex < text.Length);

            return list;
        }
    }
}
