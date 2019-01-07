using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// This class handles all the communication with the DataAcess class and the UI.
        /// </summary>
        clsFlightLogic clsLogic;

        /// <summary>
        /// This Window handles the adding of passengers.
        /// </summary>
        wndAddPassenger wndAddPass;

        /// <summary>
        /// This string holds on to the current flight that is selected.
        /// </summary>
        private string sFlightID = "";

        /// <summary>
        /// This bool will prevent the seats from being clicked unless a flight has been chosen.
        /// </summary>
        private bool bFlightChosen = false;

        /// <summary>
        /// This bool determines if the change seat method has been called.
        /// </summary>
        bool bChangeSeat = false;

        /// <summary>
        /// this variable holds the currently selected passengers ID index in the combo box.
        /// </summary>
        private int iCurrentPassCBIndex;

        /// <summary>
        /// This label holds the most recent clicked label.
        /// </summary>
        private Label lCurrentLabel;

        /// <summary>
        /// This string holds the current passengers ID.
        /// </summary>
        private string sCurrentPassID;

        /// <summary>
        /// This list holds the passenger ID's in the order of the combo box
        /// </summary>
        private List<string> lCurrentPassIDs;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                clsLogic = new clsFlightLogic();
                wndAddPass = new wndAddPassenger();
                PopulateChooseFlightCB();

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// This method populates the Choose Flight combo box.
        /// </summary>
        private void PopulateChooseFlightCB()
        {
            try
            {
                List<string> sList = new List<string>();
                int iListCount = 0;

                sList = clsLogic.CreateChooseFlightList(ref iListCount);

                for (int i = 0; i < iListCount; i++)
                {
                    cbChooseFlight.Items.Add(sList[i]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method populates the Choose Passenger combo box.
        /// </summary>
        private void PopulateChoosePassengerCB(string sFlightID)
        {
            try
            {
                List<string> sList = new List<string>();
                int iListCount = 0;
                PopulateCurrentPassID();

                sList = clsLogic.CreateChoosePassengertList(ref iListCount, sFlightID);

                cbChoosePassenger.Items.Clear();
                for (int i = 0; i < iListCount; i++)
                {
                    cbChoosePassenger.Items.Add(sList[i]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method populates the lCurrentPassIDs List.
        /// </summary>
        private void PopulateCurrentPassID()
        {
            try
            {
                int iListCount = 0;


                lCurrentPassIDs = clsLogic.CreatePassID(ref iListCount, sFlightID);

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method clears all the seats. Use this method just before a populateseats method.
        /// </summary>
        /// <param name="sFlightID"></param>
        private void ClearSeatColors(string sFlightID)
        {
            if (sFlightID == "1")
            {
                Seat1.Background = Brushes.Blue;
                Seat2.Background = Brushes.Blue;
                Seat3.Background = Brushes.Blue;
                Seat4.Background = Brushes.Blue;
                Seat5.Background = Brushes.Blue;
                Seat6.Background = Brushes.Blue;
                Seat7.Background = Brushes.Blue;
                Seat8.Background = Brushes.Blue;
                Seat9.Background = Brushes.Blue;
                Seat10.Background = Brushes.Blue;
                Seat11.Background = Brushes.Blue;
                Seat12.Background = Brushes.Blue;
                Seat13.Background = Brushes.Blue;
                Seat14.Background = Brushes.Blue;
                Seat15.Background = Brushes.Blue;
                Seat16.Background = Brushes.Blue;
            }
            else
            {
                SeatA1.Background = Brushes.Blue;
                SeatA2.Background = Brushes.Blue;
                SeatA3.Background = Brushes.Blue;
                SeatA5.Background = Brushes.Blue;
                SeatA6.Background = Brushes.Blue;
                SeatA7.Background = Brushes.Blue;
                SeatA9.Background = Brushes.Blue;
                SeatA10.Background = Brushes.Blue;
                SeatA11.Background = Brushes.Blue;
                SeatA13.Background = Brushes.Blue;
                SeatA14.Background = Brushes.Blue;
                SeatA15.Background = Brushes.Blue;
                SeatA16.Background = Brushes.Blue;
                SeatA17.Background = Brushes.Blue;
                SeatA18.Background = Brushes.Blue;
            }
        }

        /// <summary>
        /// This miethod populates the seats in the flight image.
        /// </summary>
        /// <param name="sFlightID"></param>
        private void PopulateSeats(string sFlightID)
        {
            try
            {
                List<string> sList = new List<string>();
                int iListCount = 0;
                lblPassengersSeatNumber.Content = "";
                lblPassengersSeatNumber.Background = Brushes.LightGray;

                sList = clsLogic.CreateSeatList(ref iListCount, sFlightID);

                if (sFlightID == "1")
                {
                    for (int i = 0; i < iListCount; i++)
                    {
                        switch (sList[i])
                        {
                            case "1":
                                Seat1.Background = Brushes.Red;
                                break;

                            case "2":
                                Seat2.Background = Brushes.Red;
                                break;

                            case "3":
                                Seat3.Background = Brushes.Red;
                                break;

                            case "4":
                                Seat4.Background = Brushes.Red;
                                break;

                            case "5":
                                Seat5.Background = Brushes.Red;
                                break;

                            case "6":
                                Seat6.Background = Brushes.Red;
                                break;

                            case "7":
                                Seat7.Background = Brushes.Red;
                                break;

                            case "8":
                                Seat8.Background = Brushes.Red;
                                break;

                            case "9":
                                Seat9.Background = Brushes.Red;
                                break;

                            case "10":
                                Seat10.Background = Brushes.Red;
                                break;

                            case "11":
                                Seat11.Background = Brushes.Red;
                                break;

                            case "12":
                                Seat12.Background = Brushes.Red;
                                break;

                            case "13":
                                Seat13.Background = Brushes.Red;
                                break;

                            case "14":
                                Seat14.Background = Brushes.Red;
                                break;

                            case "15":
                                Seat15.Background = Brushes.Red;
                                break;

                            case "16":
                                Seat16.Background = Brushes.Red;
                                break;
                        }// end switch
                    }// end for loop

                }
                else
                {
                    for (int i = 0; i < iListCount; i++)
                    {
                        switch (sList[i])
                        {
                            case "1":
                                SeatA1.Background = Brushes.Red;
                                break;

                            case "2":
                                SeatA2.Background = Brushes.Red;
                                break;

                            case "3":
                                SeatA3.Background = Brushes.Red;
                                break;

                            case "5":
                                SeatA5.Background = Brushes.Red;
                                break;

                            case "6":
                                SeatA6.Background = Brushes.Red;
                                break;

                            case "7":
                                SeatA7.Background = Brushes.Red;
                                break;

                            case "9":
                                SeatA9.Background = Brushes.Red;
                                break;

                            case "10":
                                SeatA10.Background = Brushes.Red;
                                break;

                            case "11":
                                SeatA11.Background = Brushes.Red;
                                break;

                            case "13":
                                SeatA13.Background = Brushes.Red;
                                break;

                            case "14":
                                SeatA14.Background = Brushes.Red;
                                break;

                            case "15":
                                SeatA15.Background = Brushes.Red;
                                break;

                            case "16":
                                SeatA16.Background = Brushes.Red;
                                break;

                            case "17":
                                SeatA17.Background = Brushes.Red;
                                break;

                            case "18":
                                SeatA18.Background = Brushes.Red;
                                break;
                        }// end switch
                    }// end for loop
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method gets the current passenger combo box Index.
        /// </summary>
        /// <returns></returns>
        private int GetCurrentCBPassengerIndex()
        {
            try
            {
                return iCurrentPassCBIndex; ;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method sets the current passenger combo box Index.
        /// </summary>
        private void SetCurrentCBPassengerIndex()
        {
            try
            {
                iCurrentPassCBIndex = cbChoosePassenger.SelectedIndex;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method deals with all seat labels when clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Seat_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (bFlightChosen == true)
                {
                    lCurrentLabel = (Label)sender;// grabs the clicked label
                    string sSeatNumber;// used for holding the seat number 
                    int iListCount = 0;
                    List<string> sList = new List<string>();
                    sList = clsLogic.CreateSeatList(ref iListCount, sFlightID);
                    PopulateSeats(sFlightID);
                    PopulateChoosePassengerCB(sFlightID);

                    if (bChangeSeat == false)// This if statement checks to see if the change seat button was pressed.
                    {
                        if (lCurrentLabel.Background == Brushes.Red)
                        {
                            lCurrentLabel.Background = Brushes.LimeGreen;

                            sSeatNumber = lCurrentLabel.Content.ToString();

                            for (int i = 0; i < cbChoosePassenger.Items.Count; i++)
                            {
                                if (sSeatNumber == sList[i])
                                {
                                    cbChoosePassenger.SelectedIndex = i;
                                    lblPassengersSeatNumber.Content = lCurrentLabel.Content;
                                    lblPassengersSeatNumber.Background = lCurrentLabel.Background;
                                }
                            }
                        }
                        else
                        {
                            cbChoosePassenger.SelectedIndex = -1;
                        }
                    }
                    else
                    {

                        if (lCurrentLabel.Background == Brushes.Red)
                        {

                        }
                        else
                        {
                            //Checks to see if this seat was clicked after a new passenger was made or not
                            if (wndAddPass.GetsubmissionStatus() == false)
                            {
                                
                                sSeatNumber = lCurrentLabel.Content.ToString();

                                for (int i = 0; i < cbChoosePassenger.Items.Count; i++)
                                {
                                    if (sSeatNumber == sList[i])
                                    {
                                        cbChoosePassenger.SelectedIndex = i;
                                    }
                                }

                                int selectedindex = GetCurrentCBPassengerIndex();

                                string sPassID = lCurrentPassIDs[GetCurrentCBPassengerIndex()];

                                clsLogic.ChangedSeat(sFlightID, lCurrentLabel.Content.ToString(), lCurrentPassIDs[GetCurrentCBPassengerIndex()]);

                            }
                            else
                            {
                                clsLogic.ChangedSeat(sFlightID, lCurrentLabel.Content.ToString(), clsLogic.GetNewPassengerID());
                                wndAddPass.SetsubmissionStatus(false);
                            }

                            lCurrentLabel.Background = Brushes.LimeGreen;
                            lblPassengersSeatNumber.Content = lCurrentLabel.Content;
                            lblPassengersSeatNumber.Background = lCurrentLabel.Background;
                            bChangeSeat = false;
                            ClearSeatColors(sFlightID);
                            PopulateSeats(sFlightID);
                            cmdAddPassenger.IsEnabled = true;
                            cmdDeletePassenger.IsEnabled = true;
                            cbChooseFlight.IsEnabled = true;
                            cbChoosePassenger.IsEnabled = true;
                            cmdChangeSeat.IsEnabled = true;

                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This method shows or hides the correct canvas with the seat images/labels.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                bFlightChosen = true;
                string selection = cbChooseFlight.SelectedItem.ToString();
                cbChoosePassenger.IsEnabled = true;
                gPassengerCommands.IsEnabled = true;
                clsLogic.Layout(selection, ref sFlightID);
                wndAddPass.SetFlightID(sFlightID);

                if (sFlightID == "1")
                {
                    CanvasA380.Visibility = Visibility.Hidden;
                    Canvas767.Visibility = Visibility.Visible;
                }
                else
                {
                    Canvas767.Visibility = Visibility.Hidden;
                    CanvasA380.Visibility = Visibility.Visible;
                }

                PopulateChoosePassengerCB(sFlightID);
                PopulateSeats(sFlightID);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This method returns the seat number.
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sCurrentPassID"></param>
        /// <returns></returns>
        private string GrabSeatNumber(string sFlightID, string sCurrentPassID)
        {
            try
            {
                return clsLogic.CreatSeatNumber(sFlightID, sCurrentPassID);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method updates the seats depending on who is selected in the passsengers combo box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbChoosePassenger.SelectedItem == null)
                {
                    // not sure why the clear(); calls this method, but it does. Everything is null and throws an error.
                }
                else
                {
                    SetCurrentCBPassengerIndex();
                    PopulateSeats(sFlightID);
                    sCurrentPassID = lCurrentPassIDs[cbChoosePassenger.SelectedIndex];
                    string sSeatNumber = GrabSeatNumber(sFlightID, sCurrentPassID);
                    lblPassengersSeatNumber.Content = sSeatNumber;
                    lblPassengersSeatNumber.Background = Brushes.LimeGreen;

                    if (sFlightID == "1")
                    {

                        switch (sSeatNumber)
                        {
                            case "1":
                                Seat1.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat1;
                                break;

                            case "2":
                                Seat2.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat2;
                                break;

                            case "3":
                                Seat3.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat3;
                                break;

                            case "4":
                                Seat4.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat4;
                                break;

                            case "5":
                                Seat5.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat5;
                                break;

                            case "6":
                                Seat6.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat6;
                                break;

                            case "7":
                                Seat7.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat7;
                                break;

                            case "8":
                                Seat8.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat8;
                                break;

                            case "9":
                                Seat9.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat9;
                                break;

                            case "10":
                                Seat10.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat10;
                                break;

                            case "11":
                                Seat11.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat11;
                                break;

                            case "12":
                                Seat12.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat12;
                                break;

                            case "13":
                                Seat13.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat13;
                                break;

                            case "14":
                                Seat14.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat14;
                                break;

                            case "15":
                                Seat15.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat15;
                                break;

                            case "16":
                                Seat16.Background = Brushes.LimeGreen;
                                lCurrentLabel = Seat16;
                                break;
                        }// end switch


                    }
                    else
                    {

                        switch (sSeatNumber)
                        {
                            case "1":
                                SeatA1.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA1;
                                break;

                            case "2":
                                SeatA2.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA2;
                                break;

                            case "3":
                                SeatA3.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA3;
                                break;

                            case "5":
                                SeatA5.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA5;
                                break;

                            case "6":
                                SeatA6.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA6;
                                break;

                            case "7":
                                SeatA7.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA7;
                                break;

                            case "9":
                                SeatA9.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA9;
                                break;

                            case "10":
                                SeatA10.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA10;
                                break;

                            case "11":
                                SeatA11.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA11;
                                break;

                            case "13":
                                SeatA13.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA13;
                                break;

                            case "14":
                                SeatA14.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA14;
                                break;

                            case "15":
                                SeatA15.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA15;
                                break;

                            case "16":
                                SeatA16.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA16;
                                break;

                            case "17":
                                SeatA17.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA17;
                                break;

                            case "18":
                                SeatA18.Background = Brushes.LimeGreen;
                                lCurrentLabel = SeatA18;
                                break;
                        }// end switch

                    }

                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This method opens a new window for a new passenger for the current flight to be added.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddPass.ShowDialog();
                //IF submission happened, then select that new person/call change seat
                if (wndAddPass.GetsubmissionStatus() == true)
                {
                    PopulateChoosePassengerCB(sFlightID);

                    cmdChangeSeat_Click(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This method Handles errors at the top level.
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// This method disables all other inputs til a new seat is chosen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cmdAddPassenger.IsEnabled = false;
                cmdDeletePassenger.IsEnabled = false;
                cbChooseFlight.IsEnabled = false;
                cbChoosePassenger.IsEnabled = false;
                cmdChangeSeat.IsEnabled = false;
                bChangeSeat = true;

                if (wndAddPass.GetsubmissionStatus() == true)
                {

                }
                else
                {
                    lCurrentLabel.Background = Brushes.Blue;
                }


            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This method Deletes the current passenger from the current flight.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clsLogic.ForwardDeletePassenger(sFlightID, sCurrentPassID);
                lCurrentLabel.Background = Brushes.Blue;
                lblPassengersSeatNumber.Content = "";
                lblPassengersSeatNumber.Background = Brushes.LightGray;
                PopulateChoosePassengerCB(sFlightID);

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }// end class
}
