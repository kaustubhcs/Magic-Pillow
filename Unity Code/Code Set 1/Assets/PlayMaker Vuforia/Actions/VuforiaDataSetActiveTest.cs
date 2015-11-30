// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Vuforia")]
	[Tooltip("Sends discrete Events based on the dataset active value.")]
	public class VuforiaDataSetActivateTest : FsmStateAction
	{
		
		[ActionSection("Set up")]
		
		[RequiredField]
		[CheckForComponent(typeof(DataSetLoadBehaviour))]
        [Tooltip("The GameObject to check a particular DataSet activate state. A 'DataSetLoadBehaviour' component is required on this GameObject.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString dataSet;
		
		public bool everyFrame;
		
		public bool ignoreInitialState;
		
		[ActionSection("Result")]
		
		[RequiredField]
		public FsmBool isActive;
		
		[Tooltip("Event to send when the dataset becomes active.")]
		public FsmEvent isActiveEvent;

        [Tooltip("Event to send when the dataset becomes inactive.")]
		public FsmEvent isNotActiveEvent;
		
		
		private  DataSetLoadBehaviour dslb;
		
		private bool _active;
		
		
		public override void Reset()
		{
			gameObject = null;
			dataSet = null;
			
			isActive = null;
			isActiveEvent = null;
			isNotActiveEvent = null;
			
			everyFrame = false;
			ignoreInitialState = true;
		}

		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null )
			{
				dslb = go.GetComponent<DataSetLoadBehaviour>();
			}

			_checkDataSetActiveState();

			if (!everyFrame)
			{
				Finish();	
			}
		}

		public override void OnUpdate()
		{
			_checkDataSetActiveState();
		}
		
		private void _checkDataSetActiveState()
		{
			if (dslb==null)
			{
				return;
			}
			
			bool _activeNow = dslb.mDataSetsToActivate.Contains(dataSet.Value);
			
            if (_activeNow && (!_active) )
            {
				if (!everyFrame || !ignoreInitialState)
				{
                	Fsm.Event(isActiveEvent);
				}
			}else if ((!_activeNow) && _active)
            {
                Fsm.Event(isNotActiveEvent);	
            }
			if (!isActive.IsNone)
			{
				isActive.Value = _activeNow;
			}
			_active = _activeNow;
			
		}
	}
}