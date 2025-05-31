using System.Collections.Generic;
using UnityEngine;

public class PuzzleImageSplitter : MonoBehaviour
{
    public Texture2D sourceImage;
    public int rows = 3;
    public int columns = 5;

    public List<Sprite> SplitImage()
    {
        List<Sprite> pieces = new List<Sprite>();

        int pieceWidth = sourceImage.width / columns;
        int pieceHeight = sourceImage.height / rows;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int x = col * pieceWidth;
                int y = (rows - 1 - row) * pieceHeight;

                Rect rect = new Rect(x, y, pieceWidth, pieceHeight);
                Vector2 pivot = new Vector2(0.5f, 0.5f);

                Sprite piece = Sprite.Create(
                    sourceImage,
                    rect,
                    pivot,
                    100f,
                    0,
                    SpriteMeshType.FullRect
                );

                pieces.Add(piece);
            }
        }

        return pieces;
    }
}
