// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Vuforia")]
	[Tooltip("Load or Unload a Vuforia DataSet")]
	public class VuforiaLoadDataSet : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(DataSetLoadBehaviour))]
        [Tooltip("The GameObject to load a particular DataSet. A 'DataSetLoadBehaviour' component is required on this GameObject.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		public FsmString dataSet;
		
		[RequiredField]
		public FsmBool load;
		
		public bool everyFrame;
		
		private DataSetLoadBehaviour dslb;
		
		public override void Reset()
		{
			gameObject = null;
			dataSet = null;
			load = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null )
			{
				dslb = go.GetComponent<DataSetLoadBehaviour>();
			}

			_loadDataSet();
			
			if (!everyFrame)
			{
				Finish();	
			}
		}

		public override void OnUpdate()
		{
			_loadDataSet();
		}
		
		private void _loadDataSet()
		{
			if (dslb==null)
			{
				return;
			}
			
			 bool prevLoadDataSet = dslb.mDataSetsToLoad.Contains(dataSet.Value);
			
			// ACTIVATE
            // Remove data sets that are being unchecked.
            if (prevLoadDataSet && (!load.Value) )
            {
                dslb.mDataSetsToLoad.Remove(dataSet.Value);
				PlayMakerVuforiaProxy.RefreshDatasets(dslb);
            }
            // Add data sets that are being checked.
            else if ((!prevLoadDataSet) && load.Value)
            {
                dslb.mDataSetsToLoad.Add(dataSet.Value);
				PlayMakerVuforiaProxy.RefreshDatasets(dslb);
            }
			
			
		
		}
	}
}