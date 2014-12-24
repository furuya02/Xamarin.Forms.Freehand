using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Freehand
{
    public class Strokes {
        public List<Stroke> Data { get; private set; }
        private Stroke _stroke;//現在描画中の線
        private int _strokeWidth;//描画中の線の太さ（Moveの時に太さの範囲内のデータを追加しないようにするため記憶しておく）
        //最後に追加したデータ
        public int LastX { get; private set; }
        public int LastY { get; private set; }

        public Strokes() {
            Clear();
        }

        public void Begin(int x, int y,Color color,int strokeWidth) {
            _strokeWidth = strokeWidth;
            _stroke = new Stroke(color, _strokeWidth);
            Data.Add(_stroke); //現在描画中の線は、配列の最後にセットされている
            Move(x, y);
        }

        //データの追加があった場合、trueを返す
        public bool Move(int x, int y) {
            if (LastX == x && LastY == y)
                return false; //同じデータは追加されない
            if (Math.Abs(LastX - x) < _strokeWidth && Math.Abs(LastY - y) < _strokeWidth) {
                return false; //線の太さの範囲内の移動は追加しない
            }
            _stroke.Add(new Point(x, y));
            LastX = x;
            LastY = y;
            return true;
        }

        public void End() {
            LastX = -1;
            LastY = -1;
        }
        
        public void Clear() {
            Data = new List<Stroke>();
        }
    }
}