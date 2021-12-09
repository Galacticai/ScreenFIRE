<p align=center>
<img src="https://i.imgur.com/P5GFZ1R.png" alt="ScreenFIRE" height="200" />

# ScreenFIRE
#### Sophisticated screenshot utility for Linux and Windows! <br/><br/>
</p>

## Status:
 - ScreenFIRE is currently in the development phase.
 - It is not far from the first alpha release!
 - #### 👁️ Note: Repository is currently private. <br/><br/>

### ✔️ Done:
 - Support for both Windows and Linux
 - Support X Window System
 - Language detection & Support for English, Arabic, and Chinese (Simplified)
 - Caching translations and other frequently used stuff to improve performance 
 - A big library ported from my old project (Contains various text, math, geometry... and other general functions etc)
 - Monitors detection including various functions
 - Screenshot base
 - Screenshot interface
 - Screenshot toolbox (Floating buttons and options)
 - Base of adding shapes and text to the screenshot (Rectangle, Rounded rectangle, Circle, Ellipse)
 - Moderate configuration window (Currently contains default actions options .etc..)
 - Save the screenshot (and avoid file collisions)
 - Save the screenshot to clipboard
 - (And a lot of stuff)

### ⏳ Pending:
 - Support Wayland display protocol
 - (Shapes section) Add shapes to the screenshot
 - - Add base for more shapes (Like triangle, star, speech bubble...)
 - (Text section) Add text to the screenshot
 - (Select section) Custom shaped screenshot (Circular screenshot...)
 - Upload the screenshot to various websites (Like imgur...)
 - Record gif and videos
 - Global shortcuts support (Custom shortcuts for custom actions)
 - Homemade icons (Nice & clean icons instead of the default gtk icons)
 - (Effects section) filters for sreenshot (tinting, white balance. Etc.)
 - (Effects section) blur/pixelate effect for screenshot (ex: blur sensitive area of screenshot)
 - Cool UI animations where fitting and possible
 - Refactor GUI code into `Companion` namespace(s)
 - (Draw section) Add base for tools (Pen, highlighter ..etc..)
 - (AI Section) Add base for object recognition
 - - Auto categorize screenshots in folders (Each object in its folder)
 - (AI section) Add base for text detection (OCR)
 - _(Future maybe)_ (Video) Add base for auto memories video (slideshow of last month/year screenshots)
 - (Shape section) Add base for stickers
 - - Design premade stickers/ load from images
 - (Text section) Add meme fonts
 - (And a lot more stuff)

### 🪲 Known bugs:
 - (Sometimes) Double extensions while saving screenshot locally
 - (Rare) Corrupt memory / Reading-Writing to illegal memory space which causes ScreenFIRE to crash completely
 - (Wayland) Screenshot produces black images
 - (Wayland) `Window`s have black backgrounds instead of transparency
 - (Wayland) Toolbox ignores `Move` commands sent from other `Window`s
 - [FIXED] ~~Toolbox might be sent to behind the main window (which makes ScreenFIRE unusable)~~
 - [FIXED] ~~Toolbox background might get squashed down from its normal size sometimes~~
 - [FIXED] ~~(User misbehavior) Capturing wrong area when the user forcefully moves ScreenFIRE window~~


----
<p align=center>
Screen 🅵🅸🆁🅴 <br/>
<code> Made with .NET & GTK# </code> <br/>
<code> ©️2021 XEROling </code>
</p>
