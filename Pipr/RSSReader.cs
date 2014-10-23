using System;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using Pipr.Properties;
using System.Speech.Recognition;
using System.Speech.Synthesis;


namespace Pipr
{
    public class RSSReader
    {
        public void CheckForEmails()
        {
            //SpeechSynthesizer Piper = new SpeechSynthesizer();
            string GmailAtomUrl = "https://mail.google.com/mail/feed/atom";
            MainWindow form = new MainWindow();
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
                form.PiperBox.Items.Clear(); 
                foreach (var item in emailItems)
                {
                    if (item.Title == String.Empty)
                    {
                        form.PiperBox.Items.Add("Message from " + item.Author + ", There is no subject and the summary reads, " + item.Summary);
                        form.PiperBox.Items.Add(item.Link);
                    }
                    else
                    {                        
                        form.PiperBox.Items.Add(item.Title.ToString());
                        //form.PiperBox.Items.Add(item.Link);
                    }
                }

                if (emailItems.Count() > 0)
                {
                    if (emailItems.Count() == 1)
                    {
                        form.Piper.SpeakAsync("You have 1 new email");
                    }
                    else { form.Piper.SpeakAsync("You have " + emailItems.Count() + " new emails"); }
                }
                else if (form.QEvent == "Checkfornewemails" && emailItems.Count() == 0)
                { form.Piper.SpeakAsync("You have no new emails"); form.QEvent = String.Empty; }
            }
            catch { form.Piper.SpeakAsync("You have submitted invalid log in information"); }
        }
    }
}
