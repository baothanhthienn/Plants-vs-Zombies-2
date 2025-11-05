using System;
using System.Collections.Generic;
using CustomProgram.Cards;
using CustomProgram.Plants;
using SplashKitSDK;

namespace CustomProgram
{
    public class InventoryCards
    {
        private List<Card> _inventoryCards;
        public InventoryCards()
        {
            _inventoryCards = new List<Card>();
            _inventoryCards.Add(new CardPeaShooter());
            _inventoryCards.Add(new CardSunFlower());
            _inventoryCards.Add(new CardTwinSunFlower());
            _inventoryCards.Add(new CardSnowpea());
            _inventoryCards.Add(new CardRepeater());
            _inventoryCards.Add(new CardPotatoMine());
            _inventoryCards.Add(new CardWallnut());
            _inventoryCards.Add(new CardSoldierPea());
            _inventoryCards.Add(new CardElectricPeashooter());
            _inventoryCards.Add(new CardScaredyShroom());

        }
        public List<Card> InventoryCard
        {
            get
            {
                return _inventoryCards;
            }
        }
        public void DrawInventoryCard()
        {
            int cardsPerRow = 8;           // max cards before wrapping to next row
            int cardWidth = 74;            // horizontal space between cards
            int cardHeight = 92;           // vertical space between rows
            int startX = 350;              // starting X position
            int startY = 160;              // starting Y position (top row)

            for (int i = 0; i < _inventoryCards.Count; i++)
            {
                int row = i / cardsPerRow;          // row index (0, 1, 2…)
                int col = i % cardsPerRow;          // column index (0–7)
                float x = startX + col * cardWidth;
                float y = startY + row * cardHeight;

                SplashKit.SpriteSetX(_inventoryCards[i].Sprite, x);
                SplashKit.SpriteSetY(_inventoryCards[i].Sprite, y);
            }
        }

    }
}
