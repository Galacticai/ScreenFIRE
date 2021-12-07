<img src="https://i.imgur.com/P5GFZ1R.png" alt="ScreenFIRE" height="200" />

# ScreenFIRE
#### Sophisticated screenshot utility for Linux and Windows! <br/><br/>

## Status:
 - ScreenFIRE is currently in the development phase.
 - It is not far from the first alpha release!
 - #### 👁️ Note: Repository is currently private. <br/><br/>

### ✔️ Done:
 - Support for both Windows and Linux
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
 - Add shapes to the screenshot
 - - Add base for more shapes (Like triangle, star, speech bubble...)
 - Add text to the screenshot
 - Add custom shaped screenshots (With transparency)
 - Upload the screenshot to various websites (Like imgur...)
 - Record gif and videos
 - Global shortcuts support (Custom shortcuts for custom actions)
 - Homemade icons (Nice & clean icons instead of the default gtk icons)
 - Refactor GUI code into `Companion` namespace(s)
 - (And a lot more stuff)

### 🪲 Known bugs:
 - (Sometimes) Double extensions while saving screenshot locally
 - (Rare) Corrupt memory / Reading-Writing to illegal memory space which causes ScreenFIRE to crash completely
 - [FIXED] ~~Toolbox might be sent to behind the main window (which makes ScreenFIRE unusable)~~
 - [FIXED] ~~Toolbox background might get squashed down from its normal size sometimes~~
 - [FIXED] ~~(User misbehavior) Capturing wrong area when the user forcefully moves ScreenFIRE window~~
