using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace Traductor_Morse
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        #region constantes
        private string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890:,;?'";
        private string abcM = ".- -... -.-. -.. . ..-. --. .... .. .--- -.- .-.. -- -. --- .--. --.- .-. ... - ..- ...- .-- -..- -.-- --.. .---- ..--- ...-- ....- ..... -.... --... ---.. ----. ----- ---... --..-- -.-.-. ..--.. .-..-.";
        private bool bandera1,bandera2;
        #endregion
        public MainPage()
        {
            
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
            tile();
           
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (e.Size.Width <= e.Size.Height)
                VisualStateManager.GoToState(this, "Snapped", false);
            else {
                VisualStateManager.GoToState(this, "FullScreenLandscape", false);
            }
        }

        /// <summary>
        /// Se invoca cuando esta página se va a mostrar en un objeto Frame.
        /// </summary>
        /// <param name="e">Datos de evento que describen cómo se llegó a esta página. La propiedad Parameter
        /// se usa normalmente para configurar la página.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void TextoMorse_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

            this.Titulo.Text = "Traductor de texto a codigo morse";
            this.backButton.Visibility = Visibility.Visible;
            this.GridTextoMorse.Visibility = Visibility.Visible;
            this.texto1.Text = this.morse1.Text = "";
            

        }

        private void MorseTexto_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

            this.Titulo.Text = "Traductor de codigo morse a texto";
            this.backButton.Visibility = Visibility.Visible;
            this.GridMorseTexto.Visibility = Visibility.Visible;
            this.texto2.Text = this.morse2.Text = "";
            
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Titulo.Text = "Traductor de código morse";
            this.GridMorseTexto.Visibility = Visibility.Collapsed;
            this.GridTextoMorse.Visibility = Visibility.Collapsed;
            this.backButton.Visibility = Visibility.Collapsed;
        }

        private void texto1_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            TransformarTextoMorse(this.texto1.Text.ToUpper());
        }

        private void morse2_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            TransformarMorseTexto(this.morse2.Text);
        }

        private void TransformarTextoMorse(string cadena) {
            string[] codigo = abcM.Split(' ');
            string texto = "";
            for (int i = 0; i < cadena.Length; i++ )
            {
                for (int j = 0; j < codigo.Length; j++)
                {
                    if (cadena[i].CompareTo(abc[j]) == 0){
                        texto += " "+codigo[j];
                    }
          
                }
                if (cadena[i].CompareTo(' ') == 0)
                {
                    texto += "  ";
                }
                else
                    if (cadena[i].CompareTo('"') == 0)
                    {
                        texto += " " + codigo[codigo.Length-1];
                    }
            }

            this.morse1.Text = texto;

        }

        private void TransformarMorseTexto(string codigo)
        {
            string[] cadena = codigo.Split(' ');
            string[] cadena2 = abcM.Split(' ');


            string texto = "";
            for (int i = 0; i < cadena.Length; i++)
            {
                for (int j = 0; j < cadena2.Length; j++)
                {
                    if (cadena[i].CompareTo(cadena2[j]) == 0)
                    {
                        texto += abc[j];
                    }

                }

                if (cadena[i].CompareTo("") == 0)
                {
                    texto += " ";
                }
             
            }

            this.texto2.Text = texto;

        }

        private void Xmodal_Click(object sender, RoutedEventArgs e)
        {
            this.modal.Visibility = Visibility.Collapsed;
            bandera1 = bandera2 = false;
            
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.modal.Visibility = Visibility.Visible;
            this.iconoSonido.Visibility = Visibility.Visible;
            this.iconoLuzApagado.Visibility = this.iconoLuzPrendida.Visibility = Visibility.Collapsed;
            //codigo de sonidos
            bandera1 = true;
            await EfectoSonido();
           
           
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.modal.Visibility = Visibility.Visible;
            this.iconoSonido.Visibility = Visibility.Collapsed;
            this.iconoLuzApagado.Visibility = Visibility.Visible;
            this.iconoLuzPrendida.Visibility = Visibility.Collapsed;
            //codigo de luz
            bandera2 = true;
            await EfectoLuz();
            

        }

        private async Task EfectoSonido()
        {

            await Task.Delay(TimeSpan.FromSeconds(0.2));

                string[] codigo = morse1.Text.Split(' ');
                for (int i = 0; i < codigo.Length; i++)
                {
                    if (bandera1 == false) {
                        break;
                    }
                    int j ;
                    for (j = 0; j < codigo[i].Length; j++)
                    {
                        if (bandera1 == false)
                        {
                            break;
                        }
                        if (codigo[i][j].CompareTo('.') == 0)
                        {
                            sonidoPunto.Play();
                            await Task.Delay(TimeSpan.FromSeconds(0.4));
                            sonidoPunto.Stop();
                            await Task.Delay(TimeSpan.FromSeconds(0.5));
                            
                        }
                        else
                        {
                            if (codigo[i][j].CompareTo('-') == 0)
                            {

                                sonidoRaya.Play();
                                await Task.Delay(TimeSpan.FromSeconds(0.99));
                                sonidoRaya.Stop();
                                await Task.Delay(TimeSpan.FromSeconds(0.5));
                            }
                        }


                    }

                    if (j == 0)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(2.5));

                    }
                    else {

                        await Task.Delay(TimeSpan.FromSeconds(1));

                    }

                    
                }

            

        }

        private async Task EfectoLuz()
        {

            await Task.Delay(TimeSpan.FromSeconds(0.2));

            string[] codigo = morse1.Text.Split(' ');
            for (int i = 0; i < codigo.Length; i++)
            {
                if (bandera2 == false)
                {
                    break;
                }
                int j;
                for (j = 0; j < codigo[i].Length; j++)
                {
                    if (codigo[i][j].CompareTo('.') == 0)
                    {

                        Luz(1);
                        await Task.Delay(TimeSpan.FromSeconds(0.4));
                        Luz(0);
                        await Task.Delay(TimeSpan.FromSeconds(0.5));

                    }
                    else
                    {
                        if (codigo[i][j].CompareTo('-') == 0)
                        {

                            Luz(1);
                            await Task.Delay(TimeSpan.FromSeconds(1));
                            Luz(0);
                            await Task.Delay(TimeSpan.FromSeconds(0.5));
                        }
                    }

                    Luz(0);

                }

                if (j == 0)
                {
                    Luz(0);
                    await Task.Delay(TimeSpan.FromSeconds(2.5));

                }
                else
                {

                    Luz(0);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }


            }



        }
        private void Luz(int tipo)
        {

            if (tipo == 0)
            {
                this.iconoLuzPrendida.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.iconoLuzPrendida.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.modal2.Visibility = Visibility.Visible;   
        }

        private void Xmodal2_Click(object sender, RoutedEventArgs e)
        {
            this.modal2.Visibility = Visibility.Collapsed;
        }

        private async void tile() {

            Random rnd = new Random();
            while(true){
                XmlDocument tileData = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideText03);
                XmlNodeList texData = tileData.GetElementsByTagName("text");
               
                int val = rnd.Next(0,abc.Length);
                texData[0].InnerText = "Aprender es facil: \n    " + abc[val] + " = " + abcM.Split(' ')[val];
                TileNotification notificacion = new TileNotification(tileData);
                notificacion.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(10);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(notificacion);
              
                await Task.Delay(TimeSpan.FromSeconds(20));
            }

        }
       
    }
}
