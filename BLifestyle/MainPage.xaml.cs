using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;




namespace BLifestyle
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        information1 ht = new information1();
        information2 hm = new information2();
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

  
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
        }

        private async void chaxun1_Click(object sender, RoutedEventArgs e)
        {
             if (Input1.Text == "")
            {
                await new MessageDialog("请输入").ShowAsync();
                return;
            }
             string cn = Input1.Text;
             UriBuilder uri = new UriBuilder("http://apis.juhe.cn/idcard/index?key=b380bfeca1593ad5a6b47c2da3a655de&dtype=json&cardno=" + cn);
             WebRequest request = HttpWebRequest.Create(uri.Uri);
             request.Method = "GET";
             IAsyncResult result = (IAsyncResult)request.BeginGetResponse(ResponseCallback, request);


        }

        private async void ResponseCallback(IAsyncResult ar)
        {
            HttpWebRequest httpWebResponse = (HttpWebRequest)ar.AsyncState;
            WebResponse response = httpWebResponse.EndGetResponse(ar);
            Stream streamResult = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(streamResult, Encoding.GetEncoding("utf-8"));
            string json = myStreamReader.ReadToEnd();  
            ht = JosnCommon.SelectDictionary<information1>(json);
            if(ht.resultcode == "200")
            {
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                     {
                         Output1.Text = "地区:" + ht.result.area + "\n" + "性别" + ht.result.sex + "\n" + "生日" + ht.result.birthday;
                     });
            }
            else
            {
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    Output1.Text = "查询不到相关信息";
                });
            }
        }

        private async void chaxun2_Click(object sender, RoutedEventArgs e)
        {
             if (Input2.Text == "")
            {
                await new MessageDialog("请输入").ShowAsync();
                return;
            }
             string cm = Input2.Text;
             UriBuilder uri = new UriBuilder("http://op.juhe.cn/onebox/hospital/query.php?key=702f293f359eaf1f56aa0c2cb2e11bfb&dtype=json&hospital=" + cm);
             WebRequest request = HttpWebRequest.Create(uri.Uri);
             request.Method = "GET";
             IAsyncResult result = (IAsyncResult)request.BeginGetResponse(ResponseCallback1, request);

        }

        private async void ResponseCallback1(IAsyncResult ar)
        {
            HttpWebRequest httpWebResponse = (HttpWebRequest)ar.AsyncState;
            WebResponse response = httpWebResponse.EndGetResponse(ar);
            Stream streamResult = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(streamResult, Encoding.GetEncoding("utf-8"));
            string json = myStreamReader.ReadToEnd();  
            hm = JosnCommon.SelectDictionary<information2>(json);
            if(hm.reason == "查询成功")
            {
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                     {
                         Output2.Text = "名称：" + hm.result.title + "\n" + "级别：" + hm.result.level + "\n" + "地址：" + hm.result.address + "\n" + "乘车路线：" + hm.result.luxian + "\n" + "电话：" + hm.result.telephone;
                     });
            }
            else
            {
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    Output2.Text = "查询不到相关信息";
                });
            }
        }
    }
}
