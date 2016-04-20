
public enum CameraStatus
{
    ThirdPersonView,
    FirstPersonView,
    InspectView
};
public class StateController {
    public static CameraStatus currentView = CameraStatus.ThirdPersonView;
    public static bool CursorLocked = true;
    public static bool MenuOpen = true;
}
