using Other;

namespace Map.Vertexes
{
    public class TreasureVertexData : VertexData
    {
        public GetAble treasure;
        public override VertexType Type => VertexType.Treasure;
    }
}