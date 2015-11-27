/*============================================================================
            Copyright (c) 2010-2011 Qualcomm Connected Experiences, Inc.
            All Rights Reserved.
            Qualcomm Confidential and Proprietary
  ============================================================================*/

#include "VideoPlayerWrapper.h"
#include "VideoPlayerHelper.h"


void* videoPlayerInitIOS()
{
    VideoPlayerHelper* videoPlayerHelper = [[VideoPlayerHelper alloc] init];
    return videoPlayerHelper;
}

bool videoPlayerDeinitIOS(void* dataSetPtr)
{
    if (dataSetPtr == NULL)
    {
        return false;
    }
    
    [((VideoPlayerHelper *) dataSetPtr) deinit];
    [((VideoPlayerHelper *) dataSetPtr) release];
    return true;
}

bool videoPlayerLoadIOS(void* dataSetPtr, const char* filename, int requestType, bool playOnTextureImmediately, float seekPosition)
{
    if (dataSetPtr == NULL)
    {
        return false;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) load:[NSString stringWithUTF8String:filename] mediaType:(MEDIA_TYPE)requestType playImmediately:playOnTextureImmediately fromPosition:seekPosition];
}

bool videoPlayerUnloadIOS(void* dataSetPtr)
{
    if (dataSetPtr == NULL)
    {
        return false;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) unload];
}

bool videoPlayerIsPlayableOnTextureIOS(void* dataSetPtr)
{
    if (dataSetPtr == NULL)
    {
        return false;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) isPlayableOnTexture];
}

bool videoPlayerIsPlayableFullscreenIOS(void* dataSetPtr)
{
    if (dataSetPtr == NULL)
    {
        return false;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) isPlayableFullscreen];
}

bool videoPlayerSetVideoTextureIDIOS(void* dataSetPtr, int textureID)
{
    if (dataSetPtr == NULL)
    {
        return false;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) setVideoTextureID:textureID];
}

int videoPlayerGetStatusIOS(void* dataSetPtr)
{
    if (dataSetPtr == NULL)
    {
        return NOT_READY;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) getStatus];
}

int videoPlayerGetVideoWidthIOS(void* dataSetPtr)
{
    if (dataSetPtr == NULL)
    {
        return 0;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) getVideoWidth];
}

int videoPlayerGetVideoHeightIOS(void* dataSetPtr)
{
    if (dataSetPtr == NULL)
    {
        return 0;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) getVideoHeight];
}

float videoPlayerGetLengthIOS(void* dataSetPtr)
{
    if (dataSetPtr == NULL)
    {
        return 0.0f;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) getLength];
}

bool videoPlayerPlayIOS(void* dataSetPtr, bool fullScreen, float seekPosition)
{
    if (dataSetPtr == NULL)
    {
        return false;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) play:fullScreen fromPosition:seekPosition];
}

bool videoPlayerPauseIOS(void* dataSetPtr)
{
    if (dataSetPtr == NULL)
    {
        return false;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) pause];
}

bool videoPlayerStopIOS(void* dataSetPtr)
{
    if (dataSetPtr == NULL)
    {
        return false;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) stop];
}

int videoPlayerUpdateVideoDataIOS(void* dataSetPtr)
{
    if (dataSetPtr == NULL)
    {
        return NOT_READY;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) updateVideoData];
}

bool videoPlayerSeekToIOS(void* dataSetPtr, float position)
{
    if (dataSetPtr == NULL)
    {
        return false;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) seekTo:position];
}

float videoPlayerGetCurrentPositionIOS(void* dataSetPtr)
{
    if (dataSetPtr == NULL)
    {
        return 0.0f;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) getCurrentPosition];
}

bool videoPlayerSetVolumeIOS(void* dataSetPtr, float value)
{
    if (dataSetPtr == NULL)
    {
        return false;
    }
    
    return [((VideoPlayerHelper *) dataSetPtr) setVolume:value];
}

int videoPlayerGetCurrentBufferingPercentageIOS(void* dataSetPtr)
{
    return 0;
}

void videoPlayerOnPauseIOS(void* dataSetPtr)
{
    if (dataSetPtr == NULL)
    {
        return;
    }
    
    [((VideoPlayerHelper *) dataSetPtr) onPause];
}
