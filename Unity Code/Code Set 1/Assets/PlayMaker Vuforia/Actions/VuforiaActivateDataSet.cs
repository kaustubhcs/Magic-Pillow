// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Vuforia")]
	[Tooltip("Activate or Deactivate a Vuforia DataSet. DataSet Must be loaded first, else it will fail")]
	public class VuforiaActivateDataSet : FsmStateAction
	{
		
		[ActionSection("Set up")]
		
		[RequiredField]
		[CheckForComponent(typeof(DataSetLoadBehaviour))]
        [Tooltip("The GameObject to activate a particular DataSet. A 'DataSetLoadBehaviour' component is required on this GameObject.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		public FsmString dataSet;
		
		[RequiredField]
		public FsmBool activate;
		
		public bool everyFrame;
		
		[ActionSection("Result")]
		
		public FsmEvent successEvent;
		public FsmEvent failureEvent;
		
		
		private  DataSetLoadBehaviour dslb;
		
		public override void Reset()
		{
			gameObject = null;
			dataSet = null;
			activate = null;
			everyFrame = false;
			
			successEvent = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null )
			{
				dslb = go.GetComponent<DataSetLoadBehaviour>();
			}

			_activateDataSet();
			
			if (!everyFrame)
			{
				Finish();	
			}
		}

		public override void OnUpdate()
		{
			_activateDataSet();
		}
		
		private void _activateDataSet()
		{
			if (dslb==null)
			{
				Fsm.Event(failureEvent);
				return;
			}
			
			if (!QCARRuntimeUtilities.IsQCAREnabled())
	        {
					Fsm.Event(failureEvent);
	            return;
	        }
			
            if ( PlayMakerVuforiaProxy.ActivateDataSet(dataSet.Value,activate.Value))
			{
				Fsm.Event(successEvent);
            }else {
				Fsm.Event(failureEvent);
            }
		
		}
		
		
		
	}
}