using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Data;

namespace Assignment6AirlineReservation
{
    class clsFlightLogic
    {
        /// <summary>
        /// Class used for communicating with the database.
        /// </summary>
        clsDataAccess clsData;

        /// <summary>
        /// This static string holds the new passengers ID
        /// </summary>
        private static string sNewPassID;

        public clsFlightLogic()
        {
            clsData = new clsDataAccess();
        }

        /// <summary>
        /// This method populates the Choose Flight combo box.
        /// </summary>
        public List<string> CreateChooseFlightList(ref int iListCount)
        {
            try
            {
                List<string> sList = new List<string>();
                DataSet ds = new DataSet();

                ds = clsData.RetrieveChooseFlight(ref iListCount);

                for (int i = 0; i < iListCount; i++)
                {
                    sList.Add("Flight: " + ds.Tables[0].Rows[i][1] + " Aircraft: " + ds.Tables[0].Rows[i][2]);
                }

                return sList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method creates a passenger list using the dataaccess class.
        /// </summary>
        /// <param name="iListCount"></param>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        public List<string> CreateChoosePassengertList(ref int iListCount, string sFlightID)
        {
            try
            {
                List<string> sList = new List<string>();
                DataSet ds = new DataSet();

                ds = clsData.RetrieveChoosePassenger(ref iListCount, sFlightID);

                for (int i = 0; i < iListCount; i++)
                {
                    sList.Add(ds.Tables[0].Rows[i][1] + " " + ds.Tables[0].Rows[i][2]);
                }

                return sList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method returns the new passengers ID number
        /// </summary>
        /// <returns></returns>
        public string GetNewPassengerID()
        {
            try
            {
                return sNewPassID;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method creates a passenger ID list.
        /// </summary>
        /// <param name="iListCount"></param>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        public List<string> CreatePassID(ref int iListCount, string sFlightID)
        {
            try
            {
                List<string> sList = new List<string>();
                DataSet ds = new DataSet();

                ds = clsData.RetrieveChoosePassenger(ref iListCount, sFlightID);

                for (int i = 0; i < iListCount; i++)
                {
                    sList.Add(ds.Tables[0].Rows[i][0].ToString());
                }

                return sList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method creates a seat list.
        /// </summary>
        /// <param name="iListCount"></param>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        public List<string> CreateSeatList(ref int iListCount, string sFlightID)
        {
            try
            {
                List<string> sList = new List<string>();
                DataSet ds = new DataSet();

                ds = clsData.RetrieveChosenSeats(ref iListCount, sFlightID);

                for (int i = 0; i < iListCount; i++)
                {
                    sList.Add(ds.Tables[0].Rows[i][0].ToString());
                }

                return sList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method forwards the information from the UI to delete a specific record.
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sCurrentPassID"></param>
        public void ForwardDeletePassenger(string sFlightID, string sCurrentPassID)
        {
            try
            {
                clsData.DeletePassenger(sFlightID, sCurrentPassID);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method returns a string value of the seat number.
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sCurrentPassID"></param>
        /// <returns></returns>
        public string CreatSeatNumber(string sFlightID, string sCurrentPassID)
        {
            try
            {
                return clsData.RetrieveSeatNumber(sFlightID, sCurrentPassID);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method returns the aircraft info to determine the layout of the seats.
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="sAircraft"></param>
        public void Layout(string selection, ref string sAircraft)
        {
            try
            {
                DataSet ds = new DataSet();
                int iListCount = 0;

                ds = clsData.RetrieveChooseFlight(ref iListCount);

                for (int i = 0; i < iListCount; i++)
                {
                    if (selection == "Flight: " + ds.Tables[0].Rows[i][1] + " Aircraft: " + ds.Tables[0].Rows[i][2])
                    {
                        sAircraft = ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method passes informatino needed to add a passenger to the database.
        /// </summary>
        /// <param name="sFirst"></param>
        /// <param name="sLast"></param>
        /// <param name="sFlightID"></param>
        public void AddPassenger(string sFirst, string sLast, string sFlightID)
        {
            try
            {
                sNewPassID = clsData.AddPassenger(sFirst, sLast, sFlightID);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method forwards the information of the seat change to the dataAccess class
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="seat"></param>
        /// <param name="ID"></param>
        public void ChangedSeat(string sFlightID, string seat, string ID)
        {
            try
            {
                int IFID = 0;
                int IID = 0;
                Int32.TryParse(sFlightID, out IFID);
                Int32.TryParse(ID, out IID);

                clsData.ChangeSeat(IFID, seat, IID);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }// end class
}
