using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TechJam
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;

        private SolidColorBrush shade1 = new SolidColorBrush(Windows.UI.Colors.Black);
        private SolidColorBrush shade2 = new SolidColorBrush(Windows.UI.Colors.DarkSlateGray);
        private SolidColorBrush shade3 = new SolidColorBrush(Windows.UI.Colors.DimGray);
        private SolidColorBrush shade4 = new SolidColorBrush(Windows.UI.Colors.LightSlateGray);
        private SolidColorBrush shade5 = new SolidColorBrush(Windows.UI.Colors.LightGray);
        private SolidColorBrush shade6 = new SolidColorBrush(Windows.UI.Colors.Gainsboro);
        private SolidColorBrush shade7 = new SolidColorBrush(Windows.UI.Colors.OldLace);
        private SolidColorBrush shade8 = new SolidColorBrush(Windows.UI.Colors.White);
        private SolidColorBrush shade9 = new SolidColorBrush(Windows.UI.Colors.OrangeRed);
        private SolidColorBrush bgGreen = new SolidColorBrush(Windows.UI.Colors.Green);

        public static int val;
        public static bool flash;
        public static double stepWidth;

        public MainPage()
        {
            this.InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;

            val = 0;
            flash = false;
            
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {

            //int x;
            //var task = Task.Run(async () => { x = await getItemClient(); if (x == -1) { Debug.WriteLine("issue with http status code"); } else { val = x; } await Task.Delay(1000); });
            //task.Wait();

            //Random rnd = new Random();
            //val = rnd.Next(0, 4);
            ProgressBarBorder.Width = showTxt.RenderSize.Width - 20;
            ProgressBarBorder.HorizontalAlignment = HorizontalAlignment.Left;
            ProgressBarColor.HorizontalAlignment = HorizontalAlignment.Left;
            ProgressBarBorder.VerticalAlignment = VerticalAlignment.Center;
            ProgressBarColor.VerticalAlignment = VerticalAlignment.Center;
            stepWidth = showTxt.RenderSize.Width / 12;
            val = val + 5;
            showTxt.FontSize= config.fSize;

            if (val == 0 && val < (config.start+config.step))
            {
                showTxt.Foreground = shade1;
                showTxt.Text = "W                  ";
                ProgressBarColor.Width = stepWidth;
            }
            else if (val < (2 * config.step))
            {
                showTxt.Foreground = shade2;
                showTxt.Text = "W                  ";
                ProgressBarColor.Width = stepWidth*2;
            }
            else if (val < (3 * config.step))
            {
                showTxt.Foreground = shade3;
                showTxt.Text = "We                 ";
                ProgressBarColor.Width = stepWidth*3;
            }
            else if (val < (4 * config.step))
            {
                showTxt.Foreground = shade4;
                showTxt.Text = "Wel                ";
                ProgressBarColor.Width = stepWidth*4;
            }
            else if (val < (5 * config.step))
            {
                showTxt.Foreground = shade5;
                showTxt.Text = "Welc               ";
                ProgressBarColor.Width = stepWidth*5;
            }
            else if (val < (6 * config.step))
            {
                showTxt.Foreground = shade6;
                showTxt.Text = "Welco              ";
                ProgressBarColor.Width = stepWidth*6;
            }
            else if (val < (7 * config.step))
            {
                showTxt.Foreground = shade7;
                showTxt.Text = "Welcom             ";
                ProgressBarColor.Width = stepWidth*7;
            }
            else if (val < (8 * config.step))
            {
                showTxt.Text = "Welcome            ";
                showTxt.Foreground = shade8;
                //showTxt.FontWeight = FontWeights.Bold;
                ProgressBarColor.Width = stepWidth*8;
            }
            else if (val < (9 * config.step))
            {
                showTxt.Text = "Welcome TECH     ";
                showTxt.Foreground = shade8;
                //showTxt.FontWeight = FontWeights.Bold;
                ProgressBarColor.Width = stepWidth*9;
            }
            else if (val < (10 * config.step))
            {
                showTxt.Text = "Welcome TECH-JAM ";
                showTxt.Foreground = shade8;
                showTxt.FontWeight = FontWeights.Bold;
                ProgressBarColor.Width = stepWidth*10;
            }
            else if (val < (11 * config.step))
            {
                showTxt.Text = "Welcome TECH-JAM!";
                showTxt.Foreground = shade8;
                showTxt.FontWeight = FontWeights.Bold;
                ProgressBarColor.Width = stepWidth*11;
            }
            else
            {
                ProgressBarColor.Width = stepWidth*12;
                ProgressBarBorder.BorderBrush = bgGreen;
                ProgressBarColor.Fill = bgGreen;
                if (flash == false)
                {
                    showTxt.Text = "Welcome to TECHJAM!";
                    showTxt.Foreground = shade8;
                    showTxt.FontWeight = FontWeights.Bold;
                    flash = true;
                }
                else
                {
                    showTxt.Text = "Welcome to TECHJAM!";
                    showTxt.Foreground = shade9;
                    showTxt.FontWeight = FontWeights.Bold;
                    flash = false;
                }
                
            }
        }

        private static async Task<int> getItemClient()
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri url = new Uri("http://techjameventmanager.azurewebsites.net/api/Events");
                client.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync();
                    int value;
                    int.TryParse(data.GetResults(), out value);
                    Debug.WriteLine("value: " + value);
                    return value;
                }
                else
                {
                    Debug.WriteLine("response is -1: "+ response.StatusCode);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //throw;
                return val;
            }
        }
    }
}
