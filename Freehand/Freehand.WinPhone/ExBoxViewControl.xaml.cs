using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Freehand.WinPhone
{
    public partial class ExBoxViewControl : UserControl {
        private readonly ExBoxView _exBoxView;

        public ExBoxViewControl(ExBoxView exBoxView) {
            InitializeComponent();
            _exBoxView = exBoxView;
        }

        protected override void OnMouseEnter(MouseEventArgs e) {
            base.OnMouseEnter(e);

            var p = e.GetPosition(this);
            var x = (int)p.X;
            var y = (int)p.Y;

            _exBoxView.OnBegin(x,y);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            //前回の位置を取得
            var x1 = _exBoxView.LastX;
            var y1 = _exBoxView.LastY;

            var p = e.GetPosition(this);
            var x2 = (int) p.X;
            var y2 = (int) p.Y;

            if (_exBoxView.OnMove(x2,y2)) {
                //今回の線分だけを描画する
                Line(x1, y1, x2, y2, ToMediaColor(_exBoxView.StrokeColor),_exBoxView.StrokeWidth);
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e) {
            base.OnMouseLeave(e);
            _exBoxView.OnEnd();
        }

        //線描画(毎回Drawすると重すぎるので線を追加するだけのメソッドを利用する)
        public void Line(int x1,int y1,int x2,int y2,Color color,int width) {
            var line = new Line {
                Stroke = new SolidColorBrush(color),
                StrokeThickness = width,
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2
            };
            ExBoxViewCanvas.Children.Add(line);//線分の追加
        }


        //再描画
        public void Draw() {
            //全部を黒で塗りつぶす
            var rect = new Rectangle {
                Width = _exBoxView.Width,
                Height = _exBoxView.Height,
                Fill = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
            };
            ExBoxViewCanvas.Children.Add(rect);

            //Lineデータにしたがって線を描画する
            foreach (var d in _exBoxView.Strokes.Data) {
                var polyline = new Polyline {
                    StrokeThickness = d.Width,
                    Stroke = new SolidColorBrush(ToMediaColor(d.Color))
                };
                foreach (var p in d.Points) {
                    polyline.Points.Add(new Point(p.X, p.Y));
                }
                ExBoxViewCanvas.Children.Add(polyline);
            }

        }

        //Xamarin.Forms.ColorからSystem.Windows.Media.Colorへの変換
        Color ToMediaColor(Xamarin.Forms.Color color) {
            return Color.FromArgb((byte) (color.A*255), (byte) (color.R*255), (byte) (color.G*255),(byte) (color.B*255));
        }
    }
}
