using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace GroundControl.Station.Components.ChunkViews
{
  [ValueConversion(typeof(Point[]), typeof(Geometry))]
  public class PointsToPathConverter : IValueConverter
  {

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      Point[] points = (Point[])value;
      if(points == null)
      {
        return null;
      }

      if (points.Length > 0)
      {
        Point start = points[0];
        List<LineSegment> segments = new List<LineSegment>();
        for (int i = 1; i < points.Length; i++)
        {
          segments.Add(new LineSegment(points[i], true));
        }
        var figure = new PathFigure(start, segments, false); //true if closed
        var geometry = new PathGeometry();
        geometry.Figures.Add(figure);
        return geometry;
      }
      else
      {
        return null;
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotSupportedException();
    }

  }
}
