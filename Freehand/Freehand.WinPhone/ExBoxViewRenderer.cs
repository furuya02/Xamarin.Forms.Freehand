using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Phone.Controls;
using Freehand;
using Freehand.WinPhone;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(ExBoxView), typeof(ExBoxViewRenderer))]
namespace Freehand.WinPhone
{
    internal class ExBoxViewRenderer : ViewRenderer<ExBoxView, ExBoxViewControl>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ExBoxView> e) {
            base.OnElementChanged(e);

            //独自のユーザコントロールをセットする
            SetNativeControl(new ExBoxViewControl(Element));//パラメータにFormsのコントロールも渡しておく
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "Height") { //プロパティHeight若しくはWidthが変更された時、再描画する
                Control.Draw();//再描画
            }

            if (e.PropertyName == "Clear") {
                //Clearが呼ばれた場合は、再描画
                Control.Draw();
            }

        }
    }
}
