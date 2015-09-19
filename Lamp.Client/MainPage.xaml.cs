using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Http;
using Microsoft.AspNet.SignalR.Client.Transports;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lamp.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        GpioPin gpio23;
        public MainPage()
        {
            this.InitializeComponent();
            SetupGpio();
        }

        


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

           
        }

        public async Task SetupGpio()
        {
            var gpioController = GpioController.GetDefault();
            if (gpioController == null)
            {
                //gpio23 = null;
               
               // return;
            }
            gpio23 = gpioController.OpenPin(23);

            await SetupHub();
        }

        private async Task SetupHub()
        {
            var hubConnection = new HubConnection("http://iotpieas.azurewebsites.net/");

            var lampHub = hubConnection.CreateHubProxy("LampHub");

            lampHub.On<bool>("OnSwitch", handleCallback);

            await hubConnection.Start(new LongPollingTransport());


        }

        private void handleCallback(bool turnOn)
        {
            if (gpio23 == null)
            {
               // return;
            }
               
            gpio23.SetDriveMode(GpioPinDriveMode.Output);
            if (turnOn)
            {
                gpio23.Write(GpioPinValue.High);
            }
            else
            {
                gpio23.Write(GpioPinValue.Low);
            }
        }

       
    }
}
