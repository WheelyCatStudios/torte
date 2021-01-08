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











public class AkMonitoringCallbackInfo : global::System.IDisposable {



  private global::System.IntPtr swigCPtr;



  protected bool swigCMemOwn;







  internal AkMonitoringCallbackInfo(global::System.IntPtr cPtr, bool cMemoryOwn) {



    swigCMemOwn = cMemoryOwn;



    swigCPtr = cPtr;



  }







  internal static global::System.IntPtr getCPtr(AkMonitoringCallbackInfo obj) {



    return (obj == null) ? global::System.IntPtr.Zero : obj.swigCPtr;



  }







  internal virtual void setCPtr(global::System.IntPtr cPtr) {



    Dispose();



    swigCPtr = cPtr;



  }







  ~AkMonitoringCallbackInfo() {



    Dispose();



  }







  public virtual void Dispose() {



    lock(this) {



      if (swigCPtr != global::System.IntPtr.Zero) {



        if (swigCMemOwn) {



          swigCMemOwn = false;



          AkSoundEnginePINVOKE.CSharp_delete_AkMonitoringCallbackInfo(swigCPtr);



        }



        swigCPtr = global::System.IntPtr.Zero;



      }



      global::System.GC.SuppressFinalize(this);



    }



  }







  public AkMonitorErrorCode errorCode { get { return (AkMonitorErrorCode)AkSoundEnginePINVOKE.CSharp_AkMonitoringCallbackInfo_errorCode_get(swigCPtr); } 



  }







  public AkMonitorErrorLevel errorLevel { get { return (AkMonitorErrorLevel)AkSoundEnginePINVOKE.CSharp_AkMonitoringCallbackInfo_errorLevel_get(swigCPtr); } 



  }







  public uint playingID { get { return AkSoundEnginePINVOKE.CSharp_AkMonitoringCallbackInfo_playingID_get(swigCPtr); } 



  }







  public ulong gameObjID { get { return AkSoundEnginePINVOKE.CSharp_AkMonitoringCallbackInfo_gameObjID_get(swigCPtr); } 



  }







  public string message { get { return AkSoundEngine.StringFromIntPtrOSString(AkSoundEnginePINVOKE.CSharp_AkMonitoringCallbackInfo_message_get(swigCPtr)); } 



  }







  public AkMonitoringCallbackInfo() : this(AkSoundEnginePINVOKE.CSharp_new_AkMonitoringCallbackInfo(), true) {



  }







}



#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.


