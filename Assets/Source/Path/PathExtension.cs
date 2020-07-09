namespace Path
{
    public static class PathExtension
    {
        public static float Length(this IPath path)
        {
            var points = path.GetVectors();
            var length = 0f;
            var iterator = points.GetEnumerator();
            if (!iterator.MoveNext()) return length;

            var prev = iterator.Current;
            while (iterator.MoveNext())
            {
                length += (iterator.Current - prev).magnitude;
                prev = iterator.Current;
            }

            return length;
        }
    }
}