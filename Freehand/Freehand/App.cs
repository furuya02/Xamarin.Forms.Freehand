using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Freehand
{
    public class App
    {
        public static Page GetMainPage() {
            return new NavigationPage(new MyPage());
        }
    }


    public class MyPage : ContentPage {
        public MyPage() {

            var exBoxView = new ExBoxView() {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            ToolbarItems.Add(new ToolbarItem {
                Name = "Pen",
                Icon = "pen.png",
                Command = new Command(async () => {
                    var ar = new[] { "細い", "普通", "太い" };
                    var result = await DisplayActionSheet("ペンの太さを指定してください", "キャンセル", null,ar);
                    for (var i=0;i<ar.Length;i++) {
                        if (ar[i] == result) {
                            exBoxView.StrokeWidth = (i+1)*2;
                            break;
                        }
                    }
                })
            });
            ToolbarItems.Add(new ToolbarItem {
                Name = "Color",
                Icon = "color.png",
                Command = new Command(async () => {
                    var ar = new[] { "白","黒","赤", "青", "黄色" };
                    var result = await DisplayActionSheet("ペンの太さを指定してください", "キャンセル", null, ar);
                    switch (result) {
                        case "白":
                            exBoxView.StrokeColor = Color.White;
                            break;
                        case "黒":
                            exBoxView.StrokeColor = Color.Black;
                            break;
                        case "赤":
                            exBoxView.StrokeColor = Color.Red;
                            break;
                        case "青":
                            exBoxView.StrokeColor = Color.Blue;
                            break;
                        case "黄色":
                            exBoxView.StrokeColor = Color.Yellow;
                            break;
                    }

                })
            });
            ToolbarItems.Add(new ToolbarItem {
                Name = "Trash",
                Icon = "trash.png",
                Command = new Command(async () => {
                    var result = await DisplayAlert("Trash", "削除して宜しいですか?", "OK", "Cancel");
                    if (result) {
                        exBoxView.Clear(); //描画データの削除
                    }
                })
            });
            Content = exBoxView;
            //Content = new StackLayout() {
            //    Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
            //    Children = {exBoxView}
            //};
        }
    }
}
