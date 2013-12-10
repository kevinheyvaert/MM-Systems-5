using ImageTools;
using ImageTools.Controls;
using ImageTools.IO;
using ImageTools.IO.Gif;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;


namespace MMSystems5Game
{
    public class GifViewer : DependencyObject
    {
        public GifViewer()
        {
            Decoders.AddDecoder<GifDecoder>();
           Uri uri = new Uri(@"\ViewModel\Gifs\gans.gif", UriKind.Relative);
            ExtendedImage image = new ExtendedImage();

            image.LoadingCompleted +=
                 (o, e) => Dispatcher.BeginInvoke(() => AnimationImage = image);
            image.UriSource = uri;

            Uri uri2 = new Uri(@"\ViewModel\Gifs\falling.gif", UriKind.Relative);
            ExtendedImage image2 = new ExtendedImage();

            image2.LoadingCompleted +=
                (o, e) => Dispatcher.BeginInvoke(() => AnimationImage2 = image2);
            image2.UriSource = uri2;
        }

        public static readonly DependencyProperty AnimationImageProperty =
            DependencyProperty.Register("AnimationImage",
                typeof(ExtendedImage),
                typeof(GifViewer),
                new PropertyMetadata(default(ExtendedImage)));

        public ExtendedImage AnimationImage
        {
            get { return (ExtendedImage)GetValue(AnimationImageProperty); }
            set { SetValue(AnimationImageProperty, value); }
        }


        public static readonly DependencyProperty AnimationImageProperty2 =
            DependencyProperty.Register("AnimationImage2",
                typeof(ExtendedImage),
                typeof(GifViewer),
                new PropertyMetadata(default(ExtendedImage)));

        public ExtendedImage AnimationImage2
        {
            get { return (ExtendedImage)GetValue(AnimationImageProperty2); }
            set { SetValue(AnimationImageProperty2, value); }
        }
    }

}
