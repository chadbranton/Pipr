#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Web;
using Pipr.Properties;
using System.Diagnostics;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Xml.Linq;
using Microsoft.Office365.Exchange;
using Microsoft.Office365.OAuth;
using Pipr.Models;
using Google.GData.Client;
using System.ServiceModel.Syndication;
using Google.GData.Extensions;
using Google.GData.Calendar;
using Google.Apis.Tasks.v1.Data;
using Google.Apis.Tasks.v1;
using Microsoft.Scripting.Utils;
using Google.Apis.Services;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Text.RegularExpressions;

#endregion


namespace Pipr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variable Declaration
       
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechRecognitionEngine startlistening = new SpeechRecognitionEngine();
        public SpeechSynthesizer Piper = new SpeechSynthesizer();
        Grammar wordscommandgrammar;
        Grammar shellcommandgrammar;
        Grammar socialcommandgrammar;
        Grammar defaultcommandgrammar;
        Grammar alarmclockgrammar;
        Grammar timesetgrammarAM;
        Grammar timesetgrammarPM;
        bool babble;
        string userName = Environment.UserName;   
        public string QEvent;        
        string BrowseDirectory;
        double timer = 10;
        int count = 1;
        int i = 0;
        int recTimeOut = 0;
        public int EmailNum = 0;
        Random rand = new Random();
        DateTime dt9pm = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 0, 0);
        DateTime time;
        string Temperature;
        string Condition;
        string Humidity;
        string WindSpeed;
        string Town;
        string TFCond;
        string TFHigh;
        string TFLow;
        string[] portRange = new string[] { "1", "1000" };
        List<string> defaultCommands = new List<string>();
        List<string> shellCommands = new List<string>();
        List<string> shellResponse = new List<string>();
        List<string> shellLocation = new List<string>();
        List<string> socialCommands = new List<string>();
        List<string> socialResponse = new List<string>();
        List<string> words = new List<string>();
        PiprDB context = new PiprDB();        
        String[] AlarmAM = { "1:00 AM", "1:01 AM", "1:02 AM", "1:03 AM", "1:04 AM", "1:05 AM", "1:06 AM", "1:07 AM", "1:08 AM", "1:09 AM", "1:10 AM", "1:11 AM", "1:12 AM", "1:13 AM", "1:14 AM", "1:15 AM", "1:16 AM", "1:17 AM", "1:18 AM", "1:19 AM", "1:20 AM", "1:21 AM", "1:22 AM", "1:23 AM", "1:24 AM", "1:25 AM", "1:26 AM", "1:27 AM", "1:28 AM", "1:29 AM", "1:30 AM", "1:31 AM", "1:32 AM", "1:33 AM", "1:34 AM", "1:35 AM", "1:36 AM", "1:37 AM", "1:38 AM", "1:39 AM", "1:40 AM", "1:41 AM", "1:42 AM", "1:43 AM", "1:44 AM", "1:45 AM", "1:46 AM", "1:47 AM", "1:48 AM", "1:49 AM", "1:50 AM", "1:51 AM", "1:52 AM", "1:53 AM", "1:54 AM", "1:55 AM", "1:56 AM", "1:57 AM", "1:58 AM", "1:59 AM", "2:00 AM", "2:01 AM", "2:02 AM", "2:03 AM", "2:04 AM", "2:05 AM", "2:06 AM", "2:07 AM", "2:08 AM", "2:09 AM", "2:10 AM", "2:11 AM", "2:12 AM", "2:13 AM", "2:14 AM", "2:15 AM", "2:16 AM", "2:17 AM", "2:18 AM", "2:19 AM", "2:20 AM", "2:21 AM", "2:22 AM", "2:23 AM", "2:24 AM", "2:25 AM", "2:26 AM", "2:27 AM", "2:28 AM", "2:29 AM", "2:30 AM", "2:31 AM", "2:32 AM", "2:33 AM", "2:34 AM", "2:35 AM", "2:36 AM", "2:37 AM", "2:38 AM", "2:39 AM", "2:40 AM", "2:41 AM", "2:42 AM", "2:43 AM", "2:44 AM", "2:45 AM", "2:46 AM", "2:47 AM", "2:48 AM", "2:49 AM", "2:50 AM", "2:51 AM", "2:52 AM", "2:53 AM", "2:54 AM", "2:55 AM", "2:56 AM", "2:57 AM", "2:58 AM", "2:59 AM", "3:00 AM", "3:01 AM", "3:02 AM", "3:03 AM", "3:04 AM", "3:05 AM", "3:06 AM", "3:07 AM", "3:08 AM", "3:09 AM", "3:10 AM", "3:11 AM", "3:12 AM", "3:13 AM", "3:14 AM", "3:15 AM", "3:16 AM", "3:17 AM", "3:18 AM", "3:19 AM", "3:20 AM", "3:21 AM", "3:22 AM", "3:23 AM", "3:24 AM", "3:25 AM", "3:26 AM", "3:27 AM", "3:28 AM", "3:29 AM", "3:30 AM", "3:31 AM", "3:32 AM", "3:33 AM", "3:34 AM", "3:35 AM", "3:36 AM", "3:37 AM", "3:38 AM", "3:39 AM", "3:40 AM", "3:41 AM", "3:42 AM", "3:43 AM", "3:44 AM", "3:45 AM", "3:46 AM", "3:47 AM", "3:48 AM", "3:49 AM", "3:50 AM", "3:51 AM", "3:52 AM", "3:53 AM", "3:54 AM", "3:55 AM", "3:56 AM", "3:57 AM", "3:58 AM", "3:59 AM", "4:00 AM", "4:01 AM", "4:02 AM", "4:03 AM", "4:04 AM", "4:05 AM", "4:06 AM", "4:07 AM", "4:08 AM", "4:09 AM", "4:10 AM", "4:11 AM", "4:12 AM", "4:13 AM", "4:14 AM", "4:15 AM", "4:16 AM", "4:17 AM", "4:18 AM", "4:19 AM", "4:20 AM", "4:21 AM", "4:22 AM", "4:23 AM", "4:24 AM", "4:25 AM", "4:26 AM", "4:27 AM", "4:28 AM", "4:29 AM", "4:30 AM", "4:31 AM", "4:32 AM", "4:33 AM", "4:34 AM", "4:35 AM", "4:36 AM", "4:37 AM", "4:38 AM", "4:39 AM", "4:40 AM", "4:41 AM", "4:42 AM", "4:43 AM", "4:44 AM", "4:45 AM", "4:46 AM", "4:47 AM", "4:48 AM", "4:49 AM", "4:50 AM", "4:51 AM", "4:52 AM", "4:53 AM", "4:54 AM", "4:55 AM", "4:56 AM", "4:57 AM", "4:58 AM", "4:59 AM", "5:00 AM", "5:01 AM", "5:02 AM", "5:03 AM", "5:04 AM", "5:05 AM", "5:06 AM", "5:07 AM", "5:08 AM", "5:09 AM", "5:10 AM", "5:11 AM", "5:12 AM", "5:13 AM", "5:14 AM", "5:15 AM", "5:16 AM", "5:17 AM", "5:18 AM", "5:19 AM", "5:20 AM", "5:21 AM", "5:22 AM", "5:23 AM", "5:24 AM", "5:25 AM", "5:26 AM", "5:27 AM", "5:28 AM", "5:29 AM", "5:30 AM", "5:31 AM", "5:32 AM", "5:33 AM", "5:34 AM", "5:35 AM", "5:36 AM", "5:37 AM", "5:38 AM", "5:39 AM", "5:40 AM", "5:41 AM", "5:42 AM", "5:43 AM", "5:44 AM", "5:45 AM", "5:46 AM", "5:47 AM", "5:48 AM", "5:49 AM", "5:50 AM", "5:51 AM", "5:52 AM", "5:53 AM", "5:54 AM", "5:55 AM", "5:56 AM", "5:57 AM", "5:58 AM", "5:59 AM", "6:00 AM", "6:01 AM", "6:02 AM", "6:03 AM", "6:04 AM", "6:05 AM", "6:06 AM", "6:07 AM", "6:08 AM", "6:09 AM", "6:10 AM", "6:11 AM", "6:12 AM", "6:13 AM", "6:14 AM", "6:15 AM", "6:16 AM", "6:17 AM", "6:18 AM", "6:19 AM", "6:20 AM", "6:21 AM", "6:22 AM", "6:23 AM", "6:24 AM", "6:25 AM", "6:26 AM", "6:27 AM", "6:28 AM", "6:29 AM", "6:30 AM", "6:31 AM", "6:32 AM", "6:33 AM", "6:34 AM", "6:35 AM", "6:36 AM", "6:37 AM", "6:38 AM", "6:39 AM", "6:40 AM", "6:41 AM", "6:42 AM", "6:43 AM", "6:44 AM", "6:45 AM", "6:46 AM", "6:47 AM", "6:48 AM", "6:49 AM", "6:50 AM", "6:51 AM", "6:52 AM", "6:53 AM", "6:54 AM", "6:55 AM", "6:56 AM", "6:57 AM", "6:58 AM", "6:59 AM", "7:00 AM", "7:01 AM", "7:02 AM", "7:03 AM", "7:04 AM", "7:05 AM", "7:06 AM", "7:07 AM", "7:08 AM", "7:09 AM", "7:10 AM", "7:11 AM", "7:12 AM", "7:13 AM", "7:14 AM", "7:15 AM", "7:16 AM", "7:17 AM", "7:18 AM", "7:19 AM", "7:20 AM", "7:21 AM", "7:22 AM", "7:23 AM", "7:24 AM", "7:25 AM", "7:26 AM", "7:27 AM", "7:28 AM", "7:29 AM", "7:30 AM", "7:31 AM", "7:32 AM", "7:33 AM", "7:34 AM", "7:35 AM", "7:36 AM", "7:37 AM", "7:38 AM", "7:39 AM", "7:40 AM", "7:41 AM", "7:42 AM", "7:43 AM", "7:44 AM", "7:45 AM", "7:46 AM", "7:47 AM", "7:48 AM", "7:49 AM", "7:50 AM", "7:51 AM", "7:52 AM", "7:53 AM", "7:54 AM", "7:55 AM", "7:56 AM", "7:57 AM", "7:58 AM", "7:59 AM", "8:00 AM", "8:01 AM", "8:02 AM", "8:03 AM", "8:04 AM", "8:05 AM", "8:06 AM", "8:07 AM", "8:08 AM", "8:09 AM", "8:10 AM", "8:11 AM", "8:12 AM", "8:13 AM", "8:14 AM", "8:15 AM", "8:16 AM", "8:17 AM", "8:18 AM", "8:19 AM", "8:20 AM", "8:21 AM", "8:22 AM", "8:23 AM", "8:24 AM", "8:25 AM", "8:26 AM", "8:27 AM", "8:28 AM", "8:29 AM", "8:30 AM", "8:31 AM", "8:32 AM", "8:33 AM", "8:34 AM", "8:35 AM", "8:36 AM", "8:37 AM", "8:38 AM", "8:39 AM", "8:40 AM", "8:41 AM", "8:42 AM", "8:43 AM", "8:44 AM", "8:45 AM", "8:46 AM", "8:47 AM", "8:48 AM", "8:49 AM", "8:50 AM", "8:51 AM", "8:52 AM", "8:53 AM", "8:54 AM", "8:55 AM", "8:56 AM", "8:57 AM", "8:58 AM", "8:59 AM", "9:00 AM", "9:01 AM", "9:02 AM", "9:03 AM", "9:04 AM", "9:05 AM", "9:06 AM", "9:07 AM", "9:08 AM", "9:09 AM", "9:10 AM", "9:11 AM", "9:12 AM", "9:13 AM", "9:14 AM", "9:15 AM", "9:16 AM", "9:17 AM", "9:18 AM", "9:19 AM", "9:20 AM", "9:21 AM", "9:22 AM", "9:23 AM", "9:24 AM", "9:25 AM", "9:26 AM", "9:27 AM", "9:28 AM", "9:29 AM", "9:30 AM", "9:31 AM", "9:32 AM", "9:33 AM", "9:34 AM", "9:35 AM", "9:36 AM", "9:37 AM", "9:38 AM", "9:39 AM", "9:40 AM", "9:41 AM", "9:42 AM", "9:43 AM", "9:44 AM", "9:45 AM", "9:46 AM", "9:47 AM", "9:48 AM", "9:49 AM", "9:50 AM", "9:51 AM", "9:52 AM", "9:53 AM", "9:54 AM", "9:55 AM", "9:56 AM", "9:57 AM", "9:58 AM", "9:59 AM", "10:00 AM", "10:01 AM", "10:02 AM", "10:03 AM", "10:04 AM", "10:05 AM", "10:06 AM", "10:07 AM", "10:08 AM", "10:09 AM", "10:10 AM", "10:11 AM", "10:12 AM", "10:13 AM", "10:14 AM", "10:15 AM", "10:16 AM", "10:17 AM", "10:18 AM", "10:19 AM", "10:20 AM", "10:21 AM", "10:22 AM", "10:23 AM", "10:24 AM", "10:25 AM", "10:26 AM", "10:27 AM", "10:28 AM", "10:29 AM", "10:30 AM", "10:31 AM", "10:32 AM", "10:33 AM", "10:34 AM", "10:35 AM", "10:36 AM", "10:37 AM", "10:38 AM", "10:39 AM", "10:40 AM", "10:41 AM", "10:42 AM", "10:43 AM", "10:44 AM", "10:45 AM", "10:46 AM", "10:47 AM", "10:48 AM", "10:49 AM", "10:50 AM", "10:51 AM", "10:52 AM", "10:53 AM", "10:54 AM", "10:55 AM", "10:56 AM", "10:57 AM", "10:58 AM", "10:59 AM", "11:00 AM", "11:01 AM", "11:02 AM", "11:03 AM", "11:04 AM", "11:05 AM", "11:06 AM", "11:07 AM", "11:08 AM", "11:09 AM", "11:10 AM", "11:11 AM", "11:12 AM", "11:13 AM", "11:14 AM", "11:15 AM", "11:16 AM", "11:17 AM", "11:18 AM", "11:19 AM", "11:20 AM", "11:21 AM", "11:22 AM", "11:23 AM", "11:24 AM", "11:25 AM", "11:26 AM", "11:27 AM", "11:28 AM", "11:29 AM", "11:30 AM", "11:31 AM", "11:32 AM", "11:33 AM", "11:34 AM", "11:35 AM", "11:36 AM", "11:37 AM", "11:38 AM", "11:39 AM", "11:40 AM", "11:41 AM", "11:42 AM", "11:43 AM", "11:44 AM", "11:45 AM", "11:46 AM", "11:47 AM", "11:48 AM", "11:49 AM", "11:50 AM", "11:51 AM", "11:52 AM", "11:53 AM", "11:54 AM", "11:55 AM", "11:56 AM", "11:57 AM", "11:58 AM", "11:59 AM", "12:00 AM", "12:01 AM", "12:02 AM", "12:03 AM", "12:04 AM", "12:05 AM", "12:06 AM", "12:07 AM", "12:08 AM", "12:09 AM", "12:10 AM", "12:11 AM", "12:12 AM", "12:13 AM", "12:14 AM", "12:15 AM", "12:16 AM", "12:17 AM", "12:18 AM", "12:19 AM", "12:20 AM", "12:21 AM", "12:22 AM", "12:23 AM", "12:24 AM", "12:25 AM", "12:26 AM", "12:27 AM", "12:28 AM", "12:29 AM", "12:30 AM", "12:31 AM", "12:32 AM", "12:33 AM", "12:34 AM", "12:35 AM", "12:36 AM", "12:37 AM", "12:38 AM", "12:39 AM", "12:40 AM", "12:41 AM", "12:42 AM", "12:43 AM", "12:44 AM", "12:45 AM", "12:46 AM", "12:47 AM", "12:48 AM", "12:49 AM", "12:50 AM", "12:51 AM", "12:52 AM", "12:53 AM", "12:54 AM", "12:55 AM", "12:56 AM", "12:57 AM", "12:58 AM", "12:59 AM" };
        String[] AlarmPM = { "1:00 PM", "1:01 PM", "1:02 PM", "1:03 PM", "1:04 PM", "1:05 PM", "1:06 PM", "1:07 PM", "1:08 PM", "1:09 PM", "1:10 PM", "1:11 PM", "1:12 PM", "1:13 PM", "1:14 PM", "1:15 PM", "1:16 PM", "1:17 PM", "1:18 PM", "1:19 PM", "1:20 PM", "1:21 PM", "1:22 PM", "1:23 PM", "1:24 PM", "1:25 PM", "1:26 PM", "1:27 PM", "1:28 PM", "1:29 PM", "1:30 PM", "1:31 PM", "1:32 PM", "1:33 PM", "1:34 PM", "1:35 PM", "1:36 PM", "1:37 PM", "1:38 PM", "1:39 PM", "1:40 PM", "1:41 PM", "1:42 PM", "1:43 PM", "1:44 PM", "1:45 PM", "1:46 PM", "1:47 PM", "1:48 PM", "1:49 PM", "1:50 PM", "1:51 PM", "1:52 PM", "1:53 PM", "1:54 PM", "1:55 PM", "1:56 PM", "1:57 PM", "1:58 PM", "1:59 PM", "2:00 PM", "2:01 PM", "2:02 PM", "2:03 PM", "2:04 PM", "2:05 PM", "2:06 PM", "2:07 PM", "2:08 PM", "2:09 PM", "2:10 PM", "2:11 PM", "2:12 PM", "2:13 PM", "2:14 PM", "2:15 PM", "2:16 PM", "2:17 PM", "2:18 PM", "2:19 PM", "2:20 PM", "2:21 PM", "2:22 PM", "2:23 PM", "2:24 PM", "2:25 PM", "2:26 PM", "2:27 PM", "2:28 PM", "2:29 PM", "2:30 PM", "2:31 PM", "2:32 PM", "2:33 PM", "2:34 PM", "2:35 PM", "2:36 PM", "2:37 PM", "2:38 PM", "2:39 PM", "2:40 PM", "2:41 PM", "2:42 PM", "2:43 PM", "2:44 PM", "2:45 PM", "2:46 PM", "2:47 PM", "2:48 PM", "2:49 PM", "2:50 PM", "2:51 PM", "2:52 PM", "2:53 PM", "2:54 PM", "2:55 PM", "2:56 PM", "2:57 PM", "2:58 PM", "2:59 PM", "3:00 PM", "3:01 PM", "3:02 PM", "3:03 PM", "3:04 PM", "3:05 PM", "3:06 PM", "3:07 PM", "3:08 PM", "3:09 PM", "3:10 PM", "3:11 PM", "3:12 PM", "3:13 PM", "3:14 PM", "3:15 PM", "3:16 PM", "3:17 PM", "3:18 PM", "3:19 PM", "3:20 PM", "3:21 PM", "3:22 PM", "3:23 PM", "3:24 PM", "3:25 PM", "3:26 PM", "3:27 PM", "3:28 PM", "3:29 PM", "3:30 PM", "3:31 PM", "3:32 PM", "3:33 PM", "3:34 PM", "3:35 PM", "3:36 PM", "3:37 PM", "3:38 PM", "3:39 PM", "3:40 PM", "3:41 PM", "3:42 PM", "3:43 PM", "3:44 PM", "3:45 PM", "3:46 PM", "3:47 PM", "3:48 PM", "3:49 PM", "3:50 PM", "3:51 PM", "3:52 PM", "3:53 PM", "3:54 PM", "3:55 PM", "3:56 PM", "3:57 PM", "3:58 PM", "3:59 PM", "4:00 PM", "4:01 PM", "4:02 PM", "4:03 PM", "4:04 PM", "4:05 PM", "4:06 PM", "4:07 PM", "4:08 PM", "4:09 PM", "4:10 PM", "4:11 PM", "4:12 PM", "4:13 PM", "4:14 PM", "4:15 PM", "4:16 PM", "4:17 PM", "4:18 PM", "4:19 PM", "4:20 PM", "4:21 PM", "4:22 PM", "4:23 PM", "4:24 PM", "4:25 PM", "4:26 PM", "4:27 PM", "4:28 PM", "4:29 PM", "4:30 PM", "4:31 PM", "4:32 PM", "4:33 PM", "4:34 PM", "4:35 PM", "4:36 PM", "4:37 PM", "4:38 PM", "4:39 PM", "4:40 PM", "4:41 PM", "4:42 PM", "4:43 PM", "4:44 PM", "4:45 PM", "4:46 PM", "4:47 PM", "4:48 PM", "4:49 PM", "4:50 PM", "4:51 PM", "4:52 PM", "4:53 PM", "4:54 PM", "4:55 PM", "4:56 PM", "4:57 PM", "4:58 PM", "4:59 PM", "5:00 PM", "5:01 PM", "5:02 PM", "5:03 PM", "5:04 PM", "5:05 PM", "5:06 PM", "5:07 PM", "5:08 PM", "5:09 PM", "5:10 PM", "5:11 PM", "5:12 PM", "5:13 PM", "5:14 PM", "5:15 PM", "5:16 PM", "5:17 PM", "5:18 PM", "5:19 PM", "5:20 PM", "5:21 PM", "5:22 PM", "5:23 PM", "5:24 PM", "5:25 PM", "5:26 PM", "5:27 PM", "5:28 PM", "5:29 PM", "5:30 PM", "5:31 PM", "5:32 PM", "5:33 PM", "5:34 PM", "5:35 PM", "5:36 PM", "5:37 PM", "5:38 PM", "5:39 PM", "5:40 PM", "5:41 PM", "5:42 PM", "5:43 PM", "5:44 PM", "5:45 PM", "5:46 PM", "5:47 PM", "5:48 PM", "5:49 PM", "5:50 PM", "5:51 PM", "5:52 PM", "5:53 PM", "5:54 PM", "5:55 PM", "5:56 PM", "5:57 PM", "5:58 PM", "5:59 PM", "6:00 PM", "6:01 PM", "6:02 PM", "6:03 PM", "6:04 PM", "6:05 PM", "6:06 PM", "6:07 PM", "6:08 PM", "6:09 PM", "6:10 PM", "6:11 PM", "6:12 PM", "6:13 PM", "6:14 PM", "6:15 PM", "6:16 PM", "6:17 PM", "6:18 PM", "6:19 PM", "6:20 PM", "6:21 PM", "6:22 PM", "6:23 PM", "6:24 PM", "6:25 PM", "6:26 PM", "6:27 PM", "6:28 PM", "6:29 PM", "6:30 PM", "6:31 PM", "6:32 PM", "6:33 PM", "6:34 PM", "6:35 PM", "6:36 PM", "6:37 PM", "6:38 PM", "6:39 PM", "6:40 PM", "6:41 PM", "6:42 PM", "6:43 PM", "6:44 PM", "6:45 PM", "6:46 PM", "6:47 PM", "6:48 PM", "6:49 PM", "6:50 PM", "6:51 PM", "6:52 PM", "6:53 PM", "6:54 PM", "6:55 PM", "6:56 PM", "6:57 PM", "6:58 PM", "6:59 PM", "7:00 PM", "7:01 PM", "7:02 PM", "7:03 PM", "7:04 PM", "7:05 PM", "7:06 PM", "7:07 PM", "7:08 PM", "7:09 PM", "7:10 PM", "7:11 PM", "7:12 PM", "7:13 PM", "7:14 PM", "7:15 PM", "7:16 PM", "7:17 PM", "7:18 PM", "7:19 PM", "7:20 PM", "7:21 PM", "7:22 PM", "7:23 PM", "7:24 PM", "7:25 PM", "7:26 PM", "7:27 PM", "7:28 PM", "7:29 PM", "7:30 PM", "7:31 PM", "7:32 PM", "7:33 PM", "7:34 PM", "7:35 PM", "7:36 PM", "7:37 PM", "7:38 PM", "7:39 PM", "7:40 PM", "7:41 PM", "7:42 PM", "7:43 PM", "7:44 PM", "7:45 PM", "7:46 PM", "7:47 PM", "7:48 PM", "7:49 PM", "7:50 PM", "7:51 PM", "7:52 PM", "7:53 PM", "7:54 PM", "7:55 PM", "7:56 PM", "7:57 PM", "7:58 PM", "7:59 PM", "8:00 PM", "8:01 PM", "8:02 PM", "8:03 PM", "8:04 PM", "8:05 PM", "8:06 PM", "8:07 PM", "8:08 PM", "8:09 PM", "8:10 PM", "8:11 PM", "8:12 PM", "8:13 PM", "8:14 PM", "8:15 PM", "8:16 PM", "8:17 PM", "8:18 PM", "8:19 PM", "8:20 PM", "8:21 PM", "8:22 PM", "8:23 PM", "8:24 PM", "8:25 PM", "8:26 PM", "8:27 PM", "8:28 PM", "8:29 PM", "8:30 PM", "8:31 PM", "8:32 PM", "8:33 PM", "8:34 PM", "8:35 PM", "8:36 PM", "8:37 PM", "8:38 PM", "8:39 PM", "8:40 PM", "8:41 PM", "8:42 PM", "8:43 PM", "8:44 PM", "8:45 PM", "8:46 PM", "8:47 PM", "8:48 PM", "8:49 PM", "8:50 PM", "8:51 PM", "8:52 PM", "8:53 PM", "8:54 PM", "8:55 PM", "8:56 PM", "8:57 PM", "8:58 PM", "8:59 PM", "9:00 PM", "9:01 PM", "9:02 PM", "9:03 PM", "9:04 PM", "9:05 PM", "9:06 PM", "9:07 PM", "9:08 PM", "9:09 PM", "9:10 PM", "9:11 PM", "9:12 PM", "9:13 PM", "9:14 PM", "9:15 PM", "9:16 PM", "9:17 PM", "9:18 PM", "9:19 PM", "9:20 PM", "9:21 PM", "9:22 PM", "9:23 PM", "9:24 PM", "9:25 PM", "9:26 PM", "9:27 PM", "9:28 PM", "9:29 PM", "9:30 PM", "9:31 PM", "9:32 PM", "9:33 PM", "9:34 PM", "9:35 PM", "9:36 PM", "9:37 PM", "9:38 PM", "9:39 PM", "9:40 PM", "9:41 PM", "9:42 PM", "9:43 PM", "9:44 PM", "9:45 PM", "9:46 PM", "9:47 PM", "9:48 PM", "9:49 PM", "9:50 PM", "9:51 PM", "9:52 PM", "9:53 PM", "9:54 PM", "9:55 PM", "9:56 PM", "9:57 PM", "9:58 PM", "9:59 PM", "10:00 PM", "10:01 PM", "10:02 PM", "10:03 PM", "10:04 PM", "10:05 PM", "10:06 PM", "10:07 PM", "10:08 PM", "10:09 PM", "10:10 PM", "10:11 PM", "10:12 PM", "10:13 PM", "10:14 PM", "10:15 PM", "10:16 PM", "10:17 PM", "10:18 PM", "10:19 PM", "10:20 PM", "10:21 PM", "10:22 PM", "10:23 PM", "10:24 PM", "10:25 PM", "10:26 PM", "10:27 PM", "10:28 PM", "10:29 PM", "10:30 PM", "10:31 PM", "10:32 PM", "10:33 PM", "10:34 PM", "10:35 PM", "10:36 PM", "10:37 PM", "10:38 PM", "10:39 PM", "10:40 PM", "10:41 PM", "10:42 PM", "10:43 PM", "10:44 PM", "10:45 PM", "10:46 PM", "10:47 PM", "10:48 PM", "10:49 PM", "10:50 PM", "10:51 PM", "10:52 PM", "10:53 PM", "10:54 PM", "10:55 PM", "10:56 PM", "10:57 PM", "10:58 PM", "10:59 PM", "11:00 PM", "11:01 PM", "11:02 PM", "11:03 PM", "11:04 PM", "11:05 PM", "11:06 PM", "11:07 PM", "11:08 PM", "11:09 PM", "11:10 PM", "11:11 PM", "11:12 PM", "11:13 PM", "11:14 PM", "11:15 PM", "11:16 PM", "11:17 PM", "11:18 PM", "11:19 PM", "11:20 PM", "11:21 PM", "11:22 PM", "11:23 PM", "11:24 PM", "11:25 PM", "11:26 PM", "11:27 PM", "11:28 PM", "11:29 PM", "11:30 PM", "11:31 PM", "11:32 PM", "11:33 PM", "11:34 PM", "11:35 PM", "11:36 PM", "11:37 PM", "11:38 PM", "11:39 PM", "11:40 PM", "11:41 PM", "11:42 PM", "11:43 PM", "11:44 PM", "11:45 PM", "11:46 PM", "11:47 PM", "11:48 PM", "11:49 PM", "11:50 PM", "11:51 PM", "11:52 PM", "11:53 PM", "11:54 PM", "11:55 PM", "11:56 PM", "11:57 PM", "11:58 PM", "11:59 PM", "12:00 PM", "12:01 PM", "12:02 PM", "12:03 PM", "12:04 PM", "12:05 PM", "12:06 PM", "12:07 PM", "12:08 PM", "12:09 PM", "12:10 PM", "12:11 PM", "12:12 PM", "12:13 PM", "12:14 PM", "12:15 PM", "12:16 PM", "12:17 PM", "12:18 PM", "12:19 PM", "12:20 PM", "12:21 PM", "12:22 PM", "12:23 PM", "12:24 PM", "12:25 PM", "12:26 PM", "12:27 PM", "12:28 PM", "12:29 PM", "12:30 PM", "12:31 PM", "12:32 PM", "12:33 PM", "12:34 PM", "12:35 PM", "12:36 PM", "12:37 PM", "12:38 PM", "12:39 PM", "12:40 PM", "12:41 PM", "12:42 PM", "12:43 PM", "12:44 PM", "12:45 PM", "12:46 PM", "12:47 PM", "12:48 PM", "12:49 PM", "12:50 PM", "12:51 PM", "12:52 PM", "12:53 PM", "12:54 PM", "12:55 PM", "12:56 PM", "12:57 PM", "12:58 PM", "12:59 PM" };
        string[] AlarmCommands = { "set the alarm", "clear the alarm", "what time is the alarm" };
        String AlarmTime;
        System.Timers.Timer AlarmTimer = new System.Timers.Timer();
        //System.Timers.Timer AlarmTimer;        
        const string ServiceResourceId = "https://outlook.office365.com";
        static readonly Uri ServiceEndpointUri = new Uri("https://outlook.office365.com/ews/odata");
        Service service = new Service("cl", "exampleCo-exampleApp-1");
        CalendarService myService = new CalendarService("exampleCo-exampleApp-1");
        Service s = new Service("tl", "exampleCo-exampleApp-1");
        
        

        // Do not make static in Web apps; store it in session or in a cookie instead
        static string _lastLoggedInUser;
        static DiscoveryContext _discoveryContext;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            #region Pipr Grammar
            Piper.SpeakAsyncCancelAll();
            Piper.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);            

            var dWords = from i in context.Words select i.lemma;
            var dCommands = from i in context.DefaultCommands select i.command;
            var sCommands = from i in context.ShellCommands select i;
            var socCommands = from i in context.SocialCommands select i;
           
            
            /*foreach (string word in dWords)
            {
                string w = word.Replace(" ", "-");
                words.Add(w);
            }*/

            
            foreach (var command in dCommands)
            {
                defaultCommands.Add(command);
                PiperBox.Items.Add(command);
            }

            foreach (var command in sCommands)
            {
                shellCommands.Add(command.command);
                shellResponse.Add(command.response);
                shellLocation.Add(command.location);
            }

            foreach (var command in socCommands)
            {
                socialCommands.Add(command.command);
                socialResponse.Add(command.response);
            }

            Choices searching = new Choices(words.ToArray());
            GrammarBuilder lookingService = new GrammarBuilder("search");
            //GrammarBuilder defineService = new GrammarBuilder("define");
            lookingService.Append(searching);

            goodMorning();

            Piper.Speak("Hello " + Settings.Default.User + ", I am starting up and initiating my system.  Please allow me a minute to load the necessary files.");

            Piper.Speak("Loading my dictionaries now.");
            try
            { wordscommandgrammar = new Grammar(lookingService); recEngine.LoadGrammar(wordscommandgrammar); }
            catch { Piper.SpeakAsync("I've detected an in valid entry in your dictionary words, possibly a blank line. dictionary words will cease to work until it is fixed."); }

            Piper.SpeakAsync("Loading my default, shell and social Commands now");
            try
            { defaultcommandgrammar = new Grammar(new GrammarBuilder(new Choices(defaultCommands.ToArray()))); recEngine.LoadGrammarAsync(defaultcommandgrammar); }
            catch { Piper.SpeakAsync("I've detected an in valid entry in your dictionary words, possibly a blank line. dictionary words will cease to work until it is fixed."); }
            try
            { shellcommandgrammar = new Grammar(new GrammarBuilder(new Choices(shellCommands.ToArray()))); recEngine.LoadGrammarAsync(shellcommandgrammar); }
            catch { Piper.SpeakAsync("I've detected an in valid entry in your shell commands, possibly a blank line. Shell commands will cease to work until it is fixed."); }            
            try
            { socialcommandgrammar = new Grammar(new GrammarBuilder(new Choices(socialCommands.ToArray()))); recEngine.LoadGrammarAsync(socialcommandgrammar); }
            catch { Piper.SpeakAsync("I've detected an in valid entry in your shell commands, possibly a blank line. Shell commands will cease to work until it is fixed."); }            
            try
            { alarmclockgrammar = new Grammar(new GrammarBuilder(new Choices(AlarmCommands))); recEngine.LoadGrammarAsync(alarmclockgrammar); }
            catch{ Piper.SpeakAsync("I've detected an in valid entry in your alarm commands, possibly a blank line. Shell commands will cease to work until it is fixed."); }
            
            try { timesetgrammarAM = new Grammar(new GrammarBuilder(new Choices(AlarmAM))); recEngine.LoadGrammarAsync(timesetgrammarAM); }
            catch { Piper.SpeakAsync("I've detected an in valid entry in your time commands, possibly a blank line. Shell commands will cease to work until it is fixed."); }

            try { timesetgrammarPM = new Grammar(new GrammarBuilder(new Choices(AlarmPM))); recEngine.LoadGrammarAsync(timesetgrammarPM); }
            catch { Piper.SpeakAsync("I've detected an in valid entry in your time commands, possibly a blank line. Shell commands will cease to work until it is fixed."); }           

            recEngine.SetInputToDefaultAudioDevice();
            recEngine.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(AlarmAM)))); //Loads AM times for our alarm feature
            recEngine.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(AlarmPM)))); //Loads PM times for our alarm feature *Might consider combining them
            //recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(dictionaryWords_Recognized);
            recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recEngine_SpeechRecognized);
            recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Shell_SpeechRecognized);
            recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Social_SpeechRecognized);
            recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(AlarmClock_SpeechRecognized);
            recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(TimeSet_SpeechRecognized);
            recEngine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(recEngine_AudioLevelUpdated);
            //recognizer.StateChanged += new EventHandler<System.Speech.Recognition.StateChangedEventArgs>(recognizer_StateChanged);
            //recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>();

            if (Settings.Default.AClockEnbl == true) { AlarmTimer.Enabled = true; }
            
            recEngine.RecognizeAsync(RecognizeMode.Multiple);

            Piper.Speak("All my system files have been loaded and I am online.  Welcome back " + Settings.Default.User + ", What can I do for you today");
           
            #endregion
        }      
               

        #region PortScanner

        private void PortScanner()
        {
            MainWindow w = new MainWindow();

            int start = 70;
            int end = 80;
            string ipString;
            string txtHost = "www.snapstudent.com";
            ipString = Dns.GetHostEntry(txtHost).AddressList[0].ToString();
            
            TcpClient asd = new TcpClient();
            // MessageBox.Show(ipString);     
            IPAddress address = IPAddress.Parse(ipString);
            for (int i = start; i <= end; i++)
            {
                try
                {
                    asd.SendTimeout = 3000;
                    asd.ReceiveTimeout = 3000;
                    asd.Connect(address, i);

                    if (asd.Connected)
                    {
                        w.Piper.SpeakAsync("Port " + i + " is open");
                    }
                }
                catch
                {
                    w.Piper.SpeakAsync("Port " + i + " is closed");
                }
            }
        }
       
        private void PortScanner2()
        {
            
            var py = Python.CreateEngine();
            var scope = py.CreateScope();
            scope.SetVariable("value", 43);
            try
            {
                //dynamic PYthon_Script = py.UseFile("script.py");
                py.ExecuteFile("script.py");
                
            }
            catch (Exception ex)
            {
                Piper.SpeakAsync(
                   "Oops! We couldn't execute the script because of an exception: " + ex.Message);
                //Piper.SpeakAsyncCancelAll();
            }
            
            
        }

        private void PortScanner3()
        {
            ProcessStartInfo start = new ProcessStartInfo();
            try
            {
                start.FileName = "script.py";
                //start.Arguments = string.Format("{0} {1}", cmd, args);
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        Piper.SpeakAsync(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Piper.SpeakAsync("Could not execute Python script due to an exception." + ex);
            }
        }
        #endregion

        #region Directories
        private void LoadDirectory()
        {
            PiperBox.Items.Clear();

            switch (QEvent)
            {
                case "music directory":
                    Piper.SpeakAsync("Sure, I would be glad to.  You should see your music directory on your screen now, Chad.");
                    string[] files = Directory.GetFiles(BrowseDirectory, "*", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        PiperBox.Items.Add(file.Replace(BrowseDirectory, ""));
                    }
                    break;

                case "video directory":
                    Piper.SpeakAsync("Sure, I would be glad to.  You should see your video directory on your screen now, Chad.");
                    files = Directory.GetFiles(BrowseDirectory, "*", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        PiperBox.Items.Add(file.Replace(BrowseDirectory, ""));
                    }
                    break;

                case "picture directory":
                    Piper.SpeakAsync("Sure, I would be glad to.  You should see your picture directory on your screen now, Chad.");
                    files = Directory.GetFiles(BrowseDirectory, "*", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        PiperBox.Items.Add(file.Replace(BrowseDirectory, ""));
                    }
                    break;

                case "load directory":
                    Piper.SpeakAsync("Sure, I would be glad to.  You should see your directory on your screen now, Chad.");
                    files = Directory.GetFiles(BrowseDirectory, "*", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        PiperBox.Items.Add(file.Replace(BrowseDirectory + "\\", ""));
                    }
                    break;


            }
        }

        #endregion

        #region recEngine Settings
       

        void recEngine_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
            //pbMicrophoneLevel.Value = e.AudioLevel;
        }

        private void tmrSpeech_Tick(object sender, EventArgs e)
        {
            if (recTimeOut == 10)
            {
                recEngine.RecognizeAsyncCancel();
            }
            else if (recTimeOut == 11)
            {
                startlistening.RecognizeAsync(RecognizeMode.Multiple);
                //tmrSpeech.Stop();
                recTimeOut = 0;
            }
            recTimeOut += 1;
        }


        #endregion

        #region Speech Recognized

        private void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {                   
           
            string speech = e.Result.Text;
            string startSpeech = e.Result.Words[0].Text;

            if (startSpeech == "search")
            {
                string searchTerm = e.Result.Words[1].Text;
                Piper.SpeakAsync("I am searching for-" + searchTerm);
                Process.Start("http://google.com/search?q=" + searchTerm);
                searchTerm = searchTerm.Replace("-", " ");
                getDefinition(searchTerm);
            }
            else
            {
                DateTime myBirthday = DateTime.Parse("03/23/1973");
                DateTime sarahBirthday = DateTime.Parse("04/04/1976");

                int randNum = rand.Next(1, 10);
                //string speech = e.Result.Text;

                ConvoBox.Items.Add("User: " + speech);

                switch (speech)
                {
                    case "exit":
                        this.Close();
                        break;

                    case "who are you":
                    case "what is your name":
                        if (randNum < 2) { Piper.Speak("My name is Piper"); }
                        else if (randNum > 2 || randNum < 7) { Piper.Speak("My name is Piper.  How are you doing " + Settings.Default.User); }
                        else { Piper.Speak("Piper"); }                       
                        break;
                    
                    /*case "log into my facebook":
                    case "Log on to my facebook":
                        System.Diagnostics.Process.Start("https://www.facebook.com/");
                        Piper.Speak("Creating a secure connection to Facebook");
                        SendKeys.Send("{b}{r}{a}{n}{t}{o}{n}{@}{g}{o}{l}{d}{m}{a}{i}{l}{.}{e}{t}{s}{u}{.}{e}{d}{u}");
                        SendKeys.Send("{TAB}");
                        SendKeys.Send("{.}{N}{E}{T}{f}{o}{r}{l}{i}{f}{e}");
                        SendKeys.Send("{ENTER}");
                        break;*/

                    case "check calendar":
                        checkGoogleCalendar();
                        break;

                    case "get tasks":
                        //getGoogleTasks();
                        break;

                    case "print my name":
                    case "what is my name":
                        Piper.SpeakAsync("Your name is " + Settings.Default.User);
                        break;
                                        
                    case "what is my daughters name":
                    case "daughters name":
                        Piper.SpeakAsync("Chloe");
                        break;

                    case "what is my girlfriends name":
                        Piper.SpeakAsync("Sarah Frost and from what I hear, she is hot!");
                        break;

                    case "are you weird":
                        Piper.SpeakAsync("No!  Sarah is weird!  hehehe");
                        break;
                    case "load email":
                    case "open email":
                        System.Diagnostics.Process.Start("http://goldmail.etsu.edu");

                        Piper.SpeakAsync("Right away");
                        break;

                    case "goodbye":
                    case "goodbye piper":
                        Piper.SpeakAsync("See you soon.");
                        Close();
                        break;

                    case "what is todays date":
                    case "what is the date today":
                        Piper.SpeakAsync("Todays date is " + DateTime.Today.ToString("MM-dd-yyyy"));
                        break;

                    case "what time is it":
                        string now = DateTime.Now.ToShortTimeString();
                        Piper.SpeakAsync("The time is " + now);
                        break;


                    #region File Directory
                    case "load music directory":
                        QEvent = "music directory";
                        BrowseDirectory = @"C:\Users\Sarah Frost\Music\";
                        LoadDirectory();

                        break;

                    case "load video directory":
                        QEvent = "video directory";
                        BrowseDirectory = @"C:\Users\Sarah Frost\Videos\";
                        LoadDirectory();
                        break;

                    case "load picture directory":
                        QEvent = "picture directory";
                        BrowseDirectory = @"C:\Users\Sarah Frost\Pictures\";
                        LoadDirectory();
                        break;

                    case "browse for directory":
                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            BrowseDirectory = fbd.SelectedPath;
                        }
                        QEvent = "load directory";
                        LoadDirectory();
                        break;
                    #endregion

                    #region Weather

                    case "what is todays weather":
                    case "hows the weather":
                        Piper.SpeakAsyncCancelAll();
                        getWeather();
                        Piper.SpeakAsync("The weather in " + Town + " is " + Condition + " at " + Temperature + " degrees.  There is a wind speed of "
                                            + WindSpeed + " miles per hour and a humidity level of " + Humidity + "percent.  What are your plans today?");
                        break;

                    case "whats tomorrows forecast":
                        getWeather();
                        Piper.SpeakAsync("It looks like tomorrow will be " + TFCond);
                        break;

                    #endregion

                    #region Port Scanner
                    case "run port scanner":
                        PortScanner2();

                        break;

                    case "port scanner":
                        Piper.SpeakAsync("Ok, chad.  I have loaded your Python 3.3 port scanner.  What c s v file would you like me to import?");
                        Thread.Sleep(10000);
                        Piper.Pause();
                        Thread.Sleep(3000);
                        Piper.Resume();
                        Piper.SpeakAsync("What Port would you like me to start with?");
                        Thread.Sleep(4000);
                        Piper.Pause();
                        Thread.Sleep(2000);
                        Piper.Resume();
                        Piper.SpeakAsync("What port would you like me to end with?");
                        Thread.Sleep(4000);
                        Piper.Pause();
                        Thread.Sleep(2000);
                        Piper.Resume();
                        Piper.SpeakAsync("Please wait while I can scan all ports from your c s v file!");
                        Thread.Sleep(4000);
                        Piper.Pause();
                        Thread.Sleep(7000);
                        Piper.Resume();
                        string readFile = File.ReadAllText(@"C:\Users\Sarah Frost\Documents\Visual Studio 2013\Projects\Pipr\Pipr\portscanOutput.txt").ToString();

                        PiperBox.Items.Add(readFile);

                        Piper.Speak(readFile);

                        //Piper.Speak(readFile);

                        Piper.SpeakAsync("That is all of the ports for the I P addresses that were imported.  Is there anything else I can do for you Chad?");

                        break;
                    #endregion

                    #region Love
                    case "how do you love":
                    case "how does a person love":
                    case "piper tell me about love":
                    case "what is love":
                        Piper.SpeakAsync("i dont know much about love because my neural network is in its beginning stages, but here is what I know.    "
                            + " Love is something that i cannot feel or take with me, however, true love is very hard to come by, and i would be jealous "
                            + "of that kind of love.  love is something you cannot ask for.  its a gift from God.     all i can say is have patience with "
                            + "those who love you.  Love is patient, love is kind.  It does not envy, it does not boast, it is not proud.  It does not "
                            + "dishonor others, it is not self-seeking, it is not easily angered, it keeps no record of wrongs.  Love does not delight in evil, "
                            + "but rejoices with the truth.  It always protects, always trusts, always hopes, always perseveres!    I hope that helps you. " + Settings.Default.User
                            + "is there anything else you would like to know?");
                        break;
                    #endregion

                    case "goodnight":
                    case "goodnight piper":
                        time = DateTime.Now;
                        if (time < dt9pm)
                        {
                            Piper.SpeakAsync("Its not your bedtime yet chad.  Why are you going to bed so early?  I thought you were in class until 10 pm");
                            Thread.Sleep(7000);
                        }
                        else
                        {
                            Piper.SpeakAsync("Goodnight chad, I will be here in the morning.  Remember to set my alarms!");
                        }

                        break;

                    case "show commands":
                    case "show me your commands":
                    case "show me your default commands":
                    case "piper show me your commands":
                        Piper.SpeakAsync("my commands are loading into the main screen.");
                        PiperBox.Items.Clear();
                        var c = from i in context.DefaultCommands orderby i.command select i.command;
                        foreach (var cmd in c)
                        {
                            PiperBox.Items.Add(cmd);
                        }

                        break;

                    case "show me your shell commands":
                        Piper.SpeakAsync("my shell commands are loading now.");
                        PiperBox.Items.Clear();
                        var sc = from i in context.ShellCommands orderby i.command select i;
                        foreach (var cmd in sc)
                        {
                            PiperBox.Items.Add(cmd.command);
                        }
                        break;

                    case "show me your social commands":
                        Piper.SpeakAsync("my social commands are loading now.");
                        PiperBox.Items.Clear();
                        var socCommands = from i in context.SocialCommands orderby i.command select i;
                        foreach (var cmd in socCommands)
                        {
                            PiperBox.Items.Add(cmd.command + " :        mi" + cmd.response);
                        }
                        break;

                    case "clear your screen":
                        PiperBox.Items.Clear();
                        break;

                    case "clear your conversation box":
                        ConvoBox.Items.Clear();
                        break;

                    #region Piper pause stop resume
                    case "hold on piper":
                        Piper.SpeakAsync("yes sir.");
                        Piper.Pause();
                        break;

                    case "piper stop talking":
                        Piper.SpeakAsyncCancelAll();
                        Piper.SpeakAsync("Yes chad,  Just let me know when you need something.");
                        break;

                    case "piper resume talking":
                        Piper.Resume();
                        break;
                    #endregion

                    case "check for new emails":
                        Piper.Speak("Right Away, " + Settings.Default.User);
                        CheckForEmails();

                        break;

                    case "add a task":
                        Piper.SpeakAsync("Right away");
                        Tasks t = new Tasks();
                        t.ShowDialog();                        
                        break;

                    case "what are my tasks":
                        Piper.SpeakAsync("I will retrieve your tasks");
                        getTasks();
                        break;

                    case "I would like to add a new shell command":
                        Piper.SpeakAsync("I will display an add shell command box for you " + Settings.Default.User);
                        ShellCommands scomm = new ShellCommands();
                        scomm.ShowDialog();
                        this.Close();
                        break;

                    case "I would like to add a new default command":
                    case "add a default command":
                    case "add a command":
                    case "I would like to add a command":
                        Piper.SpeakAsync("I will display the window to add a default command.  Keep in mind that the default command is a single command and not a shell command.");
                        DefaultCommands dcomm = new DefaultCommands();
                        dcomm.ShowDialog();
                        //this.Close();
                        break;

                    case "I would like to add a new social command":
                    case "add a social command":
                        Piper.SpeakAsync("I am displaying a window to add a social command now");
                        SocialCommands socComm = new SocialCommands();
                        socComm.ShowDialog();
                        break;

                    case "maximize":
                    case "maximize your screen":

                        try
                        {
                            WindowState = WindowState.Maximized;
                            Piper.SpeakAsync("maximizing my control window now.");
                            break;
                        }
                        catch
                        {
                            Piper.SpeakAsync("Failure to maximize my window.  Please check on this issue " + Settings.Default.User);
                            break;
                        }
                    case "what is on my schedule today":
                    case "get todays calendar events":
                        Piper.SpeakAsync("right away " + Settings.Default.User + ".  Give me a few seconds to load your calendar.");
                        QEvent = "today";
                        GetCalendarEvents();

                        break;

                    case "what is on my schedule tomorrow":
                    case "get tomorrows calendar events":
                        Piper.SpeakAsync("right away " + Settings.Default.User + ".  Give me a few seconds to load your calendar.");
                        QEvent = "tomorrow";
                        GetCalendarEvents();

                        break;

                    case "search google":
                        Piper.SpeakAsync("What would you like to know?");
                        break;

                    case "get news feed":
                        Piper.SpeakAsync("Getting news feed now");
                        PopulateRssFeed();
                        break;

                }
            }
           
           

        }
        private void Shell_SpeechRecognized(Object sender, SpeechRecognizedEventArgs e)
        {
            
            string speech = e.Result.Text; //Sets the SpeechRecognized event variable to a string variable called speech
            i = 0; //Ensures "i" is = to 0 so we can start our loop from the beginning of our arrays
            try
            {
                foreach (string line in shellCommands)
                {
                    if (line == speech) //If line == speech it will open the corresponding program/file
                    {
                        System.Diagnostics.Process.Start(shellLocation[i]); //Opens the program/file of the same elemental position as the ArrayShellCommands command that was equal to speech
                        Piper.SpeakAsync(shellResponse[i]); //Gives the response of the same elemental position as the ArrayShellCommands command that was equal to speech
                    }
                    i += 1; //if the line in ArrayShellCommands does not equal speech it will add 1 to "i" and go through the loop until it finds a match between the line and spoken event
                }
            }
            catch
            {
                i += 1;
                Piper.SpeakAsync("Im sorry it appears the shell command " + speech + " on line " + i + " is accompanied by either a blank line or an incorrect file location");
            }
        }

        private void Social_SpeechRecognized(Object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            i = 0;
            try
            {
                foreach (string line in socialCommands)
                {
                    if (line == speech)
                    {
                        Piper.SpeakAsync(socialResponse[i]);
                    }
                    i += 1;
                }
            }
            catch
            {
                i += 1;
                Piper.SpeakAsync("Im sorry it appears the social command " + speech + " on line " + i + " is accompanied by either a blank line or an incorrect file location");
            }
        }

        #endregion        

        #region Speech Detected
       


        #endregion
        private void PiperBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object open = BrowseDirectory + PiperBox.SelectedItem;
            try
            {
                System.Diagnostics.Process.Start(open.ToString());
            }
            catch
            {
                open = BrowseDirectory + "\\" + PiperBox.SelectedItem;
                System.Diagnostics.Process.Start(open.ToString());
            }
        }
        #region Weather
        public void getWeather()
        {
            string query = String.Format("http://weather.yahooapis.com/forecastrss?w=2432757");
            XmlDocument wData = new XmlDocument();
            wData.Load(query);

            XmlNamespaceManager manager = new XmlNamespaceManager(wData.NameTable);
            manager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");

            XmlNode channel = wData.SelectSingleNode("rss").SelectSingleNode("channel");
            XmlNodeList nodes = wData.SelectNodes("/rss/channel/item/yweather:forecast", manager);

            Temperature = channel.SelectSingleNode("item")
                                 .SelectSingleNode("yweather:condition", manager)
                                 .Attributes["temp"].Value;
            Condition = channel.SelectSingleNode("item")
                                 .SelectSingleNode("yweather:condition", manager)
                                 .Attributes["text"].Value;
            Humidity = channel.SelectSingleNode("yweather:atmosphere", manager)
                                     .Attributes["humidity"].Value;
            WindSpeed = channel.SelectSingleNode("yweather:wind", manager)
                                 .Attributes["speed"].Value;
            Town = channel.SelectSingleNode("yweather:location", manager)
                                 .Attributes["city"].Value;
            TFCond = channel.SelectSingleNode("item")
                                 .SelectSingleNode("yweather:forecast", manager)
                                 .Attributes["text"].Value;
        
        }
        #endregion
        #region Emails
                public void CheckForEmails()
                {
                    PiperBox.Items.Clear();
                    //SpeechSynthesizer Piper = new SpeechSynthesizer();
                    string GmailAtomUrl = "https://mail.google.com/mail/feed/atom";
                    //MainWindow form = new MainWindow();
                    XmlUrlResolver xmlResolver = new XmlUrlResolver();
                    xmlResolver.Credentials = new NetworkCredential(Settings.Default.GmailUser, Settings.Default.GmailPassword);
                    XmlTextReader xmlReader = new XmlTextReader(GmailAtomUrl);
                    xmlReader.XmlResolver = xmlResolver;
                    try
                    {
                        XNamespace ns = XNamespace.Get("http://purl.org/atom/ns#");
                        XDocument xmlFeed = XDocument.Load(xmlReader);


                        var emailItems = from item in xmlFeed.Descendants(ns + "entry")
                                         select new
                                         {
                                             Author = item.Element(ns + "author").Element(ns + "name").Value,
                                             Title = item.Element(ns + "title").Value,
                                             Link = item.Element(ns + "link").Attribute("href").Value,
                                             Summary = item.Element(ns + "summary").Value                                    
                                     
                                         };
                        PiperBox.Items.Clear();
                        foreach (var item in emailItems)
                        {
                            if (item.Title == String.Empty)
                            {
                                PiperBox.Items.Add("Message from " + item.Author + ", There is no subject and the summary reads, " + item.Summary);
                        
                                //PiperBox.Items.Add(item.Link);
                            }
                            else
                            {
                                PiperBox.Items.Add("Message From:  " + item.Author + ": " + item.Title);
                        
                                //PiperBox.Items.Add(item.Link);
                            }
                        }

                        if (emailItems.Count() > 0)
                        {
                            if (emailItems.Count() == 1)
                            {
                                Piper.SpeakAsync("You have 1 new email.");
                                foreach (var item in emailItems)
                                {
                                    Piper.SpeakAsync("Message From:  " + item.Author + ": " + item.Title);
                                }
                            }
                            else
                            {
                                Piper.SpeakAsync("You have " + emailItems.Count() + " new emails.");
                                foreach (var item in emailItems)
                                {
                                    Piper.SpeakAsync("Message From:  " + item.Author + ": " + item.Title);
                                    //Piper.SpeakAsync("The body of the message is: " + item.Content);
                                }
                            }
                        }
                        else 
                        { Piper.SpeakAsync("You have no new emails"); QEvent = String.Empty; }
                    }
                    catch { Piper.SpeakAsync("You have submitted invalid log in information"); }
                }
        #endregion  
        #region AlarmClock
        private void AlarmClock_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            if (speech == "set the alarm")
            { AlarmTime = "set"; 
              Piper.Speak("What time?");              
            }
           
           
        }

        private void AlarmTimer_Tick(object sender, EventArgs e)
        {
            //int arraylength = MyMusicPaths.Count();
            //int Ran = rnd.Next(0, arraylength);
            var timenow = DateTime.Now;
            string time = timenow.GetDateTimeFormats('t')[0];
            if (Settings.Default.AClockEnbl == false && time != Settings.Default.Alarm)
            { Settings.Default.AClockEnbl = true; Settings.Default.Save(); }
            else if (time == Settings.Default.Alarm && Settings.Default.AClockEnbl == true)
            {
                Settings.Default.AClockEnbl = false; Piper.SpeakAsync("Initializing alarm sequence");
                //SelectedMusicFile = Ran;
                //axWindowsMediaPlayer1.URL = MyMusicPaths[SelectedMusicFile];
                //Jarvis.SpeakAsync("Now playing " + MyMusicNames[SelectedMusicFile]);
                //axWindowsMediaPlayer1.Ctlcontrols.play();
                getWeather();
                if (QEvent == "connected")
                { Piper.SpeakAsync("The time is currently " + time + " and the weather in " + Town + " is " + Condition + " at " + Temperature + " degrees. Get ready to start your day."); }
                else if (QEvent == "failed")
                { Piper.SpeakAsync("The time is currently " + time + ". Get ready to start your day."); }
            }
        }

        private void TimeSet_SpeechRecognized(Object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            if (AlarmTime == "set")
            {
                foreach (string time in AlarmAM)
                {
                    if (speech == time)
                    { 
                        AlarmTimer.Enabled = true;
                        Settings.Default.AClockEnbl = true;
                        Piper.SpeakAsync("Alarm set for " + time);
                        Settings.Default.Alarm = time;
                        Settings.Default.Save();
                        AlarmTime = "";
                    }
                }
                foreach (string time in AlarmPM)
                {
                    if (speech == time)
                    {
                        AlarmTimer.Enabled = true;
                        Settings.Default.AClockEnbl = true;
                        Piper.SpeakAsync("Alarm set for " + time);
                        Settings.Default.Alarm = time;
                        Settings.Default.Save();
                        AlarmTime = "";
                    }
                }
            }
            if (speech == "clear the alarm")
            {
                Settings.Default.Alarm = String.Empty;
                Settings.Default.Save();
                AlarmTimer.Enabled = false;
                Piper.SpeakAsync("The alarm is no longer set"); 
            }
            if (speech == "what time is the alarm")
            {
                if (Settings.Default.Alarm != "")
                {
                    Piper.SpeakAsync("The alarm is set for " + Settings.Default.Alarm);
                }
                else
                {
                    Piper.SpeakAsync("You do not have a time set for the alarm.");
                }
            }
 
        }
        #endregion
        public void UpdateCommands()
        {
           
            var sCommands = from i in context.ShellCommands select i;

            foreach (var command in sCommands)
            {
                shellCommands.Add(command.command);
                shellResponse.Add(command.response);
                shellLocation.Add(command.location);
            }

            try
            {
                shellcommandgrammar = new Grammar(new GrammarBuilder(new Choices(shellCommands.ToArray()))); 
                recEngine.LoadGrammarAsync(shellcommandgrammar); 
            }
            catch 
            {
                Piper.SpeakAsync("I've detected an in valid entry in your shell commands, possibly a blank line. Shell commands will cease to work until it is fixed.");
            }

            Piper.SpeakAsync("Your Commands have been updated!");
           
        }

        

        #region Calendar
        public async System.Threading.Tasks.Task GetCalendarEvents()
        {
            List<CalendarEvent> myEvents = new List<CalendarEvent>();
            //CalendarEvent newEvent = new CalendarEvent();
            try
            {
                // Call the GetExchangeClient method, which will authenticate
                // the user and create the ExchangeClient object.
                var client = await EnsureClientCreated();
                if (client == null)
                {
                    Piper.SpeakAsync("The client returned nothing.");
                }

                // Use the ExchangeClient object to call the Calendar API.
                // Get all events that have an end time after now.
                var today = DateTime.Now.Day;
                var tomorrow = DateTime.Now.AddDays(1).Day;
                var tod = DateTime.Now;
                var tom = DateTime.Now.AddDays(1);
                List<Event> todayEventList = new List<Event>();
                List<Event> tomorrowEventList = new List<Event>();

                var eventsResultsToday = await (from i in client.Me.Events
                                           where i.End >= DateTimeOffset.UtcNow
                                           select i).Take(10).ExecuteAsync();                


                // Order the results by start time.
                var events = eventsResultsToday.CurrentPage.OrderBy(e => e.Start);
                

                foreach (Event i in events.Where(i => i.Start.Value.Day == today))
                {
                    todayEventList.Add(i);
                }
                foreach (Event i in events.Where(i => i.Start.Value.Day == tomorrow))
                {
                    tomorrowEventList.Add(i);
                }

                
                // Create a CalendarEvent object for each event returned
                // by the API.
                switch(QEvent)
                { 
                    case "today":
                        PiperBox.Items.Clear();
                        Piper.SpeakAsync("you have " + todayEventList.Count() + "items on your calendar today.");
                        foreach (Event calendarEvent in todayEventList)
                        {
                            if (calendarEvent == null)
                            {
                                Piper.SpeakAsync("You have no calendar events for today");
                            }                           
                           
                            else
                            {
                                CalendarEvent newEvent = new CalendarEvent();
                                newEvent.Subject = calendarEvent.Subject;
                                newEvent.Location = calendarEvent.Location.DisplayName;
                                newEvent.Start = calendarEvent.Start.GetValueOrDefault().DateTime;
                                newEvent.End = calendarEvent.End.GetValueOrDefault().DateTime;
                                PiperBox.Items.Add(calendarEvent);                                
                                Piper.SpeakAsync("you have an event called " + calendarEvent.Subject);
                                Piper.SpeakAsync("the start time is " + calendarEvent.Start.Value.TimeOfDay.ToString());
                                Piper.SpeakAsync("the end time is " + calendarEvent.End.Value.TimeOfDay.ToString());
                                PiperBox.Items.Add("you have an event called " + calendarEvent.Subject);
                                PiperBox.Items.Add("the start time is " + calendarEvent.Start.Value.TimeOfDay.ToString());
                                PiperBox.Items.Add("the end time is " + calendarEvent.End.Value.TimeOfDay.ToString());
                                myEvents.Add(newEvent);
                            }
                        }
                        break;

                    case "tomorrow":
                        PiperBox.Items.Clear();
                        Piper.SpeakAsync("you have " + tomorrowEventList.Count() + "items on your calendar tomorrow.");
                        if (tomorrowEventList == null)
                        {
                            Piper.SpeakAsync("you have no events planned for tomorrow");
                        }
                        foreach (Event calendarEvent in tomorrowEventList)
                        {
                            if (calendarEvent.Start.Value.Day != tomorrow)
                            {
                                Piper.SpeakAsync("You have no calendar events for tomorrow");
                            }
                            else if (calendarEvent.Start.Value.Day == tomorrow)
                            {
                                CalendarEvent newEvent = new CalendarEvent();
                                newEvent.Subject = calendarEvent.Subject;
                                newEvent.Location = calendarEvent.Location.DisplayName;
                                newEvent.Start = calendarEvent.Start.GetValueOrDefault().DateTime;
                                newEvent.End = calendarEvent.End.GetValueOrDefault().DateTime;
                                PiperBox.Items.Add(calendarEvent);
                                Piper.SpeakAsync("you have an event called " + calendarEvent.Subject);
                                Piper.SpeakAsync("the start time is " + calendarEvent.Start.Value.TimeOfDay.ToString());
                                Piper.SpeakAsync("the end time is " + calendarEvent.End.Value.TimeOfDay);
                                PiperBox.Items.Add("you have an event called " + calendarEvent.Subject);
                                PiperBox.Items.Add("the start time is " + calendarEvent.Start.Value.TimeOfDay.ToString());
                                PiperBox.Items.Add("the end time is " + calendarEvent.End.Value.TimeOfDay.ToString());
                                myEvents.Add(newEvent);
                            }
                        }                                               
                        break;
                    
                        
                    
                }
            }
            // Required exception handling to make redirection work.
            catch (Exception e)
            {
                Piper.SpeakAsync("There was a problem with your request" + e);
            }

            
        }
        

        public static async Task<ExchangeClient> EnsureClientCreated()
        {
            if (_discoveryContext == null)
            {
                _discoveryContext = await DiscoveryContext.CreateAsync();
            }

            var dcr = await _discoveryContext.DiscoverResourceAsync(ServiceResourceId);

            _lastLoggedInUser = dcr.UserId;

            return new ExchangeClient(ServiceEndpointUri, async () =>
            {
                return (await _discoveryContext.AuthenticationContext.AcquireTokenSilentAsync(ServiceResourceId, _discoveryContext.AppIdentity.ClientId, new Microsoft.IdentityModel.Clients.ActiveDirectory.UserIdentifier(dcr.UserId, Microsoft.IdentityModel.Clients.ActiveDirectory.UserIdentifierType.UniqueId))).AccessToken;
            });
        }

        public static async System.Threading.Tasks.Task SignOut()
        {
            if (string.IsNullOrEmpty(_lastLoggedInUser))
            {
                return;
            }

            if (_discoveryContext == null)
            {
                _discoveryContext = await DiscoveryContext.CreateAsync();
            }

            await _discoveryContext.LogoutAsync(_lastLoggedInUser);
        }

        public void search()
        {
            
        }
           
