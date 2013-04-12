using Callisto.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Callisto.Controls;
using Windows.UI;

// La plantilla Aplicación vacía está documentada en http://go.microsoft.com/fwlink/?LinkId=234227

namespace Traductor_Morse
{
    /// <summary>
    /// Proporciona un comportamiento específico de la aplicación para complementar la clase Application predeterminada.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Inicializa el objeto de aplicación Singleton. Esta es la primera línea de código creado
        /// ejecutado y, como tal, es el equivalente lógico de main() o WinMain().
        /// </summary>
        /// 
        private bool m_settingsReady = false;
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Se invoca cuando la aplicación la inicia normalmente el usuario final. Se usarán otros puntos
        /// de entrada cuando la aplicación se inicie para abrir un archivo específico, para mostrar
        /// resultados de la búsqueda, etc.
        /// </summary>
        /// <param name="args">Información detallada acerca de la solicitud y el proceso de inicio.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {

            //***************************************************************************************
            // Notifícame cuando el usuario abra el panel de Settings
            if (!this.m_settingsReady)
            {
                SettingsPane.GetForCurrentView().CommandsRequested += App_CommandsRequested;
                this.m_settingsReady = true;
            }

            //***************************************************************************************
            Frame rootFrame = Window.Current.Content as Frame;

            // No repetir la inicialización de la aplicación si la ventana tiene contenido todavía,
            // solo asegurarse de que la ventana está activa.
            if (rootFrame == null)
            {
                // Crear un marco para que actúe como contexto de navegación y navegar a la primera página.
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Cargar el estado de la aplicación suspendida previamente
                }

                // Poner el marco en la ventana actual.
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Cuando no se restaura la pila de navegación para navegar a la primera página,
                // configurar la nueva página al pasar la información requerida como parámetro
                // parámetro
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Asegurarse de que la ventana actual está activa.
            Window.Current.Activate();
        }


        //***************************************************************************************
        
        void App_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs eventArgs)
        {
            // Acerca de (información de contacto)
            SettingsCommand about =
            new SettingsCommand("About", "Acerca de", (x) =>
            {
                SettingsFlyout settings = new SettingsFlyout();
                settings.FlyoutWidth = Callisto.Controls.SettingsFlyout.SettingsFlyoutWidth.Narrow;
                settings.HeaderText = "Acerca de";
                settings.HeaderBrush = new SolidColorBrush(Color.FromArgb(255,44,97,193));
                settings.Background = new SolidColorBrush(Colors.White);

                settings.Content = new MyUserControl1();
                
                
                settings.IsOpen = true;
            });
            eventArgs.Request.ApplicationCommands.Add(about);
            
            // Política de privacidad
            SettingsCommand privacyPolicyCommand =
            new SettingsCommand("politicaPrivacidad",
            "Política de privacidad", (x) =>
            {
                SettingsFlyout settings = new SettingsFlyout();
                settings.FlyoutWidth = Callisto.Controls.SettingsFlyout.SettingsFlyoutWidth.Narrow;
                settings.HeaderText = "Política de privacidad";
                settings.HeaderBrush = new SolidColorBrush(Color.FromArgb(255, 44, 97, 193));
                settings.Background = new SolidColorBrush(Colors.White);

                settings.Content = new MyUserControl2();
                
                settings.IsOpen = true;
            }
            );
            eventArgs.Request.ApplicationCommands.Add(privacyPolicyCommand);


            // Política de privacidad
            SettingsCommand ayuda =
            new SettingsCommand("ayuda",
            "Ayuda", (x) =>
            {
                SettingsFlyout settings = new SettingsFlyout();
                settings.FlyoutWidth = Callisto.Controls.SettingsFlyout.SettingsFlyoutWidth.Narrow;
                settings.HeaderText = "Ayuda";
                settings.HeaderBrush = new SolidColorBrush(Color.FromArgb(255, 44, 97, 193));
                settings.Background = new SolidColorBrush(Colors.White);

                settings.Content = new MyUserControl3();

                settings.IsOpen = true;
            }
            );
            eventArgs.Request.ApplicationCommands.Add(ayuda);
            
        }
        
        //***************************************************************************************

        /// <summary>
        /// Se invoca al suspender la ejecución de la aplicación. El estado de la aplicación se guarda
        /// sin saber si la aplicación se terminará o se reanudará con el contenido
        /// de la memoria aún intacto.
        /// </summary>
        /// <param name="sender">Origen de la solicitud de suspensión.</param>
        /// <param name="e">Detalles sobre la solicitud de suspensión.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Guardar el estado de la aplicación y detener toda actividad en segundo plano
            deferral.Complete();
        }
    }
}
