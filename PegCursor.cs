using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind
{
    public class PegCursor : IDisposable
    {
        public PegCursor(string FileName) : base()
        {
            using (FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                _stream.Write(buffer, 0, (int)fs.Length);
            }
            _stream.Position = 0;   
            Cursor = new Cursor(_stream);
            _stream.Position = 62;
            int b = _stream.ReadByte();
            int g = _stream.ReadByte();
            int r = _stream.ReadByte();
            shhPegColor = Color.FromArgb(r,g,b);
        }

        private MemoryStream _stream = new MemoryStream();

        public Cursor? Cursor { get; set; }

        public Color PegColor
        {
            get
            {
                return shhPegColor;
            }
            set
            {
                if (value.ToArgb() != shhPegColor.ToArgb())
                {
                    shhPegColor = value;
                    Color contrastColor = HighContrastWith(shhPegColor);
                    
                    byte[] buffer = new byte[_stream.Length];
                    _stream.Position = 0;
                    _stream.Read(buffer, 0, (int)_stream.Length);
                    buffer[62] = (byte)shhPegColor.B;
                    buffer[63] = (byte)shhPegColor.G;
                    buffer[64] = (byte)shhPegColor.R;
                    buffer[66] = (byte)contrastColor.B;
                    buffer[67] = (byte)contrastColor.G;
                    buffer[68] = (byte)contrastColor.R;

                   _stream = new MemoryStream(buffer);
                    Dispose();
                    Cursor = new Cursor(_stream);
                }
            }
        }
        private Color shhPegColor;

        // https://stackoverflow.com/questions/3942878/how-to-decide-font-color-in-white-or-black-depending-on-background-color
        private Color HighContrastWith(Color c)
        {
            if ((c.R * 0.299 + c.G * 0.587 + c.B * 0.114) > 186)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        public void Dispose()
        {
            if (Cursor != null)
            {
                Cursor.Dispose();
            }
        }
    }

}

#if IconFileFormat

* Info about how to make the peg cursors:
CUR
IconDir
00 00	Reserved; must be 0
02 00	Type of file; 2 == cursor
01 00	Number of images, always 1 for cursor

IconDirEntry
20		Width
20		Height
00		Number of colors, 0 if no palette (256 colors)
00		Reserved; must be 0
00 00	Horizontal coords of hotspot
00 00	Vertical coords of hotspot
30 01 00 00	Size of the image in bytes - 256 + 48
16 00 00 00	Offset of image from beginning of file - 22

Image Info
28 00 00 00	size of header
20 00 00 00	width in pixels
40 00 00 00	height in pixels (includes mask)
01 00		number of color planes - must be 1
01 00		bits per pixel - must be 1 for cursors (I think)
00 00 00 00	compression method
80 00 00 00 size of the bitmap data
00 00 00 00 horizontal res
00 00 00 00 vertical res
02 00 00 00 number of colors in the color table
02 00 00 00 important colors
00 00 00 00 COLOR INDEX 0 - black - THIS WILL BE THE COLOR TO CHANGE FOR PEG COLOR - offset 62
FF FF FF 00 COLOR INDEX 1 - white - THIS WILL BE THE CONTRAST COLOR FOR + (COPY) <-> (MOVE) - offset 66

Image
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx


Mask
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx
xx xx xx xx

#endif
