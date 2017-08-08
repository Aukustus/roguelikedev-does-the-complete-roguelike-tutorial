using RogueTutorial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRogueTutorial
{
    [Serializable]
    class Message
    {
        public string Text;
        public string Color;

        public Message(string text, string color)
        {
            Text = text;
            Color = color;
        }
    }

    [Serializable]
    class MessageLog
    {
        public List<Message> Messages = new List<Message>();

        public MessageLog()
        {

        }

        public static void AddMessage(string message, string color = "white")
        {
            List<Message> wrappedMessages = WordWrap(message, color);

            Rogue.GameWorld.MessageLog.Messages.AddRange(wrappedMessages);

            while (Rogue.GameWorld.MessageLog.Messages.Count() > Constants.MessageLogLength)
            {
                Rogue.GameWorld.MessageLog.Messages.RemoveAt(0);
            }
        }

        private static List<Message> WordWrap(string text, string color)
        {
            int maxLineLength = 60;

            List<Message> list = new List<Message>();

            int currentIndex;

            int lastWrap = 0;

            char[] whitespace = { ' ', '\r', '\n', '\t' };

            do
            {
                currentIndex = lastWrap + maxLineLength > text.Length ? text.Length : (text.LastIndexOfAny(new[] { ' ', ',', '.', '?', '!', ':', ';', '-', '\n', '\r', '\t' }, Math.Min(text.Length - 1, lastWrap + maxLineLength)) + 1);
                if (currentIndex <= lastWrap)
                    currentIndex = Math.Min(lastWrap + maxLineLength, text.Length);
                list.Add(new Message(text.Substring(lastWrap, currentIndex - lastWrap).Trim(whitespace), color));
                lastWrap = currentIndex;
            } while (currentIndex < text.Length);

            return list;
        }
    }
}