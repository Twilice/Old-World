
public enum CameraStatus
{
    ThirdPersonView,
    FirstPersonView,
    InspectView,
};

public enum ZoomStatus
{
    zoomingIn,
    zoomingOut
};

public class StateController {
    public static CameraStatus currentView = CameraStatus.ThirdPersonView;
    public static ZoomStatus currentZoom = ZoomStatus.zoomingOut;
    public static bool cursorLocked = true;
    public static bool menuOpen = false;
}
