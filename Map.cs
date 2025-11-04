using System.Collections.Generic;
using SplashKitSDK;

namespace CustomProgram
{
    public class Map : GameObject
    {
        private List<Cell> _cells;
        private Sprite _sunSprite;
        public Map() : base("Map", "Resources/images/map1.png")
        {
            SplashKit.SpriteSetX(this.Sprite, 0);
            SplashKit.SpriteSetY(this.Sprite, 0);
            _cells = new List<Cell>();
            _sunSprite = SplashKit.CreateSprite(new Bitmap("Sun","Resources/images/sun.png"));
            SplashKit.SpriteSetX(_sunSprite, 300);
            SplashKit.SpriteSetY(_sunSprite, 60);
        }

        public void DrawPlayerSun(float sun)
        {
            SplashKit.DrawText(string.Format("{0}",sun) , Color.Black, 320, 80);
        }

        public void DrawRemainingZombies(float zombsleft)
        {
            SplashKit.DrawText(string.Format("Remaining zombies: {0}", zombsleft), Color.Black, 950, 30);
        }

        public void GenerateCell() //generate cell for map
        {
            if (_cells.Count > 0) return;
            float initialcellX = 445;
            float initialcellY = 145;
            float cellWidth = 90;
            float cellHeight = 110;
            for (float x=0; x<5; x++)
            {
                for (float y=0; y<10; y++)
                {
                    Point2D point = new Point2D();
                    point.X = initialcellX + cellWidth * y;
                    point.Y = initialcellY + cellHeight * x;
                    Cell cell = new Cell(point);
                    _cells.Add(cell);
                }
            }
        }
        public Cell LocateCellByMousePostion(Point2D pt) //check when mouse location in a nearest cell
        {
            float minDistance = (float)SplashKit.PointPointDistance(_cells[0].Point, pt);
            Cell cellOfMouse = _cells[0];
            foreach (Cell cell in _cells)
            {
                float distance = (float)SplashKit.PointPointDistance(cell.Point, pt);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    cellOfMouse = cell;
                }
            }
            return cellOfMouse;
        }

        public void Draw()
        {
            this.Sprite.Draw();
            SplashKit.DrawSprite(_sunSprite);
        }

    }
}
