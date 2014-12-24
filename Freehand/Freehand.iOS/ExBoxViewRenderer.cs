using System.ComponentModel;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Freehand;
using Freehand.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(ExBoxView),typeof(ExBoxViewRenderer))]
namespace Freehand.iOS
{
    internal class ExBoxViewRenderer : BoxRenderer {

        private ExBoxView _exBoxView;
        private UITouch _touch;

        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e) {
            base.OnElementChanged(e);

            //ExBoxViewへのポインタ取得
            _exBoxView = Element as ExBoxView;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            
            if (string.Equals(e.PropertyName, "Clear")) {
                //Clearが呼ばれた場合は、再描画する
                InvokeOnMainThread(SetNeedsDisplay); 
            }

        }

        public override void TouchesBegan(NSSet touches, UIEvent evt) {
            base.TouchesBegan(touches, evt);

            //UITouchの取得
            _touch = touches.AnyObject as UITouch;

            if (_touch != null) {
                var p = _touch.LocationInView(this); //位置情報取得
                _exBoxView.OnBegin((int) p.X, (int) p.Y);
            }
        }


        public override void TouchesMoved(NSSet touches, UIEvent evt) {
            base.TouchesMoved(touches, evt);
            if (_touch != null) {
                var p = _touch.LocationInView(this); //位置情報取得
                if (_exBoxView.OnMove((int)p.X, (int)p.Y)) {//データを追加すると同時に１つ前のデータを取得する
                    //追加があった場合は、再描画する
                    InvokeOnMainThread(SetNeedsDisplay);
                }
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt) {
            base.TouchesEnded(touches, evt);
            if (_touch != null) {
                _exBoxView.OnEnd();
            }
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt) {
            base.TouchesCancelled(touches, evt);
            if (_touch != null) {
                _exBoxView.OnEnd();
            }
        }

        public override void Draw(RectangleF rect) {

            //デフォルトの描画を無効にする
            //base.Draw(rect); 

            using (var context = UIGraphics.GetCurrentContext()) {
                
                //Lineデータにしたがって線を描画する
                foreach (var d in _exBoxView.Strokes.Data) {
                    context.SetLineWidth(d.Width);
                    context.SetStrokeColor(d.Color.ToCGColor());
                    for (var i = 0; i < d.Points.Count; i++) {
                        var x = (int)d.Points[i].X;
                        var y = (int)d.Points[i].Y;
                        if (i == 0) {
                            context.MoveTo(x,y);//始点
                        } else {
                            context.AddLineToPoint(x,y);//追加点
                        }
                    }
                    context.StrokePath();//描画
                }
            }
        }
    }
    
}
