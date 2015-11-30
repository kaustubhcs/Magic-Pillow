using UnityEngine;
using System.Collections;

public class PlayMakerVuforiaProxy : MonoBehaviour {
	
	public bool debug = true;
	
	
	public static bool ActivateDataSet(string dataSet,bool state)
	{
		if (!QCARRuntimeUtilities.IsQCAREnabled())
		{
			return false;
		}
		
		if (QCARRuntimeUtilities.IsPlayMode())
		{
			// initialize QCAR 
			QCARUnity.CheckInitializationError();
		}
		
		if (TrackerManager.Instance.GetTracker<ImageTracker>() == null)
			
		{
			return false;
		}
		
		ImageTracker imageTracker = (ImageTracker)TrackerManager.Instance.GetTracker<ImageTracker>();
		
		foreach(DataSet _set in imageTracker.GetDataSets())
		{
			Debug.Log (_set.Path);
			
			/*
			if (state)
			{
				return imageTracker.ActivateDataSet(dataSet);
			}else{
				return imageTracker.DeactivateDataSet(dataSet);
			}
			*/	
		}
		return false;
	}
	
	public static bool RefreshDatasets(DataSetLoadBehaviour dataSetBehaviour)
	{
		
		
		if (!QCARRuntimeUtilities.IsQCAREnabled())
		{
			return false;
		}
		
		if (QCARRuntimeUtilities.IsPlayMode())
		{
			// initialize QCAR 
			QCARUnity.CheckInitializationError();
		}
		
		if (TrackerManager.Instance.GetTracker<ImageTracker>() == null)
		{
			TrackerManager.Instance.InitTracker<ImageTracker>();
		}else{
			ImageTracker imageTracker = (ImageTracker)TrackerManager.Instance.GetTracker<ImageTracker>();
			imageTracker.DestroyAllDataSets(true);
		}
		
		if (dataSetBehaviour.mDataSetsToLoad.Count <= 0)
		{
			Debug.LogWarning("No data sets defined. Not loading any data sets.");
			return false;
		}
		
		foreach (string dataSetName in dataSetBehaviour.mDataSetsToLoad)
		{
			if (!DataSet.Exists(dataSetName))
			{
				Debug.LogWarning("Data set " + dataSetName + " does not exist.");
				return false;
			}
			
			ImageTracker imageTracker = (ImageTracker)TrackerManager.Instance.GetTracker<ImageTracker>();
			DataSet dataSet = imageTracker.CreateDataSet();
			
			if (!dataSet.Load(dataSetName))
			{
				Debug.LogWarning("Failed to load data set " + dataSetName + ".");
				return false;
			}
			
			// Activate the data set if it is the one specified in the editor.
			if (dataSetBehaviour.mDataSetsToActivate.Contains(dataSetName))
			{
				imageTracker.ActivateDataSet(dataSet);
			}
		}
		
		return true;
	}
	
}