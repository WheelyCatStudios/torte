#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.

//////////////////////////////////////////////////////////////////////

//

// Copyright (c) 2014 Audiokinetic Inc. / All Rights Reserved

//

//////////////////////////////////////////////////////////////////////



public class AkTriggerMouseUp : AkTriggerBase

{

	private void OnMouseUp()

	{

		if (triggerDelegate != null)

			triggerDelegate(null);

	}

}



#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
