using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Net.NetworkInformation;
using System.Windows.Threading;



namespace MMSystems5Game
{
    public partial class App : Application
    {
        //Hier worden alle objecten aangemaakt zodat eze overal in de phone app kunnen worden aangeroepen
        public static GanzenBordServiceCloud.GanzenbordServiceClient client1;
        public static GanzenBordServiceCloud.Player player;
        public static GanzenBordServiceCloud.Lobby lobby;
        public static GanzenBordServiceCloud.GameState gamestate;
        public static bool KanGooien = false;
       
        public static InloggenVM login;
        public static MaakAccountVM maakaccount;

        public static LobbysListVM lobbylistvm;
        public static PlayersInLobby LobbyInfo;
        public static MaakLobbyVM MaakLobby;
       
        public static DispatcherTimer timer;
        public static DispatcherTimer gametimer;
       
        public static JoinVM join;
        public static StopHostVM stophost;
        public static StartPlayVM start;
        public static GameState Status;
        public static ExitLobbyVM exitlobby;
       
        public static Dice dice;
     
        public static LocatieVM rood;
        public static LocatieVM blauw;
        public static LocatieVM groen;
        public static LocatieVM geel;
        public static BordVm bord;
        public static HighScoreVM highscore;

        public static Network connectie;

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

            // We maken ook 2 timers aan : eentje om lobby lijsten up te daten en 1 om het spel up te daten
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += timer_Tick;

            gametimer = new DispatcherTimer();
            gametimer.Interval = TimeSpan.FromSeconds(2);
            gametimer.Tick += gametimer_Tick;


            client1 = new GanzenBordServiceCloud.GanzenbordServiceClient();
            player = new GanzenBordServiceCloud.Player();
            lobby = new GanzenBordServiceCloud.Lobby();
            gamestate = new GanzenBordServiceCloud.GameState();
            login = new InloggenVM();
            maakaccount = new MaakAccountVM();
            lobbylistvm = new LobbysListVM();
            LobbyInfo = new PlayersInLobby();
            MaakLobby = new MaakLobbyVM();
            join = new JoinVM();
            stophost = new StopHostVM();
            dice = new Dice();
            start = new StartPlayVM();
            Status = new GameState();
            exitlobby = new ExitLobbyVM();

            rood = new LocatieVM();
            blauw = new LocatieVM();
            groen = new LocatieVM();
            geel = new LocatieVM();
            bord = new BordVm();
            highscore = new HighScoreVM();


            DeviceNetworkInformation.NetworkAvailabilityChanged += DeviceNetworkInformation_NetworkAvailabilityChanged;

        }

        void gametimer_Tick(object sender, EventArgs e)
        {
            //Status van de spelers updaten tijdens het spel
            Status.status(player);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            // updaten van de lobby lijsten (lobby info)
            if (lobbylistvm.InfoLobby != null)
            {
                lobbylistvm.TemplateBind = lobbylistvm.InfoLobby;
                LobbyInfo.infolobby(lobbylistvm.TemplateBind);
            }

            else
            {
                if(lobbylistvm.TemplateBind!=null)
                LobbyInfo.infolobby(lobbylistvm.TemplateBind);
            }


            if (lobbylistvm.TemplateBind!=null)
            {
                if (lobbylistvm.TemplateBind.HostPlayer != player.PlayerNaam)
                {
                    lobbylistvm.Join = true;
                }

                else
                {
                    lobbylistvm.Join = false;
                }
                
            }
           
            lobbylistvm.GetLobbys();   
        }

        void DeviceNetworkInformation_NetworkAvailabilityChanged(object sender, NetworkNotificationEventArgs e)
        {
           
            switch (e.NotificationType)
            {
                case NetworkNotificationType.InterfaceConnected:
                    {
                        MessageBox.Show("Connected");
                        break;
                    }
                case NetworkNotificationType.InterfaceDisconnected:
                    {
                        MessageBox.Show("Geen Internet");
                        break;
                    }
                case NetworkNotificationType.CharacteristicUpdate:
                    {
                        MessageBox.Show("Geen Internet");
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Fout met de verbinding");
                        break;
                    }
            }
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            stophost.StopHost(player);
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            stophost.StopHost(player);
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }
       

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}