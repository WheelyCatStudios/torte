#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.

//------------------------------------------------------------------------------

// <auto-generated />

//

// This file was automatically generated by SWIG (http://www.swig.org).

// Version 3.0.12

//

// Do not make changes to this file unless you know what you are doing--modify

// the SWIG interface file instead.

//------------------------------------------------------------------------------





public class AkSegmentInfo : global::System.IDisposable {

  private global::System.IntPtr swigCPtr;

  protected bool swigCMemOwn;



  internal AkSegmentInfo(global::System.IntPtr cPtr, bool cMemoryOwn) {

    swigCMemOwn = cMemoryOwn;

    swigCPtr = cPtr;

  }



  internal static global::System.IntPtr getCPtr(AkSegmentInfo obj) {

    return (obj == null) ? global::System.IntPtr.Zero : obj.swigCPtr;

  }



  internal virtual void setCPtr(global::System.IntPtr cPtr) {

    Dispose();

    swigCPtr = cPtr;

  }



  ~AkSegmentInfo() {

    Dispose();

  }



  public virtual void Dispose() {

    lock(this) {

      if (swigCPtr != global::System.IntPtr.Zero) {

        if (swigCMemOwn) {

          swigCMemOwn = false;

          AkSoundEnginePINVOKE.CSharp_delete_AkSegmentInfo(swigCPtr);

        }

        swigCPtr = global::System.IntPtr.Zero;

      }

      global::System.GC.SuppressFinalize(this);

    }

  }



  public int iCurrentPosition { set { AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_iCurrentPosition_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_iCurrentPosition_get(swigCPtr); } 

  }



  public int iPreEntryDuration { set { AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_iPreEntryDuration_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_iPreEntryDuration_get(swigCPtr); } 

  }



  public int iActiveDuration { set { AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_iActiveDuration_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_iActiveDuration_get(swigCPtr); } 

  }



  public int iPostExitDuration { set { AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_iPostExitDuration_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_iPostExitDuration_get(swigCPtr); } 

  }



  public int iRemainingLookAheadTime { set { AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_iRemainingLookAheadTime_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_iRemainingLookAheadTime_get(swigCPtr); } 

  }



  public float fBeatDuration { set { AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_fBeatDuration_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_fBeatDuration_get(swigCPtr); } 

  }



  public float fBarDuration { set { AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_fBarDuration_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_fBarDuration_get(swigCPtr); } 

  }



  public float fGridDuration { set { AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_fGridDuration_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_fGridDuration_get(swigCPtr); } 

  }



  public float fGridOffset { set { AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_fGridOffset_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkSegmentInfo_fGridOffset_get(swigCPtr); } 

  }



  public AkSegmentInfo() : this(AkSoundEnginePINVOKE.CSharp_new_AkSegmentInfo(), true) {

  }



}

#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
