// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Vuforia")]
	[Tooltip("Enable or Disable the PersistExtendeTracking option for ImageTrackers.")]
	public class VuforiaSetPersistExtendedTracking : FsmStateAction
	{

		[RequiredField]
		public FsmBool persistExtendedTracking;

		
		public override void Reset()
		{
			persistExtendedTracking = true;
		}
		
		public override void OnEnter()
		{

			ImageTracker tracker = TrackerManager.Instance.GetTracker<ImageTracker>();

			if (tracker!=null)
			{
				tracker.PersistExtendedTracking ( persistExtendedTracking.Value );
			}

			Finish();	

		}

		
		
		
	}
}