#endregion
       
        public void checkGoogleCalendar()
        {
            PiperBox.Items.Clear();
            myService.setUserCredentials(Settings.Default.GmailUser, Settings.Default.GmailPassword);
            service.setUserCredentials(Settings.Default.GmailUser, Settings.Default.GmailPassword);
                       
            var today = DateTime.Now;
            EventQuery myQuery = new EventQuery("https://www.google.com/calendar/feeds/branton%40goldmail.etsu.edu/private/full");
            myQuery.StartTime = today;
            //myQuery.EndTime = new DateTime(2014, 9, 29);
           
            EventFeed myResultsFeed = myService.Query(myQuery);

            foreach (var entry in myResultsFeed.Entries)
            {
                PiperBox.Items.Add(entry.Title);
                PiperBox.Items.Add(entry.Title.Text);
                               
            }
            
       
        }

        public void getDefinition(string wordInput)
        {
            /*var definition = context.Synsets
                .Join(context.Senses, s => s.synsetid, ss => ss.synsetid, (s, ss) => new { s, ss })
                .Join(context.Words, w => w.ss.wordid, ss2 => ss2.wordid, (w, ss2) => new { w, ss2 })
                .Where(word => word.ss2.lemma.Equals(wordInput))
                .Select(def => def.w.s.definition)*/
            int randNum = rand.Next(1, 10);
            var query = (from d in context.Synsets
                        join s in context.Senses on new { d.synsetid } equals new { s.synsetid }
                        join w in context.Words on new { s.wordid } equals new { w.wordid }
                        where w.lemma == wordInput
                        select d.definition).ToList();
            Piper.SpeakAsync("I have found " + query.Count() + " definitions for " + wordInput + ". The definitions are as follows:");
            foreach (string i in query)
            {
               
                Piper.SpeakAsync(i);               
                  
            }

                             
        }

        void launchGoogle(string term)
        {
            string t = term.Replace(" ", "-");
            Process.Start("http://google.com/search?q=" + t);
            //SendKeys.Send("{ENTER}");
        }

