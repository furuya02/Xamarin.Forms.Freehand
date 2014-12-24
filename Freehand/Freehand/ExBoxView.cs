using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Freehand
{
    public class ExBoxView : BoxView {

        public int StrokeWidth { get; set; }
        public Color StrokeColor { get; set; }
        public Strokes Strokes { get; set; }

        public int LastX {
            get { return Strokes.LastX; }
        }
        public int LastY {
            get { return Strokes.LastY; }
        }

        public void Clear() {
            Strokes.Clear();
            OnPropertyChanged();//レンダラーにプロパティ"Clear"が変化したことを伝える
        }

        public ExBoxView() {
            StrokeWidth = 6;//太い
            //デフォルトの描画色は、デバイスごとに背景に合わせる
            StrokeColor = Device.OnPlatform(Color.Black, Color.White, Color.White);
            Strokes = new Strokes();
        }

        public void OnBegin(int x, int y) {
            Strokes.Begin(x, y, StrokeColor,StrokeWidth);
        }

        public bool OnMove(int x, int y) {
            return Strokes.Move(x, y);
        }

        public void OnEnd() {
            Strokes.End();
        }
    }
}
