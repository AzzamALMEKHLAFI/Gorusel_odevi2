using System;
using System.Net;
using System.IO;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using static Microsoft.Maui.Controls.Internals.Profile;

namespace Gorusel_odevi2;

public partial class KurlarSayfasi : ContentPage
{
	public KurlarSayfasi()
	{
		InitializeComponent();
        LoadExchangeRates();

    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        LoadExchangeRates();

    }

    private void LoadExchangeRates()
    {
        string url = "https://api.genelpara.com/embed/altin.json";

        try
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            string jsonverisi = "";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader r = new StreamReader(response.GetResponseStream());
                jsonverisi = r.ReadToEnd();


            
                Root tur = JsonConvert.DeserializeObject<Root>(jsonverisi);         
                DolarAlis.Text = tur.USD.alis;
                Dolarsatis.Text = tur.USD.satis;
                DolarFark.Text = tur.USD.degisim;
                String yon = tur.USD.d_yon;
                if (SetDolarYonuImage(yon))
                {
                    DolarYonu.Source ="up.png";

                }else { DolarYonu.Source = "down.png"; }


                EuroAlis.Text = tur.EUR.alis;
                Eurosatis.Text = tur.EUR.satis;
                EuroFark.Text = tur.EUR.degisim;                
                 yon = tur.EUR.d_yon;
                if (SetDolarYonuImage(yon))
                {
                    EuroYonu.Source = "up.png";

                }
                else { EuroYonu.Source = "down.png"; }


                SterlinAlis.Text = tur.GBP.alis;
                Sterlinsatis.Text = tur.GBP.satis;
                SterlinFark.Text = tur.GBP.degisim;
                yon = tur.GBP.d_yon;
                
                    if (SetDolarYonuImage(yon))
                {
                    SterlinYonu.Source = "up.png";

                }
                else { SterlinYonu.Source = "down.png"; }
                    
                AltinAlis.Text = tur.GA.alis;
                Altinsatis.Text = tur.GA.satis;
                AltinFark.Text = tur.GA.degisim;
                yon = tur.GA.d_yon;
                
                     if (SetDolarYonuImage(yon))
                {
                    AltinYonu.Source = "up.png";

                }
                else { AltinYonu.Source = "down.png"; }

                 cAltinAlis.Text = tur.C.alis;
                cAltinsatis.Text = tur.C.satis;
                 cAltinFark.Text = tur.C.degisim;
                 yon = tur.C.d_yon;
                if (SetDolarYonuImage(yon))
                {
                    cAltinYonu.Source = "up.png";

                }
                else { cAltinYonu.Source = "down.png"; }
                

                 GumusAlis.Text = tur.GAG.alis;
                Gumussatis.Text = tur.GAG.satis;
                 GumusFark.Text = tur.GAG.degisim;
                yon = tur.GAG.d_yon;
                if (SetDolarYonuImage(yon))
                {
                    GumusYonu.Source = "up.png";

                }
                else { GumusYonu.Source = "down.png"; }


                BTCAlis.Text = tur.BTC.alis;
                BTCsatis.Text = tur.BTC.satis;
                BTCFark.Text = tur.BTC.degisim;
                yon = tur.BTC.d_yon;
                if (SetDolarYonuImage(yon))
                {
                    BTCYonu.Source = "up.png";

                }
                else { BTCYonu.Source = "down.png"; }


                ETHAlis.Text=tur.ETH.alis;
                ETHsatis.Text = tur.ETH.satis;
                ETHFark.Text=tur.ETH.degisim;
                yon = tur.ETH.d_yon;
                if (SetDolarYonuImage(yon))
                {
                    ETHYonu.Source = "up.png";

                }
                else { ETHYonu.Source = "down.png"; }

              

                BISTAlis.Text = tur.XU100.alis;
                BISTsatis.Text = tur.XU100.satis;
                BISTFark.Text = tur.XU100.degisim;
                



            }
        }
        catch (Exception ex)
        {
            // Handle exception, log, or display an error message
            Console.WriteLine("Error: " + ex.Message);
        }
    }



    private Boolean SetDolarYonuImage(string durum)
    {
        
         if (durum.Contains("up")) {  return true; }
        else if (durum.Contains("down"))
        {
             return false;
        }
        else { return false; }
        } 


    public class BTC
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class C
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class ETH
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class EUR
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class GA
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class GAG
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class GBP
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class Root
    {
        public USD USD { get; set; }
        public EUR EUR { get; set; }
        public GBP GBP { get; set; }
        public GA GA { get; set; }
        public C C { get; set; }
        public GAG GAG { get; set; }
        public BTC BTC { get; set; }
        public ETH ETH { get; set; }
        public XU100 XU100 { get; set; }
    }

    public class USD
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
        public string d_oran { get; set; }
        public string d_yon { get; set; }
    }

    public class XU100
    {
        public string satis { get; set; }
        public string alis { get; set; }
        public string degisim { get; set; }
    }

    

    
}