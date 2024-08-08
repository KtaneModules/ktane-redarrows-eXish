using System.Linq;

public class MazeGenerator
{
    private int _size;
    private MonoRandom _rand;
    public MazeGenerator(int size, MonoRandom rand)
    {
        _size = size;
        _rand = rand;
    }
    private bool[][] _visited;
    private char[] _charArr;

    public string GenerateMaze()
    {
        _visited = new bool[_size][];
        for (int i = 0; i < _visited.Length; i++)
            _visited[i] = new bool[_size];
        _charArr = Enumerable.Repeat('█', (_size * 2 + 1) * (_size * 2 + 1)).ToArray();
        for (int a = 0; a < _size; a++)
            for (int b = 0; b < _size; b++)
                _charArr[(a * (_size * 2 + 1) * 2) + (b * 2) + _size * 2 + 2] = ' ';
        var x = _rand.Next(0, _size);
        var y = _rand.Next(0, _size);
        Generate(x, y);
        return _charArr.Join("");
    }

    private void Generate(int x, int y)
    {
        _visited[x][y] = true;
        var arr = Enumerable.Range(0, 4).ToArray();
        _rand.ShuffleFisherYates(arr);
        var curPos = (x * (_size * 2 + 1) * 2) + (y * 2) + (_size * 2 + 2);
        for (int i = 0; i < 4; i++)
        {
            if (arr[i] == 0)
                if (y != 0 && !_visited[x][y - 1])
                {
                    _charArr[curPos - 1] = ' ';
                    Generate(x, y - 1);
                }
            if (arr[i] == 1)
                if (x != _size - 1 && !_visited[x + 1][y])
                {
                    _charArr[curPos + (_size * 2 + 1)] = ' ';
                    Generate(x + 1, y);
                }
            if (arr[i] == 2)
                if (y != _size - 1 && !_visited[x][y + 1])
                {
                    _charArr[curPos + 1] = ' ';
                    Generate(x, y + 1);
                }
            if (arr[i] == 3)
                if (x != 0 && !_visited[x - 1][y])
                {
                    _charArr[curPos - (_size * 2 + 1)] = ' ';
                    Generate(x - 1, y);
                }
        }
    }
}
