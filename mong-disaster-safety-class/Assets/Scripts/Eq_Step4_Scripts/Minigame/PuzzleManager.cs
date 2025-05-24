using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public PuzzleImageSplitter imageSplitter;
    public GameObject puzzlePiecePrefab;
    public Transform puzzleBoardParent;
    public Transform slotParent;
    public Sprite slotGuideSprite;
    public Scene4story scene4story;

    private List<Sprite> puzzleSprites;
    private List<Vector2> spawnPositions = new List<Vector2>();

    private float scaleFactor = 0.7f;
    private int rows;
    private int cols;
    private float unitWidth;
    private float unitHeight;

    void Start()
    {
        puzzleSprites = imageSplitter.SplitImage();
        rows = imageSplitter.rows;
        cols = imageSplitter.columns;

        CreateSlotGuide();
        CreateInvisibleSlots();
        GenerateSpawnPositions();

        for (int i = 0; i < puzzleSprites.Count; i++)
        {
            int row = i / cols;
            int col = i % cols;

            GameObject piece = Instantiate(puzzlePiecePrefab, puzzleBoardParent);

            var spriteRenderer = piece.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = puzzleSprites[i];
            piece.transform.localScale = Vector3.one * scaleFactor;

            var boxCollider = piece.GetComponent<BoxCollider2D>();
            if (boxCollider != null && spriteRenderer.sprite != null)
            {
                boxCollider.size = spriteRenderer.sprite.bounds.size;
                boxCollider.offset = Vector2.zero;
            }

            piece.transform.position = spawnPositions[i];
            piece.name = $"Piece_{i}";

            var puzzlePieceScript = piece.GetComponent<PuzzlePiece>();
            puzzlePieceScript.correctSlotName = $"Slot_{row}_{col}";
        }
    }

    void CreateSlotGuide()
    {
        GameObject slotGuide = new GameObject("SlotGuideImage");
        slotGuide.transform.SetParent(slotParent);

        var sr = slotGuide.AddComponent<SpriteRenderer>();
        sr.sprite = slotGuideSprite;

        sr.sortingOrder = 5;
        sr.color = new Color(1f, 1f, 1f, 0.6f);

        slotGuide.transform.localScale = Vector3.one * scaleFactor;
        slotGuide.transform.position = Vector3.zero;
    }


    void CreateInvisibleSlots()
    {
        float width = slotGuideSprite.bounds.size.x * scaleFactor;
        float height = slotGuideSprite.bounds.size.y * scaleFactor;
        unitWidth = width / cols;
        unitHeight = height / rows;

        float startX = -(width / 2f) + (unitWidth / 2f);
        float startY = (height / 2f) - (unitHeight / 2f);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject slot = new GameObject($"Slot_{row}_{col}");
                slot.transform.SetParent(slotParent);

                float x = startX + col * unitWidth;
                float y = startY - row * unitHeight;
                slot.transform.position = new Vector3(x, y, -1f);

                var sr = slot.AddComponent<SpriteRenderer>();
                sr.sprite = null;
                sr.color = new Color(1, 1, 1, 0);

                var collider = slot.AddComponent<BoxCollider2D>();
                collider.size = new Vector2(unitWidth, unitHeight);
                collider.offset = Vector2.zero;

                slot.tag = "Slot";
            }
        }
    }

    void GenerateSpawnPositions()
    {
        float guideWidth = slotGuideSprite.bounds.size.x * scaleFactor;
        float guideHeight = slotGuideSprite.bounds.size.y * scaleFactor;

        Rect guideRect = new Rect(
            -guideWidth / 2f,
            -guideHeight / 2f,
            guideWidth,
            guideHeight
        );

        float camLimitX = 8f;
        float camLimitY = 4.5f;
        float margin = 0.4f;

        int totalCount = rows * cols;
        int attempts = 0;
        spawnPositions.Clear();

        while (spawnPositions.Count < totalCount && attempts < 3000)
        {   
            float x = Random.Range(-camLimitX + margin, camLimitX - margin);
            float y = Random.Range(-camLimitY + margin, camLimitY - margin);
            Vector2 newPos = new Vector2(x, y);

            bool overlapsGuide = guideRect.Contains(newPos);
            bool overlapsOthers = spawnPositions.Exists(pos => Vector2.Distance(pos, newPos) < Mathf.Min(unitWidth, unitHeight) * 0.9f);

            if (!overlapsGuide && !overlapsOthers)
            {
                spawnPositions.Add(newPos);
            }

            attempts++;
        }

        while (spawnPositions.Count < totalCount)
        {
            spawnPositions.Add(new Vector2(
                Random.Range(-camLimitX + 1f, camLimitX - 1f),
                Random.Range(-camLimitY + 1f, camLimitY - 1f)
            ));
        }
    }

    public void CheckPuzzleCompletion()
    {
        PuzzlePiece[] allPieces = FindObjectsOfType<PuzzlePiece>();
        foreach (PuzzlePiece piece in allPieces)
        {
            if (!piece.gameObject.name.StartsWith("Piece_")) continue;
            if (!piece.IsLocked()) return;
        }

        Debug.Log("Puzzle Complete!");

        if (scene4story != null)
        {
            scene4story.OnPuzzleComplete(); 
        }
    }
}
