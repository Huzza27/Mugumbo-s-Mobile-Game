using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Analytics;

namespace UnityEditor.VSAttribution
{
    public class VSAttributionCode : Tripolygon.UModeler.Hub.VSAttribution.IVSAttribution
    {
        const int k_VersionId = 4;
        const int k_MaxEventsPerHour = 10;
        const int k_MaxNumberOfElements = 1000;

        const string k_VendorKey = "unity.vsp-attribution";
        const string k_EventName = "vspAttribution";
        public VSAttributionCode()
        {
            Tripolygon.UModeler.Hub.VSAttribution.Attribution = this;
        }

        private VSAttributionCode singleton = new VSAttributionCode();

#if UNITY_2023_2_OR_NEWER
        [AnalyticInfo(eventName: k_EventName, vendorKey: k_VendorKey, maxEventsPerHour: k_MaxEventsPerHour, maxNumberOfElements: k_MaxNumberOfElements, version: k_VersionId)]
        private class VSAttributionAnalytic : IAnalytic
        {
            private VSAttributionData _data;

            public VSAttributionAnalytic(VSAttributionData data)
            {
                _data = data;
            }

            public bool TryGatherData(out IAnalytic.IData data, out Exception error)
            {
                error = null;
                data = _data;
                return data != null;
            }
        }
#else
		static bool RegisterEvent()
		{
			AnalyticsResult result = EditorAnalytics.RegisterEventWithLimit(k_EventName, k_MaxEventsPerHour,
				k_MaxNumberOfElements, k_VendorKey, k_VersionId);

			var isResultOk = result == AnalyticsResult.Ok;
			return isResultOk;
		}
#endif

        [Serializable]
        struct VSAttributionData
#if UNITY_2023_2_OR_NEWER
            : IAnalytic.IData
#endif
        {
            public string actionName;
            public string partnerName;
            public string customerUid;
            public string extra;
        }

        /// <summary>
        /// Registers and attempts to send a Verified Solutions Attribution event.
        /// </summary>
        /// <param name="actionName">Name of the action, identifying a place this event was called from.</param>
        /// <param name="partnerName">Identifiable Verified Solutions Partner's name.</param>
        /// <param name="customerUid">Unique identifier of the customer using Partner's Verified Solution.</param>
        public bool SendAttributionEvent(string actionName, string partnerName, string customerUid)
        {
            try
            {
                // Are Editor Analytics enabled ? (Preferences)
                if (!EditorAnalytics.enabled)
                    return false;

#if !UNITY_2023_2_OR_NEWER
				if (!RegisterEvent())
					return false;
#endif
                // Create an expected data object
                var eventData = new VSAttributionData
                {
                    actionName = actionName,
                    partnerName = partnerName,
                    customerUid = customerUid,
                    extra = "{}"
                };
#if UNITY_2023_2_OR_NEWER
                VSAttributionAnalytic analytic = new VSAttributionAnalytic(eventData);
                return EditorAnalytics.SendAnalytic(analytic) == AnalyticsResult.Ok;
#else
				return EditorAnalytics.SendEventWithLimit(k_EventName, eventData, k_VersionId) == AnalyticsResult.Ok;
#endif
            }
            catch
            {
                // Fail silently
                return false;
            }
        }
    }
}
