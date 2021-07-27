using System;
using System.Collections.Generic;

namespace WitAI_DiscordBot.Lib.Extensions
{
    public static class ActionExtensions
    {
        /// <summary>
        /// Invoke all actions in the list
        /// </summary>
        /// <param name="actions"></param>
        public static void InvokeAll(this List<Action> actions)
        {
            actions.ForEach(action => action.Invoke());
        }

        /// <summary>
        /// Invoke all actions in the list
        /// </summary>
        /// <param name="actions"></param>
        public static void InvokeAll<T>(this List<Action<T>> actions, T arg)
        {
            actions.ForEach(action => action.Invoke(arg));
        }
    }
}
