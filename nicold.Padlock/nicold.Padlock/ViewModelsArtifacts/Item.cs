using nicold.Padlock.Models.DataFile;

namespace nicold.Padlock.ViewModelsArtifacts
{
    public class Item
    {
        public Item(Card card)
        {
            Card = card;

            Text = card.Title;
            Description = $"used {card.UsedCounter} times";
            Description2 = card.IsFavotire ? "FAVORITE" : "";
        }

        public Card Card { get; set; }

        public string Text { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
    }
}