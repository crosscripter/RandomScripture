using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

class UI: Form {
    WebBrowser Display = new WebBrowser {
        Dock = DockStyle.Fill,
        ScrollBarsEnabled = false
    };
    
    void LoadScripture() {
        string[] lines = File.ReadAllLines("KJV-PCE.txt");
		var rand = new Random();
        var line = lines[rand.Next(lines.Length)];
        var index = line.IndexOf(":") + 3;
        var reference = line.Substring(0, index);
        var text = line.Substring(index);
        
        var html = @"
<!doctype html>
<html>
    <head>
        <title>{0}</title>
        <style>
            body {{
                font-family: 'Georgia', serif;
                font-size: 1.1em;
            }}
            
            p {{
                line-height: 1.25;
            }}
            
            h3 {{
                text-align: center;
            }}
        </style>
    </head>
    <body>
        <h3>{0}</h3>
        <p>{1}</p>
    </body>
</html>
";
        text = text.Replace("[", "<i>").Replace("]", "</i>");
        Text = "Random Scripture - " + reference;
        Display.DocumentText = string.Format(html, reference, text);
    }

    public UI() {
        Text = "Random Scripture";
        Controls.Add(Display);
        this.ShowOnMonitor(2);

        LoadScripture();
        var timer = new Timer();
        timer.Interval = 10000;
        timer.Tick += (s, e) => LoadScripture();
        timer.Start();
    }    
}

static class FormExtensions {
	public static void ShowOnMonitor(this Form @this, int showOnMonitor=0) {
		Screen[] sc = Screen.AllScreens;
		@this.StartPosition = FormStartPosition.CenterScreen;

		@this.Location = new Point(
			sc[showOnMonitor].Bounds.Left, 
			sc[showOnMonitor].Bounds.Top
		);

		@this.WindowState = FormWindowState.Normal;
		@this.ShowInTaskbar = false;
	}
}

class Program {
    [STAThread]
    static void Main() {
        Application.EnableVisualStyles();
        Application.Run(new UI());
    }
}