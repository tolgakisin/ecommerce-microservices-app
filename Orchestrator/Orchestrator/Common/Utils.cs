using System;
using System.Collections.Generic;

namespace Orchestrator.Common
{
    public class Utils
    {
        public static (string EventName, bool IsLastEvent) GetNextEvent(List<string> events, string currentEventName)
        {
            string nextEventName = string.Empty;
            bool isLastEvent = false;
            foreach (var item in events)
            {
                var items = item.Split(",", StringSplitOptions.TrimEntries);
                int index = Array.IndexOf(items, currentEventName);
                if (index != -1 && index < items.Length - 1)
                {
                    nextEventName = items[index + 1];
                    isLastEvent = index + 1 == items.Length - 1;
                    break;
                }
            }

            return (nextEventName, isLastEvent);
        }

        public static (string EventName, bool IsLastEvent) GetPreviousEvent(List<string> events, string currentEventName)
        {
            string previousEventName = string.Empty;
            bool isLastEvent = false;
            foreach (var item in events)
            {
                var items = item.Split(",", StringSplitOptions.TrimEntries);
                int index = Array.IndexOf(items, currentEventName);
                if (index != -1 && index >= 1)
                {
                    previousEventName = items[index - 1];
                    isLastEvent = index == 1;
                    break;
                }
            }

            return (previousEventName, isLastEvent);
        }
    }
}
