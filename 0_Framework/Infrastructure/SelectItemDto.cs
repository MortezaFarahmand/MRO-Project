namespace _0_Framework.Infrastructure
{
    public class SelectItemDto
    {
        public long Value { get; set; }
        public string Text { get; set; }

        public SelectItemDto()
        {

        }
        public SelectItemDto(long value, string text)
        {
            Value = value;
            Text = text;
        }
    }

}
