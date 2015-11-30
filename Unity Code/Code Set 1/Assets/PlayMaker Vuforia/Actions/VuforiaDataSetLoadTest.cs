// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Vuforia")]
	[Tooltip("Sends discrete Events based on the dataset load value.")]
	public class VuforiaDataSetLoadTest : FsmStateAction
	{
		
		[ActionSection("Set up")]
		
		[RequiredField]
		[CheckForComponent(typeof(DataSetLoadBehaviour))]
        [Tooltip("The GameObject to check a particular DataSet load state. A 'DataSetLoadBehaviour' component is required on this GameObject.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		public FsmString dataSet;
		
		public bool everyFrame;
		
		public bool ignoreInitialState;
		
		[ActionSection("Result")]
		
		[RequiredField]
		public FsmBool isLoaded;
		
		[Tooltip("Event to send when the dataset becomes loaded.")]
		public FsmEvent isLoadedEvent;

        [Tooltip("Event to send when the dataset becomes unloaded.")]
		public FsmEvent isNotLoadedEvent;
		
		
		private  DataSetLoadBehaviour dslb;
		
		private bool _loaded;
		
		
		public override void Reset()
		{
			gameObject = null;
			dataSet = null;
			
			isLoaded = null;
			isLoadedEvent = null;
			isNotLoadedEvent = null;
			
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

			_checkDataSetLoadState();

			if (!everyFrame)
			{
				Finish();	
			}
		}

		public override void OnUpdate()
		{
			_checkDataSetLoadState();
		}
		
		private void _checkDataSetLoadState()
		{
			if (dslb==null)
			{
				return;
			}
			
			bool _loadedNow = dslb.mDataSetsToLoad.Contains(dataSet.Value);
			
            if (_loadedNow && (!_loaded) )
            {
				if (!everyFrame || !ignoreInitialState)
				{
                	Fsm.Event(isLoadedEvent);
				}
			}else if ((!_loadedNow) && _loaded)
            {
                Fsm.Event(isNotLoadedEvent);	
            }
			if (!isLoaded.IsNone)
			{
				isLoaded.Value = _loadedNow;
			}
			_loaded = _loadedNow;
			
		}
	}
}