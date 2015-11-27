/*============================================================================== 
 * Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/

using UnityEngine;
using System.Collections;

public class VideoPlaybackUIView : ISampleAppUIView
{
    #region PUBLIC_PROPERTIES
    public CameraDevice.FocusMode FocusMode
    {
        get
        {
            return m_focusMode;
        }
        set
        {
            m_focusMode = value;
        }
    }
    #endregion PUBLIC_PROPERTIES

    #region PUBLIC_MEMBER_VARIABLES
    public event System.Action TappedToClose;
    public SampleAppUIBox mBox;
    public SampleAppUICheckButton mAboutLabel;
    public SampleAppUILabel mVideoPlaybackLabel;
    public SampleAppUICheckButton mExtendedTracking;
    public SampleAppUICheckButton mCameraFlashSettings;
    public SampleAppUICheckButton mAutoFocusSetting;
    public SampleAppUICheckButton mPlayFullscreeSettings;
    public SampleAppUILabel mCameraLabel;
    public SampleAppUIRadioButton mCameraFacing;
    public SampleAppUIButton mCloseButton;
    #endregion PUBLIC_MEMBER_VARIABLES

    #region PRIVATE_MEMBER_VARIABLES
    private CameraDevice.FocusMode m_focusMode;
    #endregion PRIVATE_MEMBER_VARIABLES

    #region PUBLIC_METHODS

    public void LoadView()
    {
        mBox = new SampleAppUIBox(SampleAppUIConstants.BoxRect, SampleAppUIConstants.MainBackground);

        mVideoPlaybackLabel = new SampleAppUILabel(SampleAppUIConstants.RectLabelOne, SampleAppUIConstants.VideoPlaybackLabelStyle);

        string[] aboutStyles = { SampleAppUIConstants.AboutLableStyle, SampleAppUIConstants.AboutLableStyle };
        mAboutLabel = new SampleAppUICheckButton(SampleAppUIConstants.RectLabelAbout, false, aboutStyles);

        string[] extendedTrackingStyles = { SampleAppUIConstants.ExtendedTrackingStyleOff, SampleAppUIConstants.ExtendedTrackingStyleOn };
        mExtendedTracking = new SampleAppUICheckButton(SampleAppUIConstants.RectOptionOne, false, extendedTrackingStyles);

        string[] cameraFlashStyles = { SampleAppUIConstants.CameraFlashStyleOff, SampleAppUIConstants.CameraFlashStyleOn };
        mCameraFlashSettings = new SampleAppUICheckButton(SampleAppUIConstants.RectOptionThree, false, cameraFlashStyles);

        string[] autofocusStyles = { SampleAppUIConstants.AutoFocusStyleOff, SampleAppUIConstants.AutoFocusStyleOn };
        mAutoFocusSetting = new SampleAppUICheckButton(SampleAppUIConstants.RectOptionTwo, false, autofocusStyles);

        string[] playFullScreenStyles = { SampleAppUIConstants.PlayFullscreenModeOff, SampleAppUIConstants.PlayFullscreenModeOn };
        mPlayFullscreeSettings = new SampleAppUICheckButton(SampleAppUIConstants.RectOptionSixteen, false, playFullScreenStyles);

        mCameraLabel = new SampleAppUILabel(SampleAppUIConstants.RectLabelSix, SampleAppUIConstants.CameraLabelStyle);

        string[,] cameraFacingStyles = new string[2, 2] { { SampleAppUIConstants.CameraFacingFrontStyleOff, SampleAppUIConstants.CameraFacingFrontStyleOn }, { SampleAppUIConstants.CameraFacingRearStyleOff, SampleAppUIConstants.CameraFacingRearStyleOn } };
        SampleAppUIRect[] cameraRect = { SampleAppUIConstants.RectOptionsSvnteen, SampleAppUIConstants.RectOptionsEighteen };
        mCameraFacing = new SampleAppUIRadioButton(cameraRect, 1, cameraFacingStyles);

        string[] closeButtonStyles = { SampleAppUIConstants.closeButtonStyleOff, SampleAppUIConstants.closeButtonStyleOn };
        mCloseButton = new SampleAppUIButton(SampleAppUIConstants.CloseButtonRect, closeButtonStyles);
    }

    public void UnLoadView()
    {
        mVideoPlaybackLabel = null;
        mExtendedTracking = null;
        mCameraFlashSettings = null;
        mAutoFocusSetting = null;
        mCameraLabel = null;
        mCameraFacing = null;
        mAboutLabel = null;
        mPlayFullscreeSettings = null;
    }

    public void UpdateUI(bool tf)
    {
        if (!tf)
        {
            return;
        }

        mBox.Draw();
        mVideoPlaybackLabel.Draw();
        mAboutLabel.Draw();
        mExtendedTracking.Draw();
        mPlayFullscreeSettings.Draw();
        mCameraFlashSettings.Draw();
        mAutoFocusSetting.Draw();
        mCameraLabel.Draw();
        mCameraFacing.Draw();
        mCloseButton.Draw();
    }

    public void OnTappedToClose()
    {
        if (this.TappedToClose != null)
        {
            this.TappedToClose();
        }
    }
    #endregion PUBLIC_METHODS

}
