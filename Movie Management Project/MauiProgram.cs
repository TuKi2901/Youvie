using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
#if WINDOWS
    using Microsoft.UI;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Windowing;
    using Windows.Graphics;
    using Microsoft.Maui.LifecycleEvents;
#endif

namespace Movie_Management_Project
{
    public static class MauiProgram
    {

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if WINDOWS
    builder.ConfigureLifecycleEvents(events =>
                        {
                            events.AddWindows(wndLifeCycleBuilder =>
                            {
                                wndLifeCycleBuilder.OnWindowCreated(window =>
                                {
                                    window.ExtendsContentIntoTitleBar = false;
                                    IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                                    WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                                    AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
                                    if(winuiAppWindow.Presenter is OverlappedPresenter p)
                                    {
                                        //p.SetBorderAndTitleBar(false, false);
                                    }
                                    const int width = 1366;
                                    const int height = 768;
            /*I suggest you to use MoveAndResize instead of Resize because this way you make sure to center the window*/
                                    winuiAppWindow.MoveAndResize(new RectInt32(0,0, width, height));
                                });
                            });
                        });
#endif

            #region Test Fullscreen Windown
            //#if WINDOWS


            //            builder.ConfigureLifecycleEvents(events =>
            //                        {
            //                            events.AddWindows(wndLifeCycleBuilder =>
            //                            {
            //                                wndLifeCycleBuilder.OnWindowCreated(window =>
            //                                {
            //                                    window.ExtendsContentIntoTitleBar = true; /*This is important to prevent your app content extends into the title bar area.*/
            //                                    SetAppWindowFullscreen(window);
            //                                    IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
            //                                    WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
            //                                    AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
            //                                    if(winuiAppWindow.Presenter is OverlappedPresenter p)
            //                                    {
            //                                        //p.SetBorderAndTitleBar(false, false);
            //                                    }
            //                                    const int width = 1366;
            //                                    const int height = 768;
            //            /*I suggest you to use MoveAndResize instead of Resize because this way you make sure to center the window*/
            //                                    winuiAppWindow.MoveAndResize(new RectInt32(0,0, width, height));
            //                                });
            //                            });
            //                        });
            //#endif

            #endregion

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();

        }

    }
}