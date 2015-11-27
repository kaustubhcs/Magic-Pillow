/*==============================================================================
            Copyright (c) 2010-2013 QUALCOMM Austria Research Center GmbH.
            All Rights Reserved.
            Qualcomm Confidential and Proprietary
==============================================================================*/

// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

// original script: DefaultTrackableEventHandler
// modified to fire PlayMaker events.

using UnityEngine;
using HutongGames.PlayMaker;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface to Fire PlayMaker Events.
/// </summary>
public class PlayMakerVuforiaTrackableProxy : MonoBehaviour, ITrackableEventHandler
{

    private TrackableBehaviour mTrackableBehaviour;
    
	private bool debug;
	
	/// <summary>
	/// The fsm proxy used to send the Vuforia event to Fsm.
	/// </summary>
	PlayMakerFSM fsmProxy;
	
	void Awake()
	{
		_awakePlayMakerProxy();
	}
	
    void OnEnable()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }
	
	void OnDisable()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
        }
	}


	/// <summary>
	/// Implementation of the ITrackableEventHandler function called when the
	/// tracking state changes.
	/// </summary>
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}



	private void OnTrackingFound()
	{
		FireVuforiaEvent("VUFORIA / TRACKING FOUND");
	}
	
	
	private void OnTrackingLost()
	{
		FireVuforiaEvent("VUFORIA / TRACKING LOST");
	}


	/*
	[ContextMenu("Help")]
	public void help ()
	{
	    Application.OpenURL ("https://hutonggames.fogbugz.com/default.asp?W990");
	}
	*/
	
	
	// get the Playmaker Photon proxy fsm.
	void _awakePlayMakerProxy () {
	
		// get the vuforia proxy to fire event from
		GameObject go = GameObject.Find("PlayMaker Vuforia Proxy");
		
		if (go == null )
		{
			Debug.LogError("Working with vuforia require that you add a 'PlayMaker Vuforia Proxy' component to the gameObject. You can do so from the menu 'PlayMaker Vuforia/components/Add vuforia proxy to scene'");
			return;
		}
		
		// get the proxy to set the debug flag.
	 	PlayMakerVuforiaProxy _proxy = go.GetComponent<PlayMakerVuforiaProxy>();
		if (_proxy!=null)
		{
			debug = _proxy.debug;
		}
		
		// get the Fsm for reference when sending events.
		fsmProxy = go.GetComponent<PlayMakerFSM>();
		if (fsmProxy==null)
		{
			return;
		}
		
	}// Awake
	
	

	void FireVuforiaEvent(string eventName)
	{
		if (debug) {
			Debug.Log("sending "+eventName+" event to "+this.gameObject.name);
		}
		
		// set the target to be this gameObject.
		FsmOwnerDefault goTarget = new FsmOwnerDefault();
		goTarget.GameObject = new FsmGameObject();
		goTarget.GameObject.Value = this.gameObject;
		goTarget.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
		
       // send the event to this gameObject and all its children
		FsmEventTarget eventTarget = new FsmEventTarget();
		eventTarget.excludeSelf = false;
		eventTarget.target = FsmEventTarget.EventTarget.GameObject;
		eventTarget.gameObject = goTarget;
		eventTarget.sendToChildren = true;
		
		// create the event.
		FsmEvent fsmEvent = new FsmEvent(eventName);
	
		// send the event
		fsmProxy.Fsm.Event(eventTarget,fsmEvent.Name); // TOFIX: doesn't work if using simply fsmEvent
		
	}// FireVuforiaEvent

}
