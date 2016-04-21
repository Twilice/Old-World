﻿
public enum CameraStatus
{
    ThirdPersonView,
    FirstPersonView,
    InspectView
};
public class StateController {
    public static CameraStatus currentView = CameraStatus.ThirdPersonView;
    public static bool cursorLocked = true;
    public static bool menuOpen = true;
}