#region TaskList
        void getTasks()
        {
            PiperBox.Items.Clear();
            var tasks = from i in context.Tasks select i.taskName;
            int taskCount = (from i in context.Tasks select i).Count();

            Piper.SpeakAsync("You have a total of " + taskCount + " tasks. They are as follows:");

            foreach (string i in tasks)
            {
                
                Piper.SpeakAsync("Task: " + i);
                PiperBox.Items.Add(i);
            }
 
        }
#endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = PiperBox.SelectedItem;

            var task = from i in context.Tasks where i.taskName == item select i;
             
            PiperBox.Items.Clear();
            var tasks = from i in context.Tasks select i.taskName;
            foreach (string i in tasks)
            {
                PiperBox.Items.Add(i);
            }
             
          
        }

        private void PopulateRssFeed()
        {
            string RssFeedUrl = "http://feeds.abcnews.com/abcnews/usheadlines";
            List<Feed> feeds = new List<Feed>();

            try
            {
                XDocument xDoc = new XDocument();
                xDoc = XDocument.Load(RssFeedUrl);
                var items = (from x in xDoc.Descendants("item")
                             select new
                                {
                                    title = x.Element("title").Value,
                                    link = x.Element("link").Value,
                                    pubDate = x.Element("pubDate").Value,
                                    description = x.Element("description").Value
                                });
                if (items != null)
                {
                    foreach (var i in items)
                    {
                        Feed f = new Feed
                        {
                            Title = i.title,
                            Link = i.link,
                            PublishDate = i.pubDate,
                            Description = i.description
                        };
                        feeds.Add(f);
                    }

                    System.Diagnostics.Process.Start("http://www.pandora.com");

                    for (int i = 0; i < 4; i++)
                    {
                        string title = feeds[i].Title;
                        string desc = feeds[i].Description;
                        Piper.SpeakAsync("In other National news today, " + title);
                        Piper.SpeakAsync(desc);
                        
                    }
                }
                
            }
            catch
            {
                throw;
            }

        }

        private void populateSportsRssFeed()
        {
            string RssFeedUrl = "http://www.cbssports.com/partners/feeds/rss/cfb_news";
            List<Feed> feeds = new List<Feed>();

            try
            {
                XDocument xDoc = new XDocument();
                xDoc = XDocument.Load(RssFeedUrl);
                var items = (from x in xDoc.Descendants("item")
                             select new
                             {
                                 title = x.Element("title").Value,
                                 link = x.Element("link").Value,
                                 pubDate = x.Element("pubDate").Value,
                                 description = x.Element("description").Value
                             });
                if (items != null)
                {
                    foreach (var i in items)
                    {
                        Feed f = new Feed
                        {
                            Title = i.title,
                            Link = i.link,
                            PublishDate = i.pubDate,
                            Description = i.description
                        };
                        feeds.Add(f);
                    }

                    System.Diagnostics.Process.Start("http://www.pandora.com");

                    for (int i = 0; i < 4; i++ )
                        
                        {
                            string title = feeds[i].Title;

                            Piper.SpeakAsync("In other College football news today, " + title);


                        }
                }

            }
            catch
            {
                throw;
            }
        }

        private void populateWorldRssFeed()
        {
            string RssFeedUrl = "http://www.cbsnews.com/latest/rss/world";
            List<Feed> feeds = new List<Feed>();

            try
            {
                XDocument xDoc = new XDocument();
                xDoc = XDocument.Load(RssFeedUrl);
                var items = (from x in xDoc.Descendants("item")
                             select new
                             {
                                 title = x.Element("title").Value,
                                 link = x.Element("link").Value,
                                 pubDate = x.Element("pubDate").Value,
                                 description = x.Element("description").Value
                             });
                if (items != null)
                {
                    foreach (var i in items)
                    {
                        Feed f = new Feed
                        {
                            Title = i.title,
                            Link = i.link,
                            PublishDate = i.pubDate,
                            Description = i.description
                        };
                        feeds.Add(f);
                    }

                    System.Diagnostics.Process.Start("http://www.pandora.com");

                    for (int i = 0; i < 4; i++)
                    {
                        string title = feeds[i].Title;
                        string desc = feeds[i].Description;
                        Piper.SpeakAsync("In other World news today, " + title);
                        Piper.SpeakAsync(desc);

                    }
                }

            }
            catch
            {
                throw;
            }
        }

        private void goodMorning()
        {
            Piper.SpeakAsync("Good morning " + Settings.Default.User);

            getWeather();
            Piper.SpeakAsync("The weather in " + Town + " is " + Condition + " at " + Temperature + " degrees.  There is a wind speed of "
                                + WindSpeed + " miles per hour and a humidity level of " + Humidity + "percent.");

            Piper.SpeakAsync("I have compiled the national news for today as follows: ");
            PopulateRssFeed();

            Piper.SpeakAsync("I have compiled the national news for today as follows: ");
            populateWorldRssFeed();

            Piper.SpeakAsync(Settings.Default.User + ", go ahead and grab a cup of coffee while I share with you today's college football headlines.");
            populateSportsRssFeed();


 
        }
       
               
        }
        
}
        